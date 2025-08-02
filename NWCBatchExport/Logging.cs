using System;
using System.IO;
using System.Windows.Forms;
using Autodesk.Revit.UI;

namespace NWCBatchExport
{
    public delegate void LoggingToFile(string fileName, string message);

    public class Logger
    {
        /// <summary>
        /// Сохраняем текстовое окно, для обновления информации через событие.
        /// </summary>
        public static RichTextBox textBoxForLog { private get; set; }

        /// <summary>
        /// Событие, которое при обращении к методу Log формирует сообщение и записывает логи.
        /// </summary>
        public static event LoggingToFile LoggingToFile;

        /// <summary>
        /// Формирует сообщение для записи в лог.
        /// </summary>
        /// <param name="fileName">Название файла</param>
        /// <param name="message">Текст сообщения</param>
        public static void Log(string fileName, string message)
        {
            LoggingToFile?.Invoke(fileName, message);
        }

        /// <summary>
        /// Записывает строку в файл логов.
        /// </summary>
        /// <param name="messageToRecord">Сообщение для записи</param>
        private static void RecordingDebugLog(string messageToRecord)
        {
            //Получаем путь для логов
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            try //Записываем данные
            {
                var fullPath = Path.Combine(documentsPath, "log.txt");
                File.AppendAllText(fullPath, messageToRecord);
            }
            catch //Если не получилось, выводим сообщение
            {
                TaskDialog.Show("Предупреждение", $"Не удалось записать в тестовой лог по пути {documentsPath}");
            }
        }

        /// <summary>
        /// Событие легирования
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="message"></param>
        //Событие логирования
        public static void LoggerOut(string fileName, string message)
        {
            var messageOut = $"{DateTime.Now.ToString("[dd.MM.yyyy - HH:mm]")} | [{fileName.Replace("_отсоединено", "")}] | {message}\n";

            textBoxForLog.Text += messageOut;
            RecordingDebugLog(messageOut);
        }

    }

}
