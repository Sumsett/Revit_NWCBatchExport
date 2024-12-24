using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace RevitFormTest
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]

    public class StartClass : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            //FormMain formMain = new FormMain(commandData.Application);
            //formMain.ShowDialog();



            string[] dirs = Directory.GetFiles(@"C:\Work\ExportNWC\RVT", "*.rvt");
            string text = string.Empty;

            foreach (string dir in dirs)
                text += dir + "\n";

            Document oldDOC = commandData.Application.ActiveUIDocument.Document;


            foreach (string dir in dirs)
            {
                OpenOptions openOptions = new OpenOptions();
                openOptions.DetachFromCentralOption = DetachFromCentralOption.DetachAndPreserveWorksets;
                openOptions.OpenForeignOption = OpenForeignOption.Open;


                //====== Открыетие файлов как обычно ======
                FilePath filePath = new FilePath(dir);
                commandData.Application.OpenAndActivateDocument(filePath, openOptions, true);
                Document doc = commandData.Application.ActiveUIDocument.Document;


                ////====== Открыетие файлов без показа пользователю экрана ======
                //Application app = commandData.Application.Application;
                //Document doc = app.OpenDocumentFile(dir);

                oldDOC.Close(false);
                oldDOC = doc;

                FilteredElementCollector collector = new FilteredElementCollector(doc);
                ICollection<Element> views = collector.OfClass(typeof(Autodesk.Revit.DB.View)).ToElements();

                //Найти виды для экспорта
                ElementId selectedView = null;

                //string nameView = "Navisworks";
                string nameView = "Navisworks подвал";


                foreach (Element view in views)
                {
                    if (view.Name == nameView)
                    {
                        selectedView = view.Id;
                        break;
                    }
                }

                if (selectedView == null)
                {
                    text += $"****Не найден вид {nameView}**** " + doc.Title + "\n";
                    continue;
                }

                //Настроить настройки экспорта
                NavisworksExportOptions options = new NavisworksExportOptions
                {
                    ExportScope = NavisworksExportScope.View,
                    ViewId = selectedView,

                    ConvertLinkedCADFormats = false,
                    ExportRoomGeometry = false
                };


                string pathOut = @"C:\Work\ExportNWC\NWC";
                doc.Export(pathOut, doc.Title, options);

            }


            TaskDialog.Show("Готово", "Экспортированные файлы:\n" + text);


            return Result.Succeeded;

        }
    }
}
