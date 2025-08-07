using System.Collections.Generic;
using Autodesk.Revit.DB;
using NWCBatchExport.DataStorage;
using NWCBatchExport.Events;

namespace NWCBatchExport.FileProcessing;

internal class Export
{
    internal static void toNWC(Document document)
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
#if REVIT_2020_AND_GREATER
                ConvertLinkedCADFormats = false,
#endif  
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
