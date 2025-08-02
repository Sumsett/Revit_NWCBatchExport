using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;


namespace NWCBatchExport
{
    public partial class FormMain : System.Windows.Forms.Form
    {
        public FormMain()
        {
            InitializeComponent();

            textBox1.Text = _Data.NameOfExportedView;
            textBoxPathRVT.Text = _Data.PathToRVT;
            textBoxPathNWC.Text = _Data.PathToNWC;
            Logger.textBoxForLog = richTextBox1;

            label1.Text = "Версия: " + Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _Data.NameOfExportedView = textBox1.Text;
            _Data.PathToNWC = textBoxPathNWC.Text;
            _Data.PathToRVT = textBoxPathRVT.Text;
            richTextBox1.Text = null;

            _Data.UnloadingRoomGeometry = checkBox1.Checked;

            //Запуск таймера
            Stopwatch stopwatch = Stopwatch.StartNew();

            //Основной метод. Открытие файла, настройка вида, экспорт NWC
            _SettingsAndOpeningFile.ExportNWC();

            //Остановка таймера и логирование значения
            stopwatch.Stop();
            string time = stopwatch.Elapsed.ToString("mm\\:ss");
            Logger.Log("Все файлы", $"Общее время экспорта в NWC {time} (мин/сек)\n");
        }


        private void button_RemovingLinks_Click(object sender, EventArgs e)
        {
            _Data.PathToNWC = textBoxPathNWC.Text;
            _Data.PathToRVT = textBoxPathRVT.Text;
            _Data.NameOfExportedView = textBox1.Text;
            richTextBox1.Text = null;

            //Запуск таймера
            Stopwatch stopwatch = Stopwatch.StartNew();


            _SettingsAndOpeningFile.RemovingAllLinks();

            //Остановка таймера и логирование значения
            stopwatch.Stop();
            string time = stopwatch.Elapsed.ToString("hh\\:mm\\:ss");

            Logger.Log("Все файлы", $"Общее время удаления всех связей {time} (часы/мин/сек)\n");
        }

        private void button_openRvtFolder_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = "";
            folderBrowserDialog1.SelectedPath = textBoxPathRVT.Text;

            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBoxPathRVT.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void button_openNwcFolder_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = "";
            folderBrowserDialog1.SelectedPath = textBoxPathNWC.Text;

            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBoxPathNWC.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void button_savedJson_Click(object sender, EventArgs e)
        {
            _SavedJson aaa = new _SavedJson
            {
                NameOfExportedView = textBox1.Text,
                PathToNWC = textBoxPathNWC.Text,
                PathToRVT = textBoxPathRVT.Text
            };

            Json.WriteJson(aaa);
        }

        private void button_loadJson_Click(object sender, EventArgs e)
        {
            Json.ReadingJson();

            textBox1.Text = _Data.NameOfExportedView;
            textBoxPathRVT.Text = _Data.PathToRVT;
            textBoxPathNWC.Text = _Data.PathToNWC;
        }

        private void button_Test_Click(object sender, EventArgs e)
        {
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var fullPath = Path.Combine(documentsPath, "log.txt");
            Process.Start("explorer.exe", $"/select,\"{fullPath}\"");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Stopwatch stopwatchAll = Stopwatch.StartNew();
            string[] dirs = Directory.GetFiles(_Data.PathToRVT, "*.rvt");

            foreach (string dir in dirs)
            {
                //Запуск таймера
                Stopwatch stopwatch = Stopwatch.StartNew();

                //Открытие документа
                OpenFile.OpenFileWithoutShowing(dir, _Data.ExternalCommandData);

                UIApplication uiApp = _Data.ExternalCommandData.Application;
                DocumentSet documents = uiApp.Application.Documents;

                foreach (Autodesk.Revit.DB.Document doc in documents)
                {
                    Worksets.EnableAll(doc);
                    _Export.toNWC(doc);
                    doc.Close(false);
                }

                //Остановка таймера и логирование значения
                stopwatch.Stop();
                string time = stopwatch.Elapsed.ToString("mm\\:ss");
                string fileName = Path.GetFileNameWithoutExtension(dir);

                Logger.Log(fileName, $"Не явное открытие документа {time} (мин/сек)");
            }
            stopwatchAll.Stop();
            string timeAll = stopwatchAll.Elapsed.ToString("hh\\:mm\\:ss");

            Logger.Log("Все файлы", $"Неявное открытие файлов {timeAll} (часы/мин/сек)\n");
        }

        private void button_CheckingOpenDocuments_Click(object sender, EventArgs e)
        {
            button_CheckingOpenDocuments.Enabled = false;
            _Data.exEvent.Raise();


            /*
            UIApplication uiApp = _Data.ExternalCommandData.Application;
            DocumentSet documents = uiApp.Application.Documents;

            List<string> documentNames = new List<string>();
            foreach (Autodesk.Revit.DB.Document doc in documents)
            {
                documentNames.Add(doc.Title);
            }

            TaskDialog.Show("Открытые документы", string.Join("\n", documentNames));

            foreach (Autodesk.Revit.DB.Document doc in documents)
            {
                doc.Close(false);
            }
            */

        }
    }
}
