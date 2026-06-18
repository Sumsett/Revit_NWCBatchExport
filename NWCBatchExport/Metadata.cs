using ITEM.SDK.Metadata;
using System;
using System.Collections.Generic;

namespace ITEM_BatchExport;

public class Metadata : ITEMPluginMetadata
{
    public override string StringID => "ITEM_BatchExport";

    public override string Name => "Пакетный экспорт NWC";

    public override string Description => "Плагин, предназначенный для пакетного экспорта всех файлов в указанной папке в формат .nwc с заданными настройками и для пакетного удаления всех связей .rvt в файле с последующим сохранением";

    public override string ThisUnitSupportsRevitVersion =>
#if REVIT_2019_AND_GREATER && !REVIT_2020_AND_GREATER
        "2019";
#elif REVIT_2020_AND_GREATER && !REVIT_2025_AND_GREATER
        "2020-2024";
#else
        "2025-inf";
#endif

    public override IEnumerable<PullDownButtonMetadata> PullDownButtons => null;

    public override Dictionary<Type, PushButtonMetadata> PushButtons => new()
    {
        {
            typeof(Main), 
            new PushButtonMetadata(
                "Пакетный экспорт NWC",
                "Плагин, предназначенный для пакетного экспорта всех файлов в указанной папке в формат .nwc с заданными настройками и для пакетного удаления всех связей .rvt в файле с последующим сохранением",
                "49c5a8a6-7ab0-421b-b567-19f65281ce4c",
                "component/ITEM_BatchExport_icons.png",
                IconSourceType.ResourceInAssembly,
                null,
                "ITEM_BatchExport.AvailabilityAlways.IsCommandAvailable"
            )
        }
    };

    public override PluginDivisionEnum PluginDivision => PluginDivisionEnum.Tools;
}
