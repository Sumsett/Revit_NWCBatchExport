using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;

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

            _SettingsAndOpeningFile.Body();

            textBox4.Text = _Data.Log;





        }

        private void button2_Click(object sender, EventArgs e)
        {
            Autodesk.Revit.DB.Document document = _Data.ExternalCommandData.Application.ActiveUIDocument?.Document;

            WorksetTable worksetTable = document.GetWorksetTable();
            WorksetId activeId = worksetTable.GetActiveWorksetId();

            var aaaa = worksetTable.GetWorkset(activeId).Kind;
            //var aaaa = worksetTable.GetWorkset(activeId).;

            // get the current visibility
            //WorksetVisibility visibility = view.GetWorksetVisibility(worksetId);

            TaskDialog.Show("aaaa", aaaa.ToString());
        }
    }
}
