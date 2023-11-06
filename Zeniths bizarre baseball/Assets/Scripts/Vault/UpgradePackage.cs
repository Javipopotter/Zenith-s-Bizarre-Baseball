using System.Collections.Generic;
using UnityEngine;

public class UpgradePackage : MonoBehaviour
{
    List<Upgrader> upgraders = new List<Upgrader>();
    [SerializeField] Stats statsToModify;
    [SerializeField] Sprite[] orbSprites;
    List<string> keys = new List<string>();
    Dictionary<string, Sprite> spritesDict = new Dictionary<string, Sprite>();

    private void Awake() {
        foreach(Transform child in transform)
        {
            upgraders.Add(child.gameObject.GetComponent<Upgrader>());
        }

        keys = new List<string>(statsToModify.modifiers.Keys);

        for(int i = 0; i < keys.Count; i++)
        {
            spritesDict.Add(keys[i], orbSprites[i]);
        }
    }

    private void OnEnable() {
        foreach(Upgrader upgrade in upgraders)
        {
            Dictionary<string, float> mod = statsToModify.modifiers;
            List<string> keys = new List<string>(mod.Keys);
            string key = keys[Random.Range(0, keys.Count)];
            upgrade.SetStat(key, 0.25f, spritesDict[key]);
        }
    }
}
