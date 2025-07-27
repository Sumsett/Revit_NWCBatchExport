using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using NWCBatchExport.Доп_классы_для_отладки;


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

            label1.Text = Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _Data.NameOfExportedView = textBox1.Text;
            _Data.PathToNWC = textBoxPathNWC.Text;
            _Data.PathToRVT = textBoxPathRVT.Text;

            _Data.UnloadingRoomGeometry = checkBox1.Checked;

            //Запуск таймера
            Stopwatch stopwatch = Stopwatch.StartNew();

            //Основной метод. Открытие файла, настройка вида, экспорт NWC
            _SettingsAndOpeningFile.ExportNWC();

            //Остановка таймера и логирование значения
            stopwatch.Stop();
            string time = stopwatch.Elapsed.ToString("mm\\:ss");
            Logger.Log("Все файлы", $"Общее время экспорта в NWC {time} (мин/сек)\n");


            richTextBox1.Text = _Data.Log;
        }


        private void button_RemovingLinks_Click(object sender, EventArgs e)
        {
            _Data.PathToNWC = textBoxPathNWC.Text;
            _Data.PathToRVT = textBoxPathRVT.Text;
            _Data.NameOfExportedView = textBox1.Text;

            //Запуск таймера
            Stopwatch stopwatch = Stopwatch.StartNew();


            _SettingsAndOpeningFile.RemovingAllLinks();

            //Остановка таймера и логирование значения
            stopwatch.Stop();
            string time = stopwatch.Elapsed.ToString("hh\\:mm\\:ss");

            //var sss = DateTime.Now.ToString("HH:mm:ss");
            Logger.Log("Все файлы", $"Общее время удаления всех связей {time} (часы/мин/сек)\n");

            richTextBox1.Text = _Data.Log;
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
            //Logger.RecordingDebugLog("ффффф");
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var fullPath = Path.Combine(documentsPath, "log.txt");
            Process.Start("explorer.exe", $"/select,\"{fullPath}\"");
        }
    }
}
