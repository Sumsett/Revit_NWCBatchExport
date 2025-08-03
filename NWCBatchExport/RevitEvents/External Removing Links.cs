using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.UI;
using NWCBatchExport.Events;

namespace NWCBatchExport.RevitEvents
{
    internal class ExternalRemovingLinks
    {
        public void Execute(UIApplication app)
        {

        }

        public string GetName()
        {
            return "Удаление всех связей .rvt в файле";
        }
    }
}
