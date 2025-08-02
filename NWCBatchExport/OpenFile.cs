using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace NWCBatchExport
{
    internal class OpenFile
    {
        static public void OpenFileAsUsual(string dir, ExternalCommandData commandData)
        {
            //Настройки для открытия
            OpenOptions openOptions = new OpenOptions();
            openOptions.DetachFromCentralOption = DetachFromCentralOption.DetachAndPreserveWorksets;
            openOptions.OpenForeignOption = OpenForeignOption.Open;

            //Открытие файла
            FilePath filePath = new FilePath(dir);
            commandData.Application.OpenAndActivateDocument(filePath, openOptions, true);
        }

        static public void OpenFileWithoutShowing(string dir, ExternalCommandData commandData)
        {
            //Настройки для открытия
            OpenOptions openOptions = new OpenOptions();
            openOptions.DetachFromCentralOption = DetachFromCentralOption.DetachAndPreserveWorksets; //Отсоединение от центральной модели

            //Открытие файла без показа пользователю
            FilePath filePath = new FilePath(dir);
            commandData.Application.Application.OpenDocumentFile(filePath, openOptions);
        }
    }
}
