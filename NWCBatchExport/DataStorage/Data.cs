﻿using Autodesk.Revit.UI;

namespace NWCBatchExport.DataStorage;

internal class Data
{
    internal static ExternalCommandData ExternalCommandData { get; set; }

    #region Данные для интерфейса
    internal static string NameOfExportedView { get; set; }
    internal static string PathToRVT { get; set; }
    internal static string PathToNWC { get; set; }
    #endregion

    #region Настройки выгрузки NWC
    internal static bool UnloadingRoomGeometry { get; set; }
    #endregion

    #region Отладочная передача данных
    //=== Экспорт NWC ===
    internal static ExternalEvent EventExportNWC { get; set; }
    internal static ExternalEvent UnsubscribeEventsRevit { get; set; }
    internal static ExternalEvent RemovingLinks { get; set; }
    internal static ExternalEvent Tests { get; set; }
    #endregion
}