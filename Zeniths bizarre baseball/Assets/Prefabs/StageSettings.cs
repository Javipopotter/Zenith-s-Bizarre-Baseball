using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "StageSettings", menuName = "StageSettings")]
public class StageSettings : ScriptableObject
{
    [SerializeField] bool restArea = false;
    [SerializeField] List<string> allowedEnemies = new List<string>();
    public int numberOfSpawns = 0;
    public int hordes = 0;
    public int maxEnLimit = 0;
    public int spawnCoolDown = 0;
    public string Boss;
}
