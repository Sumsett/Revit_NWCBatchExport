using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace NWCBatchExport;

public class AvailabilityAlways
{
    public static bool IsCommandAvailable(UIApplication applicationData, CategorySet selectedCategories)
    {
        return true;
    }
}
