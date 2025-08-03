using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using NWCBatchExport.DataStorage;
using NWCBatchExport.Events;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NWCBatchExport.FileProcessing
{
    internal class _Export
    {
        public static void toNWC(Document document)
        {
            FilteredElementCollector collector = new FilteredElementCollector(document);
            ICollection<Element> views = collector.OfClass(typeof(View)).ToElements();

            //Найти виды для экспорта
            ElementId selectedView = null;
            string nameView = Data.NameOfExportedView;

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
                Logger.Log(document.Title, $"Не найден вид {nameView}");
            }

            if (selectedView != null)
            {
                //Настроить настройки экспорта
                NavisworksExportOptions options = new NavisworksExportOptions
                {
                    ExportScope = NavisworksExportScope.View,
                    ViewId = selectedView,

                    ConvertLinkedCADFormats = false,
                    ExportRoomGeometry = Data.UnloadingRoomGeometry
                };


                string pathOut = Data.PathToNWC;

                string fileName = document.Title;
                if (fileName.Contains("_отсоединено"))
                    fileName = fileName.Replace("_отсоединено", "");

                document.Export(pathOut, fileName, options);
            }
        }
    }
}
