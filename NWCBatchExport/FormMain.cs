using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
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


        private void button_RemovingLinks_Click(object sender, EventArgs e)
        {
            _Data.PathToNWC = textBoxPathNWC.Text;
            _Data.PathToRVT = textBoxPathRvt.Text;
            _Data.NameOfExportedView = textBox1.Text;


            _SettingsAndOpeningFile.RemovingAllLinks();

            textBox4.Text = _Data.Log;
        }

        private void button_openRvtFolder_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBoxPathRvt.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void button_openNwcFolder_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBoxPathNWC.Text = folderBrowserDialog1.SelectedPath;
            }
        }
    }
}
