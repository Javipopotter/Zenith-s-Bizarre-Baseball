using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class Dialogues : MonoBehaviour
{
    public static Dialogues dialogues;
    public Dictionary<string, string> texts = new Dictionary<string,string>();
    public List<string> textKeys;
    public TextAsset file;
    string[] fileDivisons;

    private void Awake() {
        dialogues = this;
        fileDivisons = file.text.Split("___");
        foreach(string line in fileDivisons)
        {
            string[] a = line.Split("//");
            texts.Add(a[0].Replace("\n", ""), a[1].Replace("\n", ""));
        }
        foreach(string key in texts.Keys)
        {
            textKeys.Add(key);
        }
        foreach(string a in texts.Keys)
        {
            textKeys.Add(a);
        }
    }
}
