using System;
using System.IO;
using System.Windows.Forms;
using Autodesk.Revit.UI;
using TaskDialog = Autodesk.Revit.UI.TaskDialog;

namespace NWCBatchExport.Events;

internal delegate void LoggingToFile(string fileName, string message);

internal class Logger
{
    /// <summary>
    /// Сохраняем текстовое окно, для обновления информации через событие.
    /// </summary>
    internal static RichTextBox textBoxForLog { private get; set; }

    /// <summary>
    /// Событие, которое при обращении к методу Log формирует сообщение и записывает логи.
    /// </summary>
    internal static event LoggingToFile EventLoggingToFile;

    /// <summary>
    /// Передает сообщение в обработчик, который формирует финальный вид строки.
    /// </summary>
    /// <param name="fileName">Название файла</param>
    /// <param name="message">Текст сообщения</param>
    internal static void Log(string fileName, string message)
    {
        EventLoggingToFile?.Invoke(fileName, message);
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
    /// Событие легирования. Формирует итоговую строку сообщения для вывода в файл и на экран.
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="message"></param>
    //Событие логирования
    internal static void OutLogger(string fileName, string message)
    {
        var messageOut = $"{DateTime.Now.ToString("[dd.MM.yyyy - HH:mm]")} | [{fileName.Replace("_отсоединено", "")}] | {message}\n";

        textBoxForLog.Text += messageOut;
        //RecordingDebugLog(messageOut); //Запись лога в файл временно отключена. Для релиза требуется доработка
    }
}
