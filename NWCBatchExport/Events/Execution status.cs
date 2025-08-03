using System.Windows.Forms;

namespace NWCBatchExport.Events
{
    public delegate void FileBeingProcessed(string fileName);
    public delegate void ButtonsActive(bool status);
    public delegate void ProgressBarTotal(int number);
    public delegate void ProgressBarProcessed(int number);

    internal class ExecutionStatus
    {
        //Ссылки для вывода информации
        public static Label Label { private get; set; }
        public static Button Button { private get; set; }
        public static ProgressBar ProgressBar { private get; set; }



        //События для обновления информации на интерфейсе
        public static event FileBeingProcessed EventFileBeingProcessed;
        public static event ButtonsActive EventButtonsActive;
        public static event ProgressBarTotal EventProgressBarTotal;
        public static event ProgressBarProcessed EventProgressBarProcessed;

        //Передача сообщений в обработчик 
        public static void FileName(string fileName)
        {
            EventFileBeingProcessed?.Invoke(fileName);
        }
        public static void ButtonsActive(bool status)
        {
            EventButtonsActive?.Invoke(status);
        }
        public static void ProgressBarTotal(int number)
        {
            EventProgressBarTotal?.Invoke(number);
        }
        public static void ProgressBarProcessed(int number)
        {
            EventProgressBarProcessed?.Invoke(number);
        }

        //Метод для подписки на событие
        public static void OutFileName(string fileName)
        {
            Label.Text = fileName;
        }
        public static void OutButtonsActive(bool status)
        {
            Button.Enabled = status;
        }
        public static void OutProgressBarTotal(int number)
        {
            ProgressBar.Maximum = number;
        }
        public static void OutProgressBarProcessed(int number)
        {
            ProgressBar.PerformStep();
            if (ProgressBar.Value == ProgressBar.Maximum)
                ProgressBar.Value = 0;
        }
    }
}
