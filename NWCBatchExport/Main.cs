using System;
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
            commandData.Application.DialogBoxShowing += RevitEventHandler.ApplicationDocumentOpened;
            SubscribeToEvents.All();

            #region Создаем внешние события Revit
            //Экспорт NWC (Полная форма записи)
            //ExternalExportNwc exportNWC = new ExternalExportNwc();
            //ExternalEvent eventExportNWC = ExternalEvent.Create(exportNWC);
            //Data.EventExportNWC = eventExportNWC;

            //Более компактная
            Data.EventExportNWC = ExternalEvent.Create(new ExternalExportNwc()); //Экспорт NWC
            Data.UnsubscribeEventsRevit = ExternalEvent.Create(new ExternalUnsubscribeEvents()); //Отписка от событий
            #endregion

            Data._ExternalCommandData = commandData;
            //--------------
            Thread thread = new Thread(() =>
            {
                FormMain formMain = new FormMain();
                formMain.Closed += (s, e) =>
                {
                    Data.UnsubscribeEventsRevit.Raise();
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
    }
}
