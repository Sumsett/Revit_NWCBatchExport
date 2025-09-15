using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace NWCBatchExport.FileProcessing;

internal class OpenFile
{
    static internal void OpenFileAsUsual(string dir, ExternalCommandData commandData)
    {
        //Настройки для открытия
        OpenOptions openOptions = new OpenOptions();
        openOptions.DetachFromCentralOption = DetachFromCentralOption.DetachAndPreserveWorksets;
        openOptions.OpenForeignOption = OpenForeignOption.Open;

        //Открытие файла
        FilePath filePath = new FilePath(dir);
        commandData.Application.OpenAndActivateDocument(filePath, openOptions, true);
    }

    static internal void OpenFileWithoutShowing(string dir, ExternalCommandData commandData)
    {
        //Закрываем все рабочие наборы
        WorksetConfiguration openConfig = new WorksetConfiguration(WorksetConfigurationOption.CloseAllWorksets);

        //Настройки для открытия
        OpenOptions openOptions = new OpenOptions();

        openOptions.DetachFromCentralOption = DetachFromCentralOption.DetachAndPreserveWorksets; //Отсоединение от центральной модели        
        openOptions.SetOpenWorksetsConfiguration(openConfig); //Указываем конфигурацию рабочих наборов

        //Открытие файла без показа пользователю
        FilePath filePath = new FilePath(dir);
        commandData.Application.Application.OpenDocumentFile(filePath, openOptions);
    }
}