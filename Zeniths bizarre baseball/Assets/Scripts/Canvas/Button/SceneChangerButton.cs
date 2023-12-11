using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChangerButton : ButtonControllerParent
{
    ScenesManager scenesManager;
    [SerializeField] string selectedScene;
    [SerializeField] SceneAction action; 

    public enum SceneAction
    {
        LoadScene,
        UnloadScene,
        ReloadScene
    }

    private void Start() {
        base.Awake();
        scenesManager = ScenesManager.instance;
    }

    public override void OnButtonClicked()
    {
        base.OnButtonClicked();
        Action();
    }

    void Action()
    {
        switch(action)
        {
            case SceneAction.LoadScene:
                scenesManager.LoadScene(selectedScene);
                break;

            case SceneAction.UnloadScene:
                scenesManager.UnloadScene(selectedScene);
                break;

            case SceneAction.ReloadScene:
                scenesManager.Reload();
                break;
        }
    }
}
