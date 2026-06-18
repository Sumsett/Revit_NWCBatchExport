using Autodesk.Revit.UI;
using ITEM_BatchExport.Events;

namespace ITEM_BatchExport.RevitEvents;

public class ExternalUnsubscribeEvents : IExternalEventHandler
{
    public void Execute(UIApplication app)
    {
        app.DialogBoxShowing -= RevitEventHandler.ApplicationDocumentOpened;
    }

    public string GetName()
    {
        return "Отписка от событий";
    }
}