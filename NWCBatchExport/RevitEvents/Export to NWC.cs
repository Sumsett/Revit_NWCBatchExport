using System.Diagnostics;
using System.IO;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using NWCBatchExport.DataStorage;
using NWCBatchExport.Events;
using NWCBatchExport.FileProcessing;

namespace NWCBatchExport.RevitEvents
{
    public class ExternalEventExample : IExternalEventHandler
    {
        public void Execute(UIApplication app)
        {
            Stopwatch stopwatchAll = Stopwatch.StartNew();
            string[] dirs = Directory.GetFiles(Data.PathToRVT, "*.rvt");

            foreach (string dir in dirs)
            {
                string fileName = Path.GetFileNameWithoutExtension(dir); // Получаем имя файла из папки

                //Обновляем информацию в интерфейсе
                ExecutionStatus.FileName("Обрабатывается файл: " + fileName);
                ExecutionStatus.ButtonsActive(false);
                ExecutionStatus.ProgressBarTotal(dirs.Length);

                Stopwatch stopwatch = Stopwatch.StartNew(); //Запускаем таймер

                OpenFile.OpenFileWithoutShowing(dir, Data.ExternalCommandData); //Открываем документ
                DocumentSet documents = app.Application.Documents; //Получаем список всех открытых проектов

                foreach (Document doc in documents)
                {
                    //Обрабатываем файлы
                    Worksets.EnableAll(doc);
                    _Export.toNWC(doc);
                    doc.Close(false);
                }

                //Остановка таймера и логирование значения
                stopwatch.Stop();
                string time = stopwatch.Elapsed.ToString("mm\\:ss");
                Logger.Log(fileName, $"Не явное открытие документа {time} (мин/сек)");

                //Обновляем информацию для каждого файла
                ExecutionStatus.ProgressBarProcessed(dirs.Length);
            }
            stopwatchAll.Stop();
            string timeAll = stopwatchAll.Elapsed.ToString("hh\\:mm\\:ss");

            Logger.Log("Все файлы", $"Неявное открытие файлов {timeAll} (часы/мин/сек)\n");

            //Обновляем данные в интерфейсе после операций
            ExecutionStatus.FileName("Операция завершена");
            ExecutionStatus.ButtonsActive(true);
        }

        public string GetName()
        {
            return "Экспорт NWC";
        }
    }
}
