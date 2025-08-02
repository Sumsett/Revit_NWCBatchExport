using Autodesk.Revit.UI;
using NWCBatchExport.Обращения_к_Ревит;

namespace NWCBatchExport
{
    internal class _Data
    {
        public static ExternalCommandData ExternalCommandData { get; set; }

        //=== Данные для интерфейса ===
        public static string NameOfExportedView { get; set; }
        public static string PathToRVT { get; set; }
        public static string PathToNWC { get; set; }

        //=== Настройки выгрузки ===
        public static bool UnloadingRoomGeometry { get; set; }
        //=== Тест
        public static ExternalEvent exEvent { get; set; }
        public static ExternalEventExample handler { get; set; }

    }
    public class _SavedJson
    {
        public string NameOfExportedView { get; set; }
        public string PathToRVT { get; set; }
        public string PathToNWC { get; set; }
    }
}
