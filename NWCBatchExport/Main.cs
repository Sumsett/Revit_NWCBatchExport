using System.Threading;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using NWCBatchExport;
using NWCBatchExport.Обращения_к_Ревит;

namespace RevitFormTest
{
    [Transaction(TransactionMode.Manual)]
    public class StartClass : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Json.ReadingJson();
            //commandData.Application.DialogBoxShowing += Application_DocumentOpened;
            Logger.LoggingToFile += Logger.LoggerOut;


            //-------------------
            ExternalEventExample handler = new ExternalEventExample();
            ExternalEvent exEvent = ExternalEvent.Create(handler);
            _Data.handler = handler;
            _Data.exEvent = exEvent;

            _Data.ExternalCommandData = commandData;
            //--------------
            Thread thread = new Thread(() =>
            {
                FormMain formMain = new FormMain();
                formMain.Closed += (s, e) =>
                {
                    //commandData.Application.DialogBoxShowing -= Application_DocumentOpened;
                    Logger.LoggingToFile -= Logger.LoggerOut;
                };
                formMain.ShowDialog();

                // Необходимо для работы WPF окна
                System.Windows.Threading.Dispatcher.Run();
            });

            thread.SetApartmentState(ApartmentState.STA);
            thread.IsBackground = true;
            thread.Start();
            //--------------

            return Result.Succeeded;
        }

        #region События
        //Событие логирования
        /*
        private void LoggerOut(string fileName, string message)
        {
            var messageOut = $"{DateTime.Now.ToString("[dd.MM.yyyy - HH:mm]")} | [{fileName.Replace("_отсоединено", "")}] | {message}\n";

            Logger.textBoxForLog.Text += messageOut;
            Logger.RecordingDebugLog(messageOut);
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
        */
        #endregion
    }
}
