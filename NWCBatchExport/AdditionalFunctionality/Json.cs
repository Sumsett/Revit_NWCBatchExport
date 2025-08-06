using System.IO;
using System.Reflection;
using Autodesk.Revit.UI;
using Newtonsoft.Json;
using NWCBatchExport.DataStorage;

namespace NWCBatchExport.AdditionalFunctionality;

internal class Json
{
    //Указываем путь до Json.
    //Json храниться по пути сборки + доп папка NWCBatchExport.
    static string assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
    static string jsonFolder = Path.Combine(assemblyFolder, "NWCBatchExport");
    static string pathToJson = Path.Combine(jsonFolder, "Data.json");

    internal static void CreateJson(string path)
    {
        //В случае если нет папки NWCBatchExport, то создаем ее.
        //Либо если есть, просто создаем ее.
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
            File.Create(Path.Combine(path, "Data.json"));
        }

        else
        {
            File.Create(Path.Combine(path, "Data.json"));
        }
    }

    internal static void ReadingJson()
    {
        //Если есть Json, то читаем его. Либо создаем новый, если его нет.
        if (File.Exists(pathToJson))
        {
            //Читаем и десериализуем данные.
            var text = File.ReadAllText(pathToJson);
            SavedJson savedParameters = new SavedJson();
            savedParameters = JsonConvert.DeserializeObject<SavedJson>(text);

            try
            {
                //Записываем в Data значения переменных.
                Data.NameOfExportedView = savedParameters.NameOfExportedView;
                Data.PathToRVT = savedParameters.PathToRVT;
                Data.PathToNWC = savedParameters.PathToNWC;
            }
            catch
            {
                TaskDialog.Show("Предупреждение", "Не удалось прочитать файл. Либо файл пуст, либо данные были повреждены");
            }
        }

        else
        {
            try
            {
                CreateJson(jsonFolder);
                TaskDialog.Show("Предупреждение", "Файл создан");
            }
            catch
            {
                TaskDialog.Show("Предупреждение", "Не удалось создать Json");
            }
        }
    }

    internal static void WriteJson(SavedJson savedParameters)
    {
        //Сериализуем данные, и записываем в существующий файл, если Json существует
        if (File.Exists(pathToJson))
        {
            string text = JsonConvert.SerializeObject(savedParameters, Formatting.Indented);
            File.WriteAllText(pathToJson, text);
        }

        else
        {
            TaskDialog.Show("Предупреждение", "Не удалось записать Json");
        }
    }
}
