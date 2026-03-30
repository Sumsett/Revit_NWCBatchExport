using Autodesk.Revit.UI.Events;
using NWCBatchExport.AdditionalFunctionality;
using NWCBatchExport.DataStorage;

namespace NWCBatchExport.Events;

internal class RevitEventHandler
{
    //События по отлову и закрытию предупреждений Revit
    internal static async void ApplicationDocumentOpened(object sender, DialogBoxShowingEventArgs e)
    {
        //Проверка на дополнительные исключения
        if (Data.ShowRevitWarnings)
        {
            switch (e)
            {
                case TaskDialogShowingEventArgs taskDialogMessage:
                    Logger.OutLogger("Ошибка открытия файла", $"Ошибка - {e.ToString()} | ID - {taskDialogMessage.DialogId} | Сообщение - {taskDialogMessage.Message}");
                    break;

                case MessageBoxShowingEventArgs messageBoxMessage:
                    Logger.OutLogger("Ошибка открытия файла", $"Ошибка - {e.ToString()} | ID - {messageBoxMessage.DialogId} | Сообщение - {messageBoxMessage.Message}");
                    break;

                case DialogBoxShowingEventArgs dialogBoxMessage:
                    Logger.OutLogger("Ошибка открытия файла", $"Ошибка - {e.ToString()} | ID - {dialogBoxMessage.DialogId} | Сообщение - {dialogBoxMessage.DialogId}");
                    break;

                default:
                    Logger.OutLogger("!!!", $"Не обрабатываемая ошибка - {e.ToString()}");
                    return;
            }
        }

        switch (e)
        {
            case TaskDialogShowingEventArgs args2:

                //Не удается найти связь Revit/AutoCAD
                if (args2.DialogId == "TaskDialog_Unresolved_References")
                    args2.OverrideResult(1002);

                //Отсутствует сторонне средство (Плагин)
                else if (args2.DialogId == "TaskDialog_Missing_Third_Party_Updaters" || args2.DialogId == "TaskDialog_Missing_Third_Party_Updater")
                    args2.OverrideResult(1);

                break;

            //НЕ РАБОТАЕТ ИЗ РЕВИТ АПИ, НУЖЕН ВИН АПИ
            case DialogBoxShowingEventArgs args3:
                if (args3.DialogId == "Dialog_Revit_DocWarnDialog")
                    await Win32Api.ClickOk();
                break;

            default:
                return;
        }
    }

}
