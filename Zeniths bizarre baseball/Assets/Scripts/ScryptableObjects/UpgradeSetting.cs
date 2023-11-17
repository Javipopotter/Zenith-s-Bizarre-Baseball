using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "UpgradeSettings", menuName = "UpgradeSettings")]
public class UpgradeSetting : ScriptableObject
{

    [SerializeField] string[] abilityName;
    [SerializeField] string[] statName;
    [SerializeField] float[] value;
    public int price;
    public Sprite icon;
    public string description;
    public UnityEvent OnUpgrade;
    public bool canBeSelected = true;

    public virtual void Upgrade()
    {
        OnUpgrade.Invoke();

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
