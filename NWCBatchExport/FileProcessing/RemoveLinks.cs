using System.Collections.Generic;
using System.IO;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using NWCBatchExport.DataStorage;

namespace NWCBatchExport.FileProcessing
{
    [Transaction(TransactionMode.Manual)]
    public class RemoveLinks
    {
        public static void AllLinks(Document document)
        {
            // Удаляем связи в отдельной транзакции
            using (Transaction trans = new Transaction(document, "Удаление всех связей"))
            {
                trans.Start();

                // Получаем все внешние ссылки (связи)
                FilteredElementCollector collector = new FilteredElementCollector(document);
                ICollection<ElementId> linkTypeIds = collector
                    .OfClass(typeof(RevitLinkType))
                    .ToElementIds();

                // Удаляем каждую связь (RevitLinkType)
                foreach (ElementId linkTypeId in linkTypeIds)
                {
                    RevitLinkType linkType = document.GetElement(linkTypeId) as RevitLinkType;
                    if (linkType != null)
                    {
                        // Удаляем RevitLinkType, что также удаляет все связанные RevitLinkInstance
                        document.Delete(linkTypeId);
                    }
                }

                // Завершаем транзакцию
                trans.Commit();
            }

            // Проверяем, является ли файл центральной моделью
            bool isCentralModel = document.IsWorkshared && !document.IsDetached;

            // Сохраняем файл на рабочий стол
            string desktopPath = Data.PathToNWC;
            string fileName = document.Title + ".rvt";
            if (fileName.Contains("_отсоединено"))
                fileName = fileName.Replace("_отсоединено", "");

            string localFilePath = Path.Combine(desktopPath, fileName);

            SaveAsOptions saveOptions = new SaveAsOptions
            {
                OverwriteExistingFile = true //Разрешить перезапись файла с таким же именем
            };

            if (isCentralModel || document.IsDetached)
            {
                // Если файл является центральной моделью или был открыт в отключенном режиме, сохраняем его как центральную модель
                WorksharingSaveAsOptions worksharingOptions = new WorksharingSaveAsOptions
                {
                    SaveAsCentral = true // Сохраняем как центральную модель
                };
                saveOptions.SetWorksharingOptions(worksharingOptions);
            }

            // Сохраняем файл
            document.SaveAs(localFilePath, saveOptions);
        }
    }
}