using Autodesk.Revit.UI;
using NWCBatchExport.RevitEvents;

namespace NWCBatchExport.DataStorage
{
    internal class Data
    {
        public static ExternalCommandData ExternalCommandData { get; set; }

        #region Данные для интерфейса
        public static string NameOfExportedView { get; set; }
        public static string PathToRVT { get; set; }
        public static string PathToNWC { get; set; }
        #endregion

        #region Настройки выгрузки NWC
        public static bool UnloadingRoomGeometry { get; set; }
        #endregion

        #region Отладочная передача данных
        //=== Экспорт NWC ===
        public static ExternalEvent exEvent { get; set; }
        public static ExternalEventExample handler { get; set; }
        #endregion
    }

    public class SavedJson
    {
        public string NameOfExportedView { get; set; }
        public string PathToRVT { get; set; }
        public string PathToNWC { get; set; }
    }
}
