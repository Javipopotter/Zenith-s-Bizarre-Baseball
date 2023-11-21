using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "StageSettings", menuName = "StageSettings")]
public class StageSettings : ScriptableObject
{
    public bool restArea = false;
    public List<string> allowedEnemies = new List<string>() {"pitcher"};

    // ENEMIES ARRAYS
    readonly string[] CITY_ENEMIES = {"pitcher", "man", "cart"};
    readonly string[] SUBWAY_ENEMIES = {"skater"};
    readonly string[] PARK_ENEMIES = {};
    readonly string[] VEGAN_ENEMIES = {};
    readonly string[] DEATH_ENEMIES = {};
    readonly string[] BANK_ENEMIES = {};

    // SPAWN SETTINGS
    public int numberOfSpawns = 0;
    public int hordes = 0;
    public int maxEnLimit = 0;
    public int spawnCoolDown = 0;

    public string Boss;
    public string musicalTheme;
    public float cameraSize;

    float timesAppeared = 0;

    public Dictionary<string, int> mod = new Dictionary<string, int>()
    {
        {"numOfSpawns", 1},
        {"hordes", 1},
        {"maxEnLimit", 1},
    };

    public void OnStart() => timesAppeared = 0;

    public void OnStageClear()
    {
        timesAppeared ++;
        for(int i = 0; i < mod.Count; i++)
        {
            List<string> keys = new List<string>(mod.Keys);
            mod[keys[i]] = Random.Range(1, 4);
        }

        switch(timesAppeared)
        {
            case 1:
                allowedEnemies.Add(CITY_ENEMIES[1]);
                break;

            case 4:
                allowedEnemies.Add(CITY_ENEMIES[2]);
                break;

            case 12:
                allowedEnemies.Add(SUBWAY_ENEMIES[1]);
                break;

            case 16:
                allowedEnemies.Add(SUBWAY_ENEMIES[2]);
                break;

            case 24:
                allowedEnemies.Add(PARK_ENEMIES[1]);
                break;

            case 28:
                allowedEnemies.Add(PARK_ENEMIES[2]);
                break;
        }
    }

    public void Reset() 
    {
        allowedEnemies = new List<string>() {"pitcher"};
    }

    public void IsIndebted(bool value)
    {
        if(value){
            allowedEnemies.AddRange(BANK_ENEMIES);
        }else{
            allowedEnemies.RemoveRange(BANK_ENEMIES.Length, allowedEnemies.Count - BANK_ENEMIES.Length);
        }
    }

    public void OnGoHunter()
    {
        allowedEnemies.AddRange(DEATH_ENEMIES);
    }

    public void OnVeganBetrayal()
    {
        allowedEnemies.AddRange(VEGAN_ENEMIES);
    }
}
