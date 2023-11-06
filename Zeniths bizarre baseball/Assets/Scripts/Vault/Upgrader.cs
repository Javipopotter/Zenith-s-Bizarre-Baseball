using UnityEngine;

public class Upgrader : MonoBehaviour
{
    [SerializeField] Stats statsTomodify;
    string statToModify;
    float value;
    Animator an;
    string _upgradeType;
    [SerializeField] SpriteRenderer sr;
    public string upgradeType{
        get{return _upgradeType;} 
        set{_upgradeType = value;
        }
    }

    private void Awake() {
        an = GetComponent<Animator>();
    }

    private void OnEnable() {
        an.Play("Appear", 0, Random.Range(0f, 0.4f));
    }

    public void Upgrade()
    {
        statsTomodify.modifiers[statToModify] += value;
        foreach(Upgrader upgrade in transform.parent.GetComponentsInChildren<Upgrader>())
        {
            upgrade.an.Play("Vanish", 0, Random.Range(0f, 0.4f));
        }
    }

    public void SetStat(string stat, float val, Sprite sprite)
    {
        statToModify = stat;
        value = val;
        sr.sprite = sprite;
    }

    public void Vanish()
    {
        transform.parent.gameObject.SetActive(false);
    }
}
