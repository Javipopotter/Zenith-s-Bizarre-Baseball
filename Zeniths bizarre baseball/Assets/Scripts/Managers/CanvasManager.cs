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

    private void Awake() {

        if(canvas == null) {canvas = this;} else {Destroy(gameObject);}

        canvasControllerList = GetComponentsInChildren<CanvasController>().ToList();
        DeactivateAllCanvases();
    }

    private void Start() {
        if(GameManager.GM != null) 
        {
            GameManager.GM.OnStateEnter.AddListener(OnStateEnter);
            GameManager.GM.OnStateExit.AddListener(OnStateExit);
        }
    }

    void ActivateCanvas(CanvasType desiredType)
    { 
        canvasControllerList.Find(canvas => canvas.type == desiredType).gameObject.SetActive(true);
    }

    void DeactivateCanvas(CanvasType desiredType)
    {
        canvasControllerList.Find(canvas => canvas.type == desiredType).gameObject.SetActive(false);
    }

    void DeactivateAllCanvases()
    {
        foreach(CanvasController canvasController in canvasControllerList)
        {
            if(canvasController.type != CanvasType.GameUI)
            {
                canvasController.gameObject.SetActive(false);
            }
        }
    }


    void OnStateExit(GameStates state)
    {
        switch(state)
        {
            case GameStates.paused:
                DeactivateCanvas(CanvasType.Menu);
                break;
            case GameStates.gameOver:
                DeactivateCanvas(CanvasType.GameOver);
                break;
            case GameStates.talking:
                DeactivateCanvas(CanvasType.Dialogue);
                break;
            case GameStates.shoping:
                DeactivateCanvas(CanvasType.Dialogue);
                break;
            case GameStates.notificated:
                DeactivateCanvas(CanvasType.Notification);
                break;
            case GameStates.upgrading:
                DeactivateCanvas(CanvasType.Upgrader);
                break;
        }
    }
    void OnStateEnter(GameStates state)
    {
        switch(state)
        {
            case GameStates.paused:
                ActivateCanvas(CanvasType.Menu);
                break;
            case GameStates.gameOver:
                ActivateCanvas(CanvasType.GameOver);
                break;
            case GameStates.talking:
                ActivateCanvas(CanvasType.Dialogue);
                break;
            case GameStates.shoping:
                ActivateCanvas(CanvasType.Dialogue);
                break;
            case GameStates.notificated:
                ActivateCanvas(CanvasType.Notification);
                break;
            case GameStates.upgrading:
                ActivateCanvas(CanvasType.Upgrader);
                break;
        }
    }

    private void OnDestroy() {
        if(GameManager.GM != null)
        {
            GameManager.GM.OnStateEnter.RemoveListener(OnStateEnter);
            GameManager.GM.OnStateExit.AddListener(OnStateExit);
        }
    }
}
