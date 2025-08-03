using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.UI;

namespace NWCBatchExport.RevitEvents
{
    public class UnsubscribeEvents : IExternalEventHandler
    {
        public void Execute(UIApplication app)
        {
            //commandData.Application.DialogBoxShowing -= Application_DocumentOpened;
            //Logger.LoggingToFile -= LoggerOut;

            TaskDialog.Show("Оповещение", "Произошла отписка от событий");
        }

        public string GetName()
        {
            return "Отписка от событий";
        }
    }
}
