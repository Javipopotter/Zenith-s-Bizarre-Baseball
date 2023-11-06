using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class partyclesManager : MonoBehaviour
{
    public static partyclesManager FX;
    [SerializeField] string[] keys;
    [SerializeField] ParticleSystem[] values;
    Dictionary<string, ParticleSystem> partyclesDict = new Dictionary<string, ParticleSystem>();

    private void Awake() {
        for(int i = 0; i < keys.Length; i++)
        {
            partyclesDict.Add(keys[i], values[i]);
        }
    }

    public void PlayEffect(string name)
    {
        partyclesDict[name].Play();
    }
}
