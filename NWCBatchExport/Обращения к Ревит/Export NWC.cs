using System.Diagnostics;
using System.IO;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace NWCBatchExport.Обращения_к_Ревит
{
    public class ExternalEventExample : IExternalEventHandler
    {
        public void Execute(UIApplication app)
        {
            //for (int i = 0; i < 10; i++)
            //{
            //    Task.Delay(100).Wait();
            //    Logger.Log("___", "число: " + i.ToString());
            //}

            Stopwatch stopwatchAll = Stopwatch.StartNew();
            string[] dirs = Directory.GetFiles(_Data.PathToRVT, "*.rvt");

            foreach (string dir in dirs)
            {
                //Запуск таймера
                Stopwatch stopwatch = Stopwatch.StartNew();

                //Открытие документа
                OpenFile.OpenFileWithoutShowing(dir, _Data.ExternalCommandData);

                UIApplication uiApp = _Data.ExternalCommandData.Application;
                DocumentSet documents = uiApp.Application.Documents;

                foreach (Autodesk.Revit.DB.Document doc in documents)
                {
                    Worksets.EnableAll(doc);
                    _Export.toNWC(doc);
                    doc.Close(false);
                }

                //Остановка таймера и логирование значения
                stopwatch.Stop();
                string time = stopwatch.Elapsed.ToString("mm\\:ss");
                string fileName = Path.GetFileNameWithoutExtension(dir);

                Logger.Log(fileName, $"Не явное открытие документа {time} (мин/сек)");
            }
            stopwatchAll.Stop();
            string timeAll = stopwatchAll.Elapsed.ToString("hh\\:mm\\:ss");

            Logger.Log("Все файлы", $"Неявное открытие файлов {timeAll} (часы/мин/сек)\n");


        }

        public string GetName()
        {
            return "Экспорт NWC";
        }
    }
}
