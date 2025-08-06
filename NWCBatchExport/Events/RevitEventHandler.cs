using Autodesk.Revit.UI.Events;
using NWCBatchExport.AdditionalFunctionality;

namespace NWCBatchExport.Events;

internal class RevitEventHandler
{
    //События по отлову и закрытию предупреждений Revit
    internal static async void ApplicationDocumentOpened(object sender, DialogBoxShowingEventArgs e)
    {
        switch (e)
        {
            case TaskDialogShowingEventArgs args2:

                //Не удается найти связь Revit/AutoCAD
                if (args2.DialogId == "TaskDialog_Unresolved_References")
                    args2.OverrideResult(1002);

                //Отсутсвует сторонне средство (Плагин)
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
