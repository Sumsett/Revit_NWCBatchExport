using System;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Events;
using NWCBatchExport;

namespace RevitFormTest
{
    [Transaction(TransactionMode.Manual)]
    public class StartClass : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Json.ReadingJson();
            commandData.Application.DialogBoxShowing += Application_DocumentOpened;
            NWCBatchExport.Logger.LoggingToFile += LoggerOut;


            _Data.ExternalCommandData = commandData;

            FormMain formMain = new FormMain();
            formMain.ShowDialog();

            commandData.Application.DialogBoxShowing -= Application_DocumentOpened;
            NWCBatchExport.Logger.LoggingToFile -= LoggerOut;
            return Result.Succeeded;
        }

        //Событие логирования
        private void LoggerOut(string fileName, string message)
        {
            var messageOut = $"{DateTime.Now.ToString("[dd.MM.yyyy - HH:mm]")} | [{fileName.Replace("_отсоединено", "")}] | {message}\n";
<<<<<<< HEAD
            _Data.Log += messageOut;
=======
>>>>>>> 928837a (Тестовая верстия для отладки 02)

            Logger.textBoxForLog.Text += messageOut;
            NWCBatchExport.Logger.RecordingDebugLog(messageOut);
        }

        //События по отлову и закрытию предупреждений Revit
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
