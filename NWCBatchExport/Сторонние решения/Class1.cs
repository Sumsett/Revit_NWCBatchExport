using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace NWCBatchExport.Сторонние_решения
{
    internal class Class112
    {
        public static void AAA(Document doc ,ICollection<Element> allElements)
        {
            foreach (Element element in allElements)
            {
                // Получаем рабочий набор элемента
                WorksetId worksetId = element.WorksetId;
                Workset workset = doc.GetWorksetTable().GetWorkset(worksetId);

                // Получаем параметр "_Рабочий набор"
                Parameter worksetParam = element.LookupParameter("NE_Рабочий набор");

                if (worksetParam != null && !worksetParam.IsReadOnly)
                {
                    // Устанавливаем значение параметра
                    worksetParam.Set(workset.Name);
                }
            }

        }
    }
}
