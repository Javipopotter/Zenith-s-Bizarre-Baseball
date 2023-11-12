using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeSettings", menuName = "UpgradeSettings")]
public class UpgradeSetting : ScriptableObject
{

    [SerializeField] string[] abilityName;
    [SerializeField] string[] statName;
    [SerializeField] float[] value;
    public Sprite icon;
    public string description;

    public void Upgrade()
    {
        if(abilityName != null)
        {
            foreach(string ability in abilityName)
            {
                GameManager.GM.UpgradeAbility(ability);
            }
        }

        if(statName != null)
        {
            for(int i = 0; i < statName.Length; i++)
            {
                GameManager.GM.UpgradeStat(statName[i], value[i]);
            }
        }
    }
}
