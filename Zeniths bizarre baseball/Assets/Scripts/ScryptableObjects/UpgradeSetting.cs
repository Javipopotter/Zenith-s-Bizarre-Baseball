using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem.Interactions;

[CreateAssetMenu(fileName = "UpgradeSettings", menuName = "UpgradeSettings")]
public class UpgradeSetting : ScriptableObject
{

    [SerializeField] string[] abilityName;
    [SerializeField] string[] statName;
    [SerializeField] float[] value;
    public int price;
    public Sprite icon;
    public string description;

    public virtual void Upgrade()
    {
        if(abilityName != null)
        {
            foreach(string ability in abilityName)
            {
                // GameManager.GM.UpgradeAbility(ability);
            }
        }

        if(statName != null)
        {
            for(int i = 0; i < statName.Length; i++)
            {
                // GameManager.GM.UpgradeStat(statName[i], value[i]);
            }
        }
    }
}
