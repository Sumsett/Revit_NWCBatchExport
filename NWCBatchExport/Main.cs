using System.Threading;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using NWCBatchExport.AdditionalFunctionality;
using NWCBatchExport.DataStorage;
using NWCBatchExport.Events;
using NWCBatchExport.RevitEvents;

namespace NWCBatchExport
{
    [Transaction(TransactionMode.Manual)]
    public class Main : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Json.ReadingJson();
            //commandData.Application.DialogBoxShowing += Application_DocumentOpened;
            //Logger.EventLoggingToFile += Logger.OutLogger;
            SubscribeToEvents.All();

            //-------------------
            ExternalEventExample handler = new ExternalEventExample();
            ExternalEvent exEvent = ExternalEvent.Create(handler);
            Data.handler = handler;
            Data.exEvent = exEvent;

            Data.ExternalCommandData = commandData;
            //--------------
            Thread thread = new Thread(() =>
            {
                FormMain formMain = new FormMain();
                formMain.Closed += (s, e) =>
                {
                    //commandData.Application.DialogBoxShowing -= Application_DocumentOpened;
                    //Logger.EventLoggingToFile -= Logger.OutLogger;
                    UnsubscribeToEvents.CurrentForm();
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
        /*
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
