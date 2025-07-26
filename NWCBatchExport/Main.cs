using System;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Events;
using NWCBatchExport;
using NWCBatchExport.Доп_классы_для_отладки;

namespace RevitFormTest
{
    [Transaction(TransactionMode.Manual)]
    public class StartClass : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Json.ReadingJson();
            commandData.Application.DialogBoxShowing += Application_DocumentOpened;
            Logger.loggingToFile += Logger.Logger_loggingToFile;


            _Data.ExternalCommandData = commandData;

            FormMain formMain = new FormMain();
            formMain.ShowDialog();

            commandData.Application.DialogBoxShowing -= Application_DocumentOpened;
            Logger.loggingToFile -= Logger.Logger_loggingToFile;
            return Result.Succeeded;
        }



        private async void Application_DocumentOpened(object sender, DialogBoxShowingEventArgs e)
        {
            //TaskDialog.Show("Открыт документ", args5.DialogId);
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
}
