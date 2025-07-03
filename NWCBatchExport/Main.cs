using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Events;
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

            _Data.ExternalCommandData = commandData;

            FormMain formMain = new FormMain();
            formMain.ShowDialog();

            commandData.Application.DialogBoxShowing -= Application_DocumentOpened;
            return Result.Succeeded;
        }

        private void Application_DocumentOpened(object sender, DialogBoxShowingEventArgs e)
        {
            //e.OverrideResult(1);
            //TaskDialogShowingEventArgs args5 = e as TaskDialogShowingEventArgs;
            //TaskDialog.Show("Открыт документ", args5.DialogId);
            switch (e)
            {
                // (Konrad) Dismiss Unresolved References pop-up.
                case TaskDialogShowingEventArgs args2:
                    if (args2.DialogId == "TaskDialog_Unresolved_References")
                        args2.OverrideResult(1002);
                    break;
                default:
                    return;
            }

        }

        private void Application_DocumentOpened(object sender, DocumentOpenedEventArgs e)
        {
            //throw new System.NotImplementedException();
            Document doc = e.Document;
            //string docName = System.IO.Path.GetFileNameWithoutExtension(doc.PathName);
            //TaskDialog.Show("Открыт документ", $"Был открыт документ: {docName}");    
        }
    }
}
