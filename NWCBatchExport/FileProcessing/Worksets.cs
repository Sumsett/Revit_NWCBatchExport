using System;
using System.Collections.Generic;
using System.Linq;
using Autodesk.Revit.DB;
using ITEM_BatchExport.DataStorage;
using ITEM_BatchExport.Events;

namespace ITEM_BatchExport.FileProcessing;

internal class Worksets
{
    static internal void EnableAll(Document doc)
    {
        // Имя вида, который мы ищем
        string viewName = Data.NameOfExportedView;

        //Ищем 3D вид с нужными именем.
        var view3D = new FilteredElementCollector(doc).OfClass(typeof(View3D))
            .Cast<View3D>()
            .Where(x => !x.IsTemplate)
            .FirstOrDefault(x => x.Name.Equals(viewName, StringComparison.OrdinalIgnoreCase));

        // Получаем все рабочие наборы в документе
        IList<Workset> worksets = new FilteredWorksetCollector(doc).OfKind(WorksetKind.UserWorkset)
            .ToWorksets()
            .ToList();

        if (view3D != null)
        {
            // Начинаем транзакцию для изменения видимости
            using (Transaction trans = new Transaction(doc, "Включение всех рабочих наборов"))
            {
                trans.Start();

                // Включаем видимость всех рабочих наборов
                foreach (Workset workset in worksets)
                {
                    view3D.SetWorksetVisibility(workset.Id, WorksetVisibility.Visible);
                }

                //Отключаем подрезку 3д вида
                if (view3D.IsSectionBoxActive == true)
                    Logger.Log(doc.Title, "Включена подрезка 3D вида");
                
                if (view3D.IsSectionBoxActive == true && Data.DisablingTrims3DView == true)
                    view3D.IsSectionBoxActive = false;
                

                trans.Commit();
            }
        }
    }
}
