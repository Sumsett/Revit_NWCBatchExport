using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using NWCBatchExport.DataStorage;
using NWCBatchExport.Events;
using NWCBatchExport.FileProcessing;
using System.Diagnostics;
using System.IO;

namespace NWCBatchExport.RevitEvents;

public class ExternalExportNwc : IExternalEventHandler
{
    public void Execute(UIApplication app)
    {
        Stopwatch stopwatchAll = Stopwatch.StartNew();
        string[] dirs = Directory.GetFiles(Data.PathToRVT, "*.rvt");
        int currentDocNumber = 0;

        foreach (string dir in dirs)
        {
            string fileName = Path.GetFileNameWithoutExtension(dir); // Получаем имя файла из папки

            //Обновляем информацию в интерфейсе
            ExecutionStatus.FileName($"Обработано: ({currentDocNumber}/{dirs.Length}). Текущий файл: {fileName}");
            ExecutionStatus.ButtonsActive(false);
            ExecutionStatus.ProgressBarTotal(dirs.Length);
            
            Stopwatch stopwatch = Stopwatch.StartNew(); //Запускаем таймер

            OpenFile.OpenFileWithoutShowing(dir, Data.ExternalCommandData); //Открываем документ
            DocumentSet documents = app.Application.Documents; //Получаем список всех открытых проектов


            foreach (Document doc in documents)
            {
                //Обрабатываем файлы
                Worksets.EnableAll(doc);
                Export.toNWC(doc);
                doc.Close(false);
            }

            //Остановка таймера и логирование значения
            stopwatch.Stop();
            string time = stopwatch.Elapsed.ToString("mm\\:ss");
            Logger.Log(fileName, $"Экспорт NWC {time} (мин/сек)");

            //Обновляем информацию для каждого файла
            currentDocNumber++;
            ExecutionStatus.ProgressBarProcessed(dirs.Length);
        }
        stopwatchAll.Stop();
        string timeAll = stopwatchAll.Elapsed.ToString("hh\\:mm\\:ss");

        Logger.Log("Все файлы", $"Экспорт NWC {timeAll} (часы/мин/сек)\n");

        //Обновляем данные в интерфейсе после операций
        ExecutionStatus.FileName("Операция завершена");
        ExecutionStatus.ButtonsActive(true);
    }

    public string GetName()
    {
        return "Экспорт NWC";
    }
}
