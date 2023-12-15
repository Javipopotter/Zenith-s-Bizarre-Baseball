using UnityEngine;
using UnityEngine.Events;

public class StagesManager : MonoBehaviour
{
    Stage currentStage;
    public UnityEvent<Stage> OnStageSet;
    public static StagesManager stagesManager;

    public void SetStage(Stage stage)
    {
        if(currentStage != null)
        { 
            currentStage.gameObject.SetActive(false);

            if(stage.settings.musicalTheme != currentStage.settings.musicalTheme)
            {
                AudioManager.instance.Stop(currentStage.settings.musicalTheme);
                AudioManager.instance.Play(stage.settings.musicalTheme);
            }
        }
        else
        {
            AudioManager.instance.Play(stage.settings.musicalTheme);
        }

        stage.gameObject.SetActive(true);
        currentStage = stage;

        OnStageSet?.Invoke(currentStage);

        // StartLevel();
    }

    public void OpenGates()
    {
        currentStage.OpenGates();
    }

    void OnStateChange(GameStates state)
    {
        if(state == GameStates.gameOver)
        {
            AudioManager.instance.Stop(currentStage.settings.musicalTheme);
        }
    }

    private void Awake() {

        if(stagesManager == null)
        {
            stagesManager = this;
        }
        else
        {
            Destroy(gameObject);
        }

        if(TryGetComponent(out Spawner spawner))
        {
            spawner.OnStageCleared.AddListener(OpenGates);
        }
    }

    private void OnDestroy() {
        if(TryGetComponent(out Spawner spawner))
        {
            spawner.OnStageCleared.RemoveListener(OpenGates);
        }
    }
}
