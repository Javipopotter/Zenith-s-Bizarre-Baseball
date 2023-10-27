using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    static GameObject spawn;
    string[] enemies = new string[] {"pitcher", "man", "cart"};
    float spCoolDown = 2;
    [SerializeField] float levelCoolDown = 3;
    [SerializeField] int numberOfSpawns = 1;
    [SerializeField] int hordes;
    public int _spawnLevel = 0;
    int maxEnLimit;
    public int enemyCount = 0;
    [SerializeField] int restrictedEnemies = 0;
    GameObject limits;
    public static Spawner sp;
    CinematicPlayerController cine;
    [SerializeField] ClausBoss clausBoss;
    [SerializeField]TextMeshProUGUI EnemyCounter;
    int EnMax = 1;
    int _KillCount = 0;
    public int KillCount
    {
        get{return _KillCount;}
        set
        {
            _KillCount = value;
            EnemyCounter.text = "Defeated Enemies " + KillCount + "/" + EnMax;
            EnemyCounter.color = Color.Lerp(Color.white, Color.yellow, (float)_KillCount/(float)EnMax);
            GameManager.GM.EnemyCounterUpdate();
        }
    }
    
    
    private void Awake() {
        sp = this;
        limits = GameObject.Find("limits");
        cine = GameObject.Find("Player").GetComponent<CinematicPlayerController>();
    }

    private void Start() {
        spawnLevel = _spawnLevel;
    }

    // Update is called once per frame
    void Update()
    {
        if(spCoolDown <= 0 && hordes > 0 && enemyCount < maxEnLimit && spawnLevel < 2 && !DialoguesManager.dialoguesManager.cinematic)
        {
            hordes--;
            spCoolDown = levelCoolDown;
            for(int i = 0; i < numberOfSpawns; i++)
            {
                enemyCount++;
                var en = GameManager.GM.GetObject(enemies[Random.Range(0, enemies.Length - restrictedEnemies)]);
                en.transform.position = spawn.transform.GetChild(Random.Range(0, spawn.transform.childCount)).transform.position;
            }
        }
        else
        {
            spCoolDown -= Time.deltaTime;
        }

        if (KillCount >= EnMax && EnMax != 0 || spawnLevel == -1){
            GameManager.GM.NextStageText();
            foreach(Collider2D col in limits.GetComponents<Collider2D>())
            {
                col.isTrigger = true;
            }
        }
    }

    public void PlayHorde()
    {
        GameManager.GM.StartLevel();
        spCoolDown = 3f;
        spawn = GameObject.Find("Spawners");
        GameManager.GM.BossLifeBarIsActive(false);
        switch (spawnLevel)
        {
            case -1:
                GameManager.GM.SetScenario(0);
                DialoguesManager.dialoguesManager.ExecuteDialog(Dialogues.dialogues.texts["InitialCutscene"]);
                hordes = 0;
                numberOfSpawns = 0;
                break;
            case 0:
                restrictedEnemies = 2;
                numberOfSpawns = 2;
                levelCoolDown = 3;
                hordes = 10;
                maxEnLimit = 6;
                break;
            case 1:
                restrictedEnemies = 0;
                numberOfSpawns = 4;
                levelCoolDown = 4;
                hordes = 5;
                maxEnLimit = 8;
                break;
            case 2:
                GameManager.GM.SetScenario(1);
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
                break;
        }

        KillCount = 0;
        EnMax = numberOfSpawns * hordes;
    }

    public int spawnLevel
    {
        get
        {
            return _spawnLevel;
        }

        set
        {
            _spawnLevel = value;
            PlayHorde();
        }
    }
}