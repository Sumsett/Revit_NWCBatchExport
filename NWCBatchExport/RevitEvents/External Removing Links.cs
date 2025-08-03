using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using NWCBatchExport.DataStorage;
using NWCBatchExport.Events;
using NWCBatchExport.FileProcessing;

namespace NWCBatchExport.RevitEvents
{
    internal class ExternalRemovingLinks : IExternalEventHandler
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
                    RemoveLinks.AllLinks(doc);
                    doc.Close(false);
                }

                //Остановка таймера и логирование значения
                stopwatch.Stop();
                string time = stopwatch.Elapsed.ToString("mm\\:ss");
                Logger.Log(fileName, $"Удаление связей {time} (мин/сек)");

                //Обновляем информацию для каждого файла
                ExecutionStatus.ProgressBarProcessed(dirs.Length);
            }
            stopwatchAll.Stop();
            string timeAll = stopwatchAll.Elapsed.ToString("hh\\:mm\\:ss");

            Logger.Log("Все файлы", $"Удаление связей {timeAll} (часы/мин/сек)\n");

            //Обновляем данные в интерфейсе после операций
            ExecutionStatus.FileName("Операция завершена");
            ExecutionStatus.ButtonsActive(true);
        }

        public string GetName()
        {
            return "Удаление всех связей .rvt в файле";
        }
    }
}