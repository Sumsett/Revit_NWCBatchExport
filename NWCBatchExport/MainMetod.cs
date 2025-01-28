using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.IO;

namespace NWCBatchExport
{
    internal class _SettingsAndOpeningFile
    {
        static public void Body()
        {
            string[] dirs = Directory.GetFiles(_Data.PathToRVT, "*.rvt");

            Document oldDoc = null;

            foreach (string dir in dirs)
            {
                if (oldDoc != null)
                    oldDoc.Close(false);

                OpenFile.OpenFileAsUsual(dir, _Data.ExternalCommandData);
                Document document = _Data.ExternalCommandData.Application.ActiveUIDocument?.Document;

                _Export.toNWC(document);

                _Data.Log += document.PathName;
            }
        }
    }
}
