using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    static GameObject spawn;
    float spCoolDown = 1;
    public int enemyCount = 0;
    public static Spawner sp;
    CinematicPlayerController cine;
    [SerializeField]TextMeshProUGUI EnemyCounter;
    public StageSettings CurrentSettings;
    int EnMax = 1;
    int _KillCount = 0;
    bool stageTrigger = false;
    int hordes;
    public int KillCount
    {
        get{return _KillCount;}
        set
        {
            _KillCount = value;
            EnemyCounter.text = KillCount + " / " + EnMax;
            EnemyCounter.color = Color.Lerp(Color.white, Color.yellow, (float)_KillCount/(float)EnMax);
            GameManager.GM.EnemyCounterUpdate();
        }
    }
    
    
    private void Awake() {
        sp = this;
        cine = GameObject.Find("Player").GetComponent<CinematicPlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(CurrentSettings.restArea || !GameManager.GM.gameElementsContainer.activeInHierarchy){return;}
        if(spCoolDown <= 0 && hordes > 0 && enemyCount < CurrentSettings.maxEnLimit)
        {
            hordes--;
            spCoolDown = CurrentSettings.spawnCoolDown;
            for(int i = 0; i < CurrentSettings.numberOfSpawns; i++)
            {
                enemyCount++;
                GameObject en = GameManager.GM.GetObject(CurrentSettings.allowedEnemies[Random.Range(0, CurrentSettings.allowedEnemies.Count)]);
                en.transform.position = spawn.transform.GetChild(Random.Range(0, spawn.transform.childCount)).transform.position;
            }
        }
        else
        {
            spCoolDown -= Time.deltaTime;
        }

        if(KillCount >= EnMax && EnMax != 0 && stageTrigger)
        {
            stageTrigger = false;
            GameManager.GM.OnStageCleared();
        }
    }

    public void PlayHorde()
    {
        stageTrigger = true;
        spCoolDown = 2f;
        hordes = CurrentSettings.hordes;
        spawn = GameManager.GM.currentStage.spawners;
        GameManager.GM.BossLifeBarIsActive(false);

        enemyCount = 0;
        KillCount = 0;
        EnMax = CurrentSettings.numberOfSpawns * CurrentSettings.hordes;

        //Boss Setup
        cine.dialog = CurrentSettings.Boss;
        GameManager.GM.SetProgressBar(CurrentSettings.Boss == "");
        GameManager.GM.BossLifeBarIsActive(CurrentSettings.Boss != "");
    }
}
