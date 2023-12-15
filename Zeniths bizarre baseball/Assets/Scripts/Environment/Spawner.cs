using UnityEngine;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{
    static GameObject spawn;
    float spCoolDown = 1;
    public int enemyCount = 0;
    public static Spawner sp;
    public StageSettings currentSettings;
    int EnMax = 1;
    int _killCount = 0;
    bool stageTrigger = false;
    int hordes;
    
    public int killCount
    {
        get{return _killCount;}
        set
        {
            _killCount = value;
            OnEnemyDefeated?.Invoke(value);
        }
    }

    public UnityEvent OnStageCleared;
    public UnityEvent<int> OnEnemyDefeated;
    
    private void Awake() {
        sp = this;

        if(TryGetComponent(out StagesManager stagesManager))
        {
            stagesManager.OnStageSet.AddListener(SetSpawnSettings);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(currentSettings == null) {return;}

        if(spCoolDown <= 0 && hordes > 0 && enemyCount < currentSettings.maxEnLimit)
        {
            hordes--;
            spCoolDown = currentSettings.spawnCoolDown;
            for(int i = 0; i < currentSettings.numberOfSpawns; i++)
            {
                enemyCount++;
                
                Vector2 desiredEnemyPos = spawn.transform.GetChild(Random.Range(0, spawn.transform.childCount)).transform.position;
                entity desiredEnemy = currentSettings.allowedEnemies[Random.Range(0, currentSettings.allowedEnemies.Count)];

                ObjectPooler.pooler.GetObject(desiredEnemy, desiredEnemyPos);
            }
        }
        else
        {
            spCoolDown -= Time.deltaTime;
        }

        if(killCount >= EnMax && EnMax != 0 && stageTrigger)
        {
            stageTrigger = false;
            OnStageCleared?.Invoke();
        }
    }

    public void SetSpawnSettings(Stage newStage)
    {
        currentSettings = newStage.settings;

        enabled = true;
        if(currentSettings.restArea){ enabled = false; }

        stageTrigger = true;
        spCoolDown = 2f;
        hordes = currentSettings.hordes;
        spawn = newStage.spawners;

        enemyCount = 0;
        killCount = 0;
        EnMax = currentSettings.numberOfSpawns * currentSettings.hordes;
    }

    void OnDestroy()
    {
        if(TryGetComponent(out StagesManager stagesManager))
        {
            stagesManager.OnStageSet.RemoveListener(SetSpawnSettings);
        }
    }
}
