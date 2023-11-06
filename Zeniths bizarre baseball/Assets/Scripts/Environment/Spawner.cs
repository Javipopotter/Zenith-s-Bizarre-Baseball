using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    static GameObject spawn;
    float spCoolDown = 1;
    public int enemyCount = 0;
    public static Spawner sp;
    CinematicPlayerController cine;
    [SerializeField] ClausBoss clausBoss;
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
            EnemyCounter.text = "Enemigos derrotados " + KillCount + "/" + EnMax;
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
        if(CurrentSettings.restArea){return;}
        if(spCoolDown <= 0 && hordes > 0 && enemyCount < CurrentSettings.maxEnLimit && !DialoguesManager.dialoguesManager.cinematic)
        {
            hordes--;
            spCoolDown = CurrentSettings.spawnCoolDown;
            for(int i = 0; i < CurrentSettings.numberOfSpawns; i++)
            {
                enemyCount++;
                var en = GameManager.GM.GetObject(CurrentSettings.allowedEnemies[Random.Range(0, CurrentSettings.allowedEnemies.Count)]);
                en.transform.position = spawn.transform.GetChild(Random.Range(0, spawn.transform.childCount)).transform.position;
            }
        }
        else
        {
            spCoolDown -= Time.deltaTime;
        }

        if (KillCount >= EnMax && EnMax != 0 && stageTrigger)
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
        if(CurrentSettings.Boss != "")
        {
            GameManager.GM.BossLifeBarIsActive(true);
                if(!clausBoss.appeared)
                {
                    clausBoss.appeared = true;
                    cine.dialog = "Claus";
                    clausBoss.gameObject.SetActive(true);
                }
                else
                {
                    cine.dialog = "";
                }
                clausBoss.GetComponent<ClausBoss>().Restart();
        }
        KillCount = 0;
        EnMax = CurrentSettings.numberOfSpawns * CurrentSettings.hordes;
    }
}
