using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class partyclesManager : MonoBehaviour
{
    public static partyclesManager FX;
    [SerializeField] string[] keys;
    [SerializeField] ParticleSystem[] values;
    Dictionary<string, ParticleSystem> partyclesDict = new Dictionary<string, ParticleSystem>();
    [SerializeField] TextMeshPro text;

    private void Awake() {
        FX = this;
        for(int i = 0; i < keys.Length; i++)
        {
            partyclesDict.Add(keys[i], values[i]);
        }
    }

    public void PlayEffect(string name, Vector2 pos, Vector2 dir)
    {
        ParticleSystem partycle = partyclesDict[name];
        partycle.transform.position = pos;
        partycle.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg);
        partycle.Play();
    }

    public void PlayText(Vector2 pos, string txt)
    {
        // text.transform.parent.gameObject.SetActive(true);
        // text.transform.parent.GetComponent<Animator>().SetTrigger("DmgTxt");
        // text.transform.parent.transform.position = pos;
        // text.text = txt;
    }
}
