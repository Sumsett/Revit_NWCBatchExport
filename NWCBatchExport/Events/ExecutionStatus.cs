using System.Windows.Forms;

namespace NWCBatchExport.Events;

internal delegate void FileBeingProcessed(string fileName);
internal delegate void ButtonsActive(bool status);
internal delegate void ProgressBarTotal(int number);
internal delegate void ProgressBarProcessed(int number);

internal class ExecutionStatus
{
    //Ссылки для вывода информации
    internal static Label Label { private get; set; }
    internal static Button Button { private get; set; }
    internal static ProgressBar ProgressBar { private get; set; }



    //События для обновления информации на интерфейсе
    internal static event FileBeingProcessed EventFileBeingProcessed;
    internal static event ButtonsActive EventButtonsActive;
    internal static event ProgressBarTotal EventProgressBarTotal;
    internal static event ProgressBarProcessed EventProgressBarProcessed;

    //Передача сообщений в обработчик 
    internal static void FileName(string fileName)
    {
        EventFileBeingProcessed?.Invoke(fileName);
    }
    internal static void ButtonsActive(bool status)
    {
        EventButtonsActive?.Invoke(status);
    }
    internal static void ProgressBarTotal(int number)
    {
        EventProgressBarTotal?.Invoke(number);
    }
    internal static void ProgressBarProcessed(int number)
    {
        EventProgressBarProcessed?.Invoke(number);
    }

    //Метод для подписки на событие
    internal static void OutFileName(string fileName)
    {
        Label.Text = fileName;
    }
    internal static void OutButtonsActive(bool status)
    {
        Button.Enabled = status;
    }
    internal static void OutProgressBarTotal(int number)
    {
        ProgressBar.Maximum = number;
    }
    internal static void OutProgressBarProcessed(int number)
    {
        ProgressBar.PerformStep();
        if (ProgressBar.Value == ProgressBar.Maximum)
        {
            ProgressBar.Value = 0;
            ProgressBar.Visible = false;
        }
    }
}
