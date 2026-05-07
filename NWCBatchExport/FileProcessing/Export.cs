using Autodesk.Revit.DB;
using NWCBatchExport.DataStorage;
using NWCBatchExport.Events;
using System;
using System.Linq;

namespace NWCBatchExport.FileProcessing;

internal class Export
{
    internal static void toNWC(Document document)
    {
        string nameView = Data.NameOfExportedView;

        View3D selectedView = new FilteredElementCollector(document).OfClass(typeof(View3D))
            .Cast<View3D>()
            .Where(x => !x.IsTemplate)
            .FirstOrDefault(x => x.Name.Equals(nameView, StringComparison.OrdinalIgnoreCase));

        if (selectedView != null)
        {
            //Настроить настройки экспорта
            NavisworksExportOptions options = new NavisworksExportOptions
            {
                ExportScope = NavisworksExportScope.View,
                ViewId = selectedView.Id,
#if REVIT_2020_AND_GREATER
                ConvertLinkedCADFormats = false,
#endif  
                ExportRoomGeometry = Data.UnloadingRoomGeometry,
                DivideFileIntoLevels = Data.DivideFileIntoLevels,
            };


            string pathOut = Data.PathToNWC;

            string fileName = document.Title;
            if (fileName.Contains("_отсоединено"))
                fileName = fileName.Replace("_отсоединено", "");

            document.Export(pathOut, fileName, options);
        }

        else
        {
            Logger.Log(document.Title, $"Не найден вид {nameView}. Файл не экспортирован");
            return;
        }
    }
}
