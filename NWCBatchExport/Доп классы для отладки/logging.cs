using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace NWCBatchExport.Доп_классы_для_отладки
{
    public delegate void LoggingToFile(string fileName, string message);

    public class Logger
    {
        public static event LoggingToFile loggingToFile;

        public static void Log(string fileName, string message)
        {
            loggingToFile?.Invoke(fileName, message);
        }

        public static void RecordingDebugLog(string messageToRecord)
        {
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            try
            {
                var fullPath = Path.Combine(documentsPath, "log.txt");
                File.AppendAllText(fullPath, messageToRecord);
            }
            catch 
            {
                TaskDialog.Show("Предупреждение", $"Не удалось записать в тестовой лог по пути {documentsPath}");
            }
        }
    }

}
