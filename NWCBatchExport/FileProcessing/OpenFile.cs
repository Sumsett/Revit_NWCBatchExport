using System.Collections.Generic;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace NWCBatchExport.FileProcessing;

internal class OpenFile
{
    internal static void OpenFileAsUsual(string dir, ExternalCommandData commandData)
    {
        //Настройки для открытия
        OpenOptions openOptions = new OpenOptions();
        openOptions.DetachFromCentralOption = DetachFromCentralOption.DetachAndPreserveWorksets;
        openOptions.OpenForeignOption = OpenForeignOption.Open;

        //Открытие файла
        FilePath filePath = new FilePath(dir);
        commandData.Application.OpenAndActivateDocument(filePath, openOptions, true);
    }

    internal static void OpenFileWithoutShowing(string dir, ExternalCommandData commandData)
    {
        ModelPath modelPath = new FilePath(dir);
        TransmissionData transmissionData = TransmissionData.ReadTransmissionData(modelPath);

        if (transmissionData != null)
        {
            ICollection<ElementId> externalReference = transmissionData.GetAllExternalFileReferenceIds(); //Получаем все связи в файле

            foreach (ElementId refId in externalReference)
            {
                ExternalFileReference extRef = transmissionData.GetLastSavedReferenceData(refId);

                if (extRef.ExternalFileReferenceType == ExternalFileReferenceType.RevitLink)
                {
                    transmissionData.SetDesiredReferenceData(refId, extRef.GetPath(), extRef.PathType, false);
                }
            }
            
            transmissionData.IsTransmitted = true;
            
            TransmissionData.WriteTransmissionData(modelPath, transmissionData);
        }
        
        else
        {
            Autodesk.Revit.UI.TaskDialog.Show("Выгрузка связей", "Не удалось");
        }
        
        commandData.Application.Application.OpenDocumentFile(dir);
    }
}