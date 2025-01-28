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
            _Data.ExternalCommandData = commandData;
            _Data.NameOfExportedView = "Navisworks";
            _Data.PathToRVT = @"C:\Work\ExportNWC\RVT";
            _Data.PathToNWC = @"C:\Work\ExportNWC\NWC";

            FormMain formMain = new FormMain();
            formMain.ShowDialog();

            return Result.Succeeded;
        }
    }
}
