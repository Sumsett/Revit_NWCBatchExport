using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using NWCBatchExport.DataStorage;
using NWCBatchExport.FileProcessing;
using System.Collections.Generic;
using System.IO;

namespace NWCBatchExport.RevitEvents;

public class ExternalTests : IExternalEventHandler
{
    public void Execute(UIApplication app)
    {
        #region поиск открытых документов
        DocumentSet documents = app.Application.Documents; //Получаем список всех открытых проектов
        List<string> strings = new List<string>();

        foreach (Document doc in documents)
        {
            strings.Add(doc.Title);
        }
        var message = "пусто";

        if (strings.Count > 0)
        {
            message = string.Join("\n", strings);
        }

        TaskDialog.Show("Открытые файлы", message);
        #endregion

        string[] dirs = Directory.GetFiles(Data.PathToRVT, "*.rvt");
        foreach (string dir in dirs)
        {
            OpenFile.OpenFileWithoutShowing(dir, Data.ExternalCommandData); //Открываем документ
        }
    }

    public string GetName()
    {
        return "Тест разных функций";
    }
}
