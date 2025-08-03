using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using NWCBatchExport.DataStorage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace NWCBatchExport.FileProcessing
{
    [Transaction(TransactionMode.Manual)]
    public class RemoveLinksCommand 
    {
        /*
        public Result Execute(_ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            // Получаем приложение и документ
            UIApplication uiApp = commandData.Application;
            UIDocument uiDoc = uiApp.ActiveUIDocument;
            Document doc = uiDoc.Document;

            try
            {
                // Удаляем связи в отдельной транзакции
                using (Transaction trans = new Transaction(doc, "Remove Links"))
                {
                    trans.Start();

                    // Получаем все внешние ссылки (связи)
                    FilteredElementCollector collector = new FilteredElementCollector(doc);
                    ICollection<ElementId> linkTypeIds = collector
                        .OfClass(typeof(RevitLinkType))
                        .ToElementIds();

                    // Удаляем каждую связь (RevitLinkType)
                    foreach (ElementId linkTypeId in linkTypeIds)
                    {
                        RevitLinkType linkType = doc.GetElement(linkTypeId) as RevitLinkType;
                        if (linkType != null)
                        {
                            // Удаляем RevitLinkType, что также удаляет все связанные RevitLinkInstance
                            doc.Delete(linkTypeId);
                        }
                    }

                    // Завершаем транзакцию
                    trans.Commit();
                }

                // Проверяем, является ли файл центральной моделью
                bool isCentralModel = doc.IsWorkshared && !doc.IsDetached;

                // Сохраняем файл на рабочий стол
                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string fileName = isCentralModel ? "Central_Model.rvt" : "Local_Model.rvt";
                string localFilePath = Path.Combine(desktopPath, fileName);

                SaveAsOptions saveOptions = new SaveAsOptions
                {
                    OverwriteExistingFile = true
                };

                if (isCentralModel || doc.IsDetached)
                {
                    // Если файл является центральной моделью или был открыт в отключенном режиме, сохраняем его как центральную модель
                    WorksharingSaveAsOptions worksharingOptions = new WorksharingSaveAsOptions
                    {
                        SaveAsCentral = true // Сохраняем как центральную модель
                    };
                    saveOptions.SetWorksharingOptions(worksharingOptions);
                }

                // Сохраняем файл
                doc.SaveAs(localFilePath, saveOptions);

                // Показываем сообщение об успешном завершении
                string messageText = isCentralModel
                    ? $"Центральная модель сохранена на рабочий стол: {localFilePath}\n\n" +
                      "Теперь файл доступен для совместной работы через рабочие наборы."
                    : $"Локальный файл сохранен на рабочий стол: {localFilePath}";

                TaskDialog.Show("Успех", messageText);

                return Result.Succeeded;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                TaskDialog.Show("Ошибка", $"Произошла ошибка: {ex.Message}");
                return Result.Failed;
            }
        }
        */
        public static void AAA(Document document)
        {
            // Получаем приложение и документ

            Document doc = document;

            // Удаляем связи в отдельной транзакции
            using (Transaction trans = new Transaction(doc, "_Удаление связей"))
            {
                trans.Start();

                // Получаем все внешние ссылки (связи)
                FilteredElementCollector collector = new FilteredElementCollector(doc);
                ICollection<ElementId> linkTypeIds = collector
                    .OfClass(typeof(RevitLinkType))
                    .ToElementIds();

                // Удаляем каждую связь (RevitLinkType)
                foreach (ElementId linkTypeId in linkTypeIds)
                {
                    RevitLinkType linkType = doc.GetElement(linkTypeId) as RevitLinkType;
                    if (linkType != null)
                    {
                        // Удаляем RevitLinkType, что также удаляет все связанные RevitLinkInstance
                        doc.Delete(linkTypeId);
                    }
                }

                // Завершаем транзакцию
                trans.Commit();
            }

            // Проверяем, является ли файл центральной моделью
            bool isCentralModel = doc.IsWorkshared && !doc.IsDetached;

            // Сохраняем файл на рабочий стол
            string desktopPath = Data.PathToNWC;
            string fileName = document.Title + ".rvt";
            if (fileName.Contains("_отсоединено"))
                fileName = fileName.Replace("_отсоединено", "");

            string localFilePath = Path.Combine(desktopPath, fileName);

            SaveAsOptions saveOptions = new SaveAsOptions
            {
                OverwriteExistingFile = true
            };

            if (isCentralModel || doc.IsDetached)
            {
                // Если файл является центральной моделью или был открыт в отключенном режиме, сохраняем его как центральную модель
                WorksharingSaveAsOptions worksharingOptions = new WorksharingSaveAsOptions
                {
                    SaveAsCentral = true // Сохраняем как центральную модель
                };
                saveOptions.SetWorksharingOptions(worksharingOptions);
            }

            // Сохраняем файл
            doc.SaveAs(localFilePath, saveOptions);

            // Показываем сообщение об успешном завершении
            //string messageText = isCentralModel
            //    ? $"Центральная модель сохранена на рабочий стол: \n{localFilePath}\n\n" +
            //      "Теперь файл доступен для совместной работы через рабочие наборы."
            //    : $"Локальный файл сохранен на рабочий стол: \n{localFilePath}";

            //TaskDialog.Show("Успех", messageText);




        }

    }


}