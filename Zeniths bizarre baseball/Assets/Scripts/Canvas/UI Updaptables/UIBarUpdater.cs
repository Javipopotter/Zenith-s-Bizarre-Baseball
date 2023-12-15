using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class UIBarUpdater : MonoBehaviour, IUpdaptable
{
    public Slider bar;
    private void Awake() {
        bar = GetComponent<Slider>();
    }

    public virtual void UpdateUI(float value) 
    {
        bar.value = value;
    }
}
