using System.Collections;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    private void Start() {
        DialoguesManager.dialoguesManager.ExecuteDialogViaKey("Tutorial");
    }
}
