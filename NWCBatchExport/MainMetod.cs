using Autodesk.Revit.DB;
using System.IO;

namespace NWCBatchExport
{
    internal class _SettingsAndOpeningFile
    {
        static public void ExportNWC()
        {
            string[] dirs = Directory.GetFiles(_Data.PathToRVT, "*.rvt");

            Document oldDoc = null;

            foreach (string dir in dirs)
            {
                OpenFile.OpenFileAsUsual(dir, _Data.ExternalCommandData);
                Document document = _Data.ExternalCommandData.Application.ActiveUIDocument?.Document;

                if (oldDoc != null)
                    oldDoc.Close(false);

                Class1.AAAA();
                _Export.toNWC(document);

                oldDoc = document;

                _Data.Log += document.PathName;
            }
        }

        static public void RemovingAllLinks()
        {
            string[] dirs = Directory.GetFiles(_Data.PathToRVT, "*.rvt");

            Document oldDoc = null;

            foreach (string dir in dirs)
            {
                OpenFile.OpenFileAsUsual(dir, _Data.ExternalCommandData);
                Document document = _Data.ExternalCommandData.Application.ActiveUIDocument?.Document;

                if (oldDoc != null)
                    oldDoc.Close(false);

                RemoveLinksCommand.AAA(document);

                oldDoc = document;

                _Data.Log += document.PathName + "\n";
            }
        }
    }
}
