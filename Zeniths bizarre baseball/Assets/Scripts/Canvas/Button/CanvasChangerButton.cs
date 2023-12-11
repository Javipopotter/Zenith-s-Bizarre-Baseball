using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ButtonType
{
    ActivateCanvas, DeactivateCanvas, SwitchCanvas
}

public class CanvasChangerButton : ButtonControllerParent
{
    public CanvasType type;
    public ButtonType buttonType;
    [HideInInspector] public CanvasManager canvasManager;

    public override void Awake() {
        canvasManager = CanvasManager.canvas;
    }
    
    public override void OnButtonClicked()
    {
        base.OnButtonClicked();
        CanvasAction(type);
    }

    public void CanvasAction(CanvasType canvasType)
    {
        switch(buttonType)
        {
            case ButtonType.ActivateCanvas:
                canvasManager.ActivateCanvas(canvasType);
                break;
            
            case ButtonType.DeactivateCanvas:
                canvasManager.DeactivateCanvas(canvasType);
                break;

            case ButtonType.SwitchCanvas:
                canvasManager.SwitchCanvas(canvasType);
                break;
        }
    }
}
