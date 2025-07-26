using System.Diagnostics;
using System.IO;
using Autodesk.Revit.DB;
using NWCBatchExport.Доп_классы_для_отладки;

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
                //Запуск таймера
                Stopwatch stopwatch = Stopwatch.StartNew();

                //Открытие документа
                OpenFile.OpenFileAsUsual(dir, _Data.ExternalCommandData);
                Document document = _Data.ExternalCommandData.Application.ActiveUIDocument?.Document;

                if (oldDoc != null)
                    oldDoc.Close(false);

                Worksets.EnableAll();
                _Export.toNWC(document);

                oldDoc = document;

                //Остановка таймера и логирование значения
                stopwatch.Stop();

                string time = stopwatch.Elapsed.ToString("mm\\:ss");
                Logger.Log(document.Title, $"Время открытия и экспорта {time} (мин/сек)");
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

                _Data.Log += document.Title + "\n";
            }
        }
    }
}
