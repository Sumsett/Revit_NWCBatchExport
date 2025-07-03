using Autodesk.Revit.UI;

namespace NWCBatchExport
{
    internal class _Data
    {
        public static Autodesk.Revit.DB.Document Document { get; set; }
        public static ExternalCommandData ExternalCommandData { get; set; }

        //=== Данные для интерфейса ===
        public static string NameOfExportedView { get; set; }
        public static string PathToRVT { get; set; }
        public static string PathToNWC { get; set; }
        public static string Log { get; set; }

        //=== Настройки выгрузки ===
        public static bool UnloadingRoomGeometry { get; set; }

    }
    public class _SavedJson
    {
        public string NameOfExportedView { get; set; }
        public string PathToRVT { get; set; }
        public string PathToNWC { get; set; }
    }
}
