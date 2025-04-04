using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using NWCBatchExport.Сторонние_решения;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Forms;
using View = Autodesk.Revit.DB.View;

namespace NWCBatchExport
{
    public partial class FormMain : System.Windows.Forms.Form
    {
        public FormMain()
        {
            InitializeComponent();

            textBox1.Text = _Data.NameOfExportedView;
            //label1.Text = _Data.NameOfExportedView;
            textBoxPathRvt.Text = _Data.PathToRVT;
            textBoxPathNWC.Text = _Data.PathToNWC;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _Data.NameOfExportedView = textBox1.Text;
            _Data.PathToNWC = textBoxPathNWC.Text;
            _Data.PathToRVT = textBoxPathRvt.Text;
            _Data.UnloadingRoomGeometry = checkBox1.Checked;

            _SettingsAndOpeningFile.ExportNWC();

            textBox4.Text = _Data.Log;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Document doc = _Data.ExternalCommandData.Application.ActiveUIDocument?.Document;

            ICollection<Element> walls = new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_Walls).WhereElementIsNotElementType().ToElements();
            ICollection<Element> floors = new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_Floors).WhereElementIsNotElementType().ToElements();
            ICollection<Element> structuralFoundation = new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_StructuralFoundation).WhereElementIsNotElementType().ToElements();
            ICollection<Element> columns = new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_Columns).WhereElementIsNotElementType().ToElements();
            ICollection<Element> structuralColumns = new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_StructuralColumns).WhereElementIsNotElementType().ToElements();
            ICollection<Element> structuralFraming = new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_StructuralFraming).WhereElementIsNotElementType().ToElements();

            using (Transaction trans = new Transaction(doc, "Update Workset Parameter"))
            {
                trans.Start();

                Class112.AAA(doc, walls);
                Class112.AAA(doc, floors);
                Class112.AAA(doc, structuralFoundation);
                Class112.AAA(doc, columns);
                Class112.AAA(doc, structuralColumns);
                Class112.AAA(doc, structuralFraming);


                trans.Commit();
            }


        }

        private void button_RemovingLinks_Click(object sender, EventArgs e)
        {
            _Data.PathToNWC = textBoxPathNWC.Text;
            _Data.PathToRVT = textBoxPathRvt.Text;
            _Data.NameOfExportedView = textBox1.Text;


            _SettingsAndOpeningFile.RemovingAllLinks();

            textBox4.Text = _Data.Log;
        }
    }
}
