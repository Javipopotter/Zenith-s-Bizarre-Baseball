using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public abstract class ButtonControllerParent : MonoBehaviour
{
    Button button;

    public virtual void Awake() {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClicked);
    }

    public virtual void OnButtonClicked() {}
}
