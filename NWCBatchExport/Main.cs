using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using NWCBatchExport;

namespace RevitFormTest
{
    [Transaction(TransactionMode.Manual)]
    public class StartClass : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Json.ReadingJson();

            _Data.ExternalCommandData = commandData;

            FormMain formMain = new FormMain();
            formMain.ShowDialog();

            return Result.Succeeded;
        }
    }
}
