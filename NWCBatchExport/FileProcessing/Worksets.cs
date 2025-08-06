using System;
using System.Collections.Generic;
using System.Linq;
using Autodesk.Revit.DB;
using NWCBatchExport.DataStorage;
using NWCBatchExport.Events;

namespace NWCBatchExport.FileProcessing;

internal class Worksets
{
    static internal void EnableAll(Document doc)
    {
        // Имя вида, который мы ищем
        string viewName = Data.NameOfExportedView;

        // Находим вид по имени
        View targetView = new FilteredElementCollector(doc)
            .OfClass(typeof(View))
            .Cast<View>()
            .FirstOrDefault(v => v.Name.Equals(viewName, StringComparison.OrdinalIgnoreCase));

        //Даункастим до 3д вида
        View3D view3D = (View3D)targetView;


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

                //Отключаем подрезку 3д вида
                if (view3D.IsSectionBoxActive == true)
                {
                    view3D.IsSectionBoxActive = false;
                    Logger.Log(doc.Title, "Включена подрезка 3D вида");
                }

                trans.Commit();
            }
        }
    }
}
