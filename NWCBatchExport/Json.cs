using System.IO;
using System.Reflection;
using Autodesk.Revit.UI;

namespace NWCBatchExport
{
    public class Json
    {

        public static void CreateJson(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                File.Create(Path.Combine(path, "Data.json"));
                TaskDialog.Show("Предупреждение", "Файл создан");
            }
        }

        public static void ReadingJson()
        {
            string assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string jsonFolder = Path.Combine(assemblyFolder, "NWCBatchExport");
            string pathToJson = Path.Combine(jsonFolder, "Data.json");

            if (File.Exists(pathToJson))
            {
                //var aaa = File.ReadAllText(pathToJson);
                //TaskDialog.Show("aaa", aaa);
            }

            else
            {
                CreateJson(jsonFolder);
            }
        }
    }
}
