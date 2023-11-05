using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "StageSettings", menuName = "StageSettings")]
public class StageSettings : ScriptableObject
{
    public bool restArea = false;
    public List<string> enemiesInOrder = new List<string> {"man", "cart"};
    public List<string> allowedEnemies = new List<string>() {"pitcher"};
    readonly List<string> enemies = new List<string>() {"pitcher", "man", "cart"};
    public int numberOfSpawns = 0;
    public int hordes = 0;
    public int maxEnLimit = 0;
    public int spawnCoolDown = 0;
    public string Boss;
    public Dictionary<string, int> mod = new Dictionary<string, int>()
    {
        {"numOfSpawns", 1},
        {"hordes", 1},
        {"maxEnLimit", 1},
    };

    public void Reset()
    {
        enemiesInOrder =  new List<string>(enemies);
        enemiesInOrder.RemoveAt(0);
        allowedEnemies = new List<string>() {enemies[0]};
        for(int i = 0; i < mod.Count; i++)
        {
            List<string> keys = new List<string>(mod.Keys);
            mod[keys[i]] = 1;
        }
    }

    public void LevelUp()
    {
        if(enemiesInOrder.Count != 0){
            allowedEnemies.Add(enemiesInOrder[0]);
            enemiesInOrder.RemoveAt(0);
        }

        for(int i = 0; i < mod.Count; i++)
        {
            List<string> keys = new List<string>(mod.Keys);
            mod[keys[i]] += Random.Range(1, 3);
        }
    }
}
