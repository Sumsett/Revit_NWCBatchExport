namespace NWCBatchExport.Events;

internal class UnsubscribeToEvents
{
    internal static void CurrentForm()
    {
        //Логирование
        Logger.EventLoggingToFile -= Logger.OutLogger;

        //Изменение в интерфейсе
        ExecutionStatus.EventFileBeingProcessed -= ExecutionStatus.OutFileName;
        ExecutionStatus.EventButtonsActive -= ExecutionStatus.OutButtonsActive;
        ExecutionStatus.EventProgressBarTotal -= ExecutionStatus.OutProgressBarTotal;
        ExecutionStatus.EventProgressBarProcessed -= ExecutionStatus.OutProgressBarProcessed;
    }
}
