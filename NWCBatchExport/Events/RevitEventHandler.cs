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
                    Logger.OutLogger("Ошибка", $"Тип - {e.ToString()} | ID - {taskDialogMessage.DialogId} | Сообщение - {taskDialogMessage.Message.Replace("\r\n", " ").Replace("\n", " ")}");
                    break;

                case MessageBoxShowingEventArgs messageBoxMessage:
                    Logger.OutLogger("Ошибка", $"Тип - {e.ToString()} | ID - {messageBoxMessage.DialogId} | Сообщение - {messageBoxMessage.Message.Replace("\r\n", " ").Replace("\n", " ")}");
                    break;

                case DialogBoxShowingEventArgs dialogBoxMessage:
                    Logger.OutLogger("Ошибка", $"Тип - {e.ToString()} | ID - {dialogBoxMessage.DialogId} | Сообщение - {dialogBoxMessage.DialogId.Replace("\r\n", " ").Replace("\n", " ")}");
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

                //Требуется обновление ресурсов перед печатью/экспортом
                else if (args2.DialogId == "TaskDialog_Update_Resources")
                    args2.OverrideResult(1001);

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
