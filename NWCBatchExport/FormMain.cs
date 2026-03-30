using Autodesk.Revit.DB;
using NWCBatchExport.AdditionalFunctionality;
using NWCBatchExport.DataStorage;
using NWCBatchExport.Events;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace NWCBatchExport;

public partial class FormMain : System.Windows.Forms.Form
{
    public FormMain()
    {
        InitializeComponent();

        //Загрузка в данных в текстбоксы
        textBox1.Text = Data.NameOfExportedView;
        textBoxPathRVT.Text = Data.PathToRVT;
        textBoxPathNWC.Text = Data.PathToNWC;

        //Передача данных в обработчики событий
        Logger.textBoxForLog = richTextBox1;
        ExecutionStatus.Label = label_CurrentFile;
        ExecutionStatus.Button = button1;
        ExecutionStatus.ProgressBar = progressBar1;

        progressBar1.Visible = false; //Отключение прогресс бара

        Text += $" (Версия: {Assembly.GetExecutingAssembly().GetName().Version.ToString()})"; //Версия сборки в названии

#if TEST
        BackColor = System.Drawing.Color.Thistle;
        richTextBox1.Text += "ТЕСТОВАЯ ВЕРСИЯ\n";
#else
        groupBox2.Visible = false;
#endif
    }

    private void button1_Click(object sender, EventArgs e)
    {
        Data.NameOfExportedView = textBox1.Text;
        Data.PathToRVT = textBoxPathRVT.Text;
        //Data.PathToNWC = textBoxPathNWC.Text;

        if (checkBox3.Checked)
        {
            Data.PathToNWC = textBoxPathRVT.Text; //Сохраняет в папку с исходным файлом с заменой
        }
        else
        {
            Data.PathToNWC = textBoxPathNWC.Text; //Сохраняет в указанную папку
        }

        Data.UnloadingRoomGeometry = checkBox1.Checked;
        Data.DisablingTrims3DView = checkBox2.Checked;
        Data.ShowRevitWarnings = checkBox4.Checked;

        progressBar1.Visible = true;

        if (radioButton1.Checked)
        {
            Data.EventExportNWC.Raise();
        }

        if (radioButton2.Checked)
        {
            Data.RemovingLinks.Raise();
        }
    }

    #region Выбор папки в проводнике
    private void button_openRvtFolder_Click(object sender, EventArgs e)
    {
        folderBrowserDialog1.SelectedPath = "";
        folderBrowserDialog1.SelectedPath = textBoxPathRVT.Text;

        if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
        {
            textBoxPathRVT.Text = folderBrowserDialog1.SelectedPath;
        }
    }

    private void Button_openNwcFolder_Click(object sender, EventArgs e)
    {
        folderBrowserDialog1.SelectedPath = "";
        folderBrowserDialog1.SelectedPath = textBoxPathNWC.Text;

        if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
        {
            textBoxPathNWC.Text = folderBrowserDialog1.SelectedPath;
        }
    }
    #endregion

    #region Кнопки из настроек
    //Сохранение Json
    private void Button_savedJson_Click(object sender, EventArgs e)
    {
        SavedJson aaa = new SavedJson
        {
            NameOfExportedView = textBox1.Text,
            PathToNWC = textBoxPathNWC.Text,
            PathToRVT = textBoxPathRVT.Text
        };

        Json.WriteJson(aaa);
    }

    //Загрузка Json
    private void button_loadJson_Click(object sender, EventArgs e)
    {
        Json.ReadingJson();

        textBox1.Text = Data.NameOfExportedView;
        textBoxPathRVT.Text = Data.PathToRVT;
        textBoxPathNWC.Text = Data.PathToNWC;
    }

    //Сохранение лога в файл
    private void button_SaveLogs_Click(object sender, EventArgs e)
    {
        //Запись лога в файл
        folderBrowserDialog1.SelectedPath = "";
        folderBrowserDialog1.SelectedPath = textBoxPathNWC.Text;

        if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
        {
            var pathToFolder = folderBrowserDialog1.SelectedPath;
            var pathToFile = System.IO.Path.Combine(pathToFolder, $"Log.txt");

            try
            {
                File.WriteAllText(pathToFile, richTextBox1.Text);
                MessageBox.Show("Файл сохранен");
            }
            catch
            {
                MessageBox.Show("Не удается записать файл лога");
            }
        }
    }

    //Тестовые функции
    //Открытие папки с логом
    private void Button_OpenLogFile_Click(object sender, EventArgs e)
    {
        string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        var fullPath = System.IO.Path.Combine(documentsPath, "log.txt");
        Process.Start("explorer.exe", $"/select,\"{fullPath}\"");
    }

    //Разное для отладки
    private void Button_Tests_Click(object sender, EventArgs e)
    {       
        //Data.PathToRVT = textBoxPathRVT.Text;
        //Data.Tests.Raise();
    }
    #endregion

    private void checkBox3_CheckedChanged(object sender, EventArgs e)
    {
        if (checkBox3.Checked)
        {
            textBoxPathNWC.Enabled = false;
            Button_openNwcFolder.Enabled = false;
        }

        else
        {
            textBoxPathNWC.Enabled = true;
            Button_openNwcFolder.Enabled = true;
        }
    }

}
