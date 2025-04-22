using System;
using System.Collections.Generic;
using System.Linq;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace NWCBatchExport
{
    internal class Class1
    {
        static public void AAAA()
        {
            // Получаем доступ к текущему документу и приложению
            //UIApplication uiApp = commandData.Application;
            //UIDocument uiDoc = uiApp.ActiveUIDocument;
            //Document doc = uiDoc.Document;

            Document doc = _Data.ExternalCommandData.Application.ActiveUIDocument?.Document;


            // Имя вида, который мы ищем
            string targetViewName = _Data.NameOfExportedView;

            // Находим вид по имени
            View targetView = new FilteredElementCollector(doc)
                .OfClass(typeof(View))
                .Cast<View>()
                .FirstOrDefault(v => v.Name.Equals(targetViewName, StringComparison.OrdinalIgnoreCase));

            //// Проверяем, найден ли вид
            //if (targetView == null)
            //{
            //    TaskDialog.Show("Успех", $"Вид с именем '{targetViewName}' не найден.");
            //}

            // Получаем все рабочие наборы в документе
            IList<Workset> worksets = new FilteredWorksetCollector(doc)
                .OfKind(WorksetKind.UserWorkset)
                .ToWorksets()
                .ToList();


            if (targetView != null)
            {
                // Начинаем транзакцию для изменения видимости
                using (Transaction trans = new Transaction(doc, "Включение всех рабочих наборов"))
                {
                    trans.Start();

                    // Включаем видимость всех рабочих наборов
                    foreach (Workset workset in worksets)
                    {
                        targetView.SetWorksetVisibility(workset.Id, WorksetVisibility.Visible);
                    }

                    trans.Commit();
                }
            }

            //TaskDialog.Show("Успех", $"Видимость всех рабочих наборов включена для вида '{targetViewName}'.");

        }
    }
}
