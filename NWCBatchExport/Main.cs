using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using NWCBatchExport.AdditionalFunctionality;
using NWCBatchExport.DataStorage;
using NWCBatchExport.Events;
using NWCBatchExport.RevitEvents;
using System;
using System.Threading;

namespace NWCBatchExport;

[Transaction(TransactionMode.Manual)]
public class Main : IExternalCommand
{
    public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
    {
        Json.ReadingJson();
        commandData.Application.DialogBoxShowing += RevitEventHandler.ApplicationDocumentOpened;
        SubscribeToEvents.All();

        
        #region Создаем внешние события Revit
        //Экспорт NWC (Полная форма записи)
        //ExternalExportNwc exportNWC = new ExternalExportNwc();
        //ExternalEvent eventExportNWC = ExternalEvent.Create(exportNWC);
        //Data.EventExportNWC = eventExportNWC;

        //(Более компактная)
        Data.EventExportNWC = ExternalEvent.Create(new ExternalExportNwc()); //Экспорт NWC
        Data.UnsubscribeEventsRevit = ExternalEvent.Create(new ExternalUnsubscribeEvents()); //Отписка от событий
        Data.RemovingLinks = ExternalEvent.Create(new ExternalRemovingLinks()); //Удаление связей
        Data.Tests = ExternalEvent.Create(new ExternalTests()); //Удаление связей
        #endregion

        Data.ExternalCommandData = commandData;

        try
        {
            Thread thread = new Thread(() =>
            {
                FormMain formMain = new FormMain();
                formMain.Closed += (s, e) =>
                {
                    Data.UnsubscribeEventsRevit.Raise();
                    UnsubscribeToEvents.CurrentForm();
                };
                formMain.Show();

                //Не дает потоку завершится, а ставит его на паузу для ожидания дальнейших действий.
                System.Windows.Threading.Dispatcher.Run();
            });

            thread.SetApartmentState(ApartmentState.STA);
            thread.IsBackground = true;
            thread.Start();
        }

        catch (Exception ex)
        {
            TaskDialog.Show("Не удалось создать главное окно", ex.Message); //Предупреждение для пользователя

            //Отписка от событий
            Data.UnsubscribeEventsRevit.Raise();
            UnsubscribeToEvents.CurrentForm();

            return Result.Failed;
        }

        return Result.Succeeded;
    }
}
