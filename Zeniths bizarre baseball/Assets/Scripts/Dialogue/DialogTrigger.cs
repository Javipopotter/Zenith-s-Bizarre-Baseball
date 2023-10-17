using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    [SerializeField]GameObject canvas;
    public static DialogTrigger trigger;

    private void Awake() {
        trigger = this;
    }

    public void PlayDialog(string dialogName) {

        canvas.SetActive(true);
        DialoguesManager.dialoguesManager.ExecuteDialog(Dialogues.dialogues.texts[dialogName]);
    }
}
