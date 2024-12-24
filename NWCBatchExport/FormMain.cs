using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using RevitFormTest;
using System;
using System.Collections.Generic;
using System.IO;

namespace NWCBatchExport
{
    public partial class FormMain : System.Windows.Forms.Form
    {
        UIApplication uiapplocal;
        Document doclocal;

        public FormMain(UIApplication application)
        {
            InitializeComponent();


            
        }

        private void button1_Click(object sender, EventArgs e)
        {


        }

        private void button2_Click(object sender, EventArgs e)
        {

            string Path = @"C:\Users\E.Popov\Desktop\qqqq\wwwww\Проект1.rvt";
            uiapplocal.OpenAndActivateDocument(Path);

            StartClass aaa = new StartClass();
        
            label1.Text = "aaa";
            //Export();

        }


        void Export()
        {

            FilteredElementCollector collector = new FilteredElementCollector(doclocal);
            ICollection<Element> views = collector.OfClass(typeof(Autodesk.Revit.DB.View)).ToElements();


            ElementId selectedView = null;

            foreach (Element view in views)
            {
                if (view.Name == "Уровень 1")
                {
                    selectedView = view.Id;
                    break;
                }
            }

            //Настроить настройки экспорта
            NavisworksExportOptions options = new NavisworksExportOptions
            {
                ExportScope = NavisworksExportScope.View,
                ViewId = selectedView
            };

            label1.Text = doclocal.Title + " - " + selectedView;

            string path = @"C:\Users\E.Popov\Desktop\qqqq";
            doclocal.Export(path, doclocal.Title, options);

            TaskDialog.Show("Готово", "Файлы экспортированны");

        }
    }
}
