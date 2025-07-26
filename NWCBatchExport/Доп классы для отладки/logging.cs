using System;

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

        public static void Logger_loggingToFile(string fileName, string message)
        {
            _Data.Log += $"{DateTime.Now.ToString("[dd.MM.yyyy] [HH:mm]")} | [{fileName}] | {message}\n";
        }
    }

}
