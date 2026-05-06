using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using NWCBatchExport.Events;
using System.Linq;

namespace NWCBatchExport.RevitEvents;

public class ExternalTests : IExternalEventHandler
{
    public void Execute(UIApplication app)
    {
        //Поиск открытых документов
        DocumentSet documents = app.Application.Documents;

        var titles = documents.Cast<Document>().Select(d => d.Title).ToList();
        Logger.Log("Отладка", titles.Count == 0 ? "пусто" : string.Join("\n", titles));




        //string[] dirs = Directory.GetFiles(Data.PathToRVT, "*.rvt");
        //foreach (string dir in dirs)
        //{
        //    OpenFile.OpenFileWithoutShowing(dir, Data.ExternalCommandData); //Открываем документ
        //}
    }

    public string GetName()
    {
        return "Тест разных функций";
    }
}
