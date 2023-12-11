using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public enum CanvasType
{
    GameOver,
    Menu,
    Notification,
    Shop,
    Dialogue,
    GameUI,
    Upgrader
}

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager canvas;
    List<CanvasController> canvasControllerList;
    CanvasController lastCanvasController;

    private void Awake() {

        if(canvas == null) {canvas = this;} else {Destroy(gameObject);}

        canvasControllerList = GetComponentsInChildren<CanvasController>().ToList();
        DeactivateAllCanvases();
    }

    public void SwitchCanvas(CanvasType desiredType)
    {
        if(lastCanvasController != null)
        {
            lastCanvasController.gameObject.SetActive(false);
        }

        CanvasController desiredCanvas = canvasControllerList.Find(canvas => canvas.type == desiredType);
        desiredCanvas.gameObject.SetActive(true);

        lastCanvasController = desiredCanvas;
    }

    public void ActivateCanvas(CanvasType desiredType)
    { 
        canvasControllerList.Find(canvas => canvas.type == desiredType).gameObject.SetActive(true);
    }

    public void DeactivateCanvas(CanvasType desiredType)
    {
        canvasControllerList.Find(canvas => canvas.type == desiredType).gameObject.SetActive(false);
    }

    public void DeactivateAllCanvases()
    {
        canvasControllerList.ForEach(canvas => canvas.gameObject.SetActive(false));
    }
}
