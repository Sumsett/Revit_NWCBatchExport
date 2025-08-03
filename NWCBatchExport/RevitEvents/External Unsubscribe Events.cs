using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.UI;
using NWCBatchExport.Events;

namespace NWCBatchExport.RevitEvents
{
    public class ExternalUnsubscribeEvents : IExternalEventHandler
    {
        public void Execute(UIApplication app)
        {
            app.DialogBoxShowing -= RevitEventHandler.ApplicationDocumentOpened;
        }

        public string GetName()
        {
            return "Отписка от событий";
        }
    }
}
