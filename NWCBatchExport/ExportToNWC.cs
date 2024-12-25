using Autodesk.Revit.DB;
using System.Collections.Generic;

namespace NWCBatchExport
{
    internal class _Export
    {
        public static void toNWC(Document document)
        {

            FilteredElementCollector collector = new FilteredElementCollector(document);
            ICollection<Element> views = collector.OfClass(typeof(View)).ToElements();

            //Найти виды для экспорта
            ElementId selectedView = null;
            string nameView = _Data.NameOfExportedView;

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
                _Data.Log += $"****Не найден вид {nameView}**** " + document.Title + "\n";
            }

            if (selectedView != null)
            {
                //Настроить настройки экспорта
                NavisworksExportOptions options = new NavisworksExportOptions
                {
                    ExportScope = NavisworksExportScope.View,
                    ViewId = selectedView,

                    ConvertLinkedCADFormats = false,
                    ExportRoomGeometry = false
                };


                string pathOut = _Data.PathToNWC;

                string fileName = document.Title;
                if (fileName.Contains("_отсоединено"))
                    fileName = fileName.Replace("_отсоединено", "");

                document.Export(pathOut, fileName, options);
            }


        }


    }
}
