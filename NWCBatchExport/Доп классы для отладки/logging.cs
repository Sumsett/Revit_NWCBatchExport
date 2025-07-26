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
    }

}
