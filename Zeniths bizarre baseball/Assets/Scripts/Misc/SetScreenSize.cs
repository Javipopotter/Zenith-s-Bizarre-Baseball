using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetScreenSize : MonoBehaviour
{
    private void Awake() {
        Screen.SetResolution(1920, 1080, FullScreenMode.FullScreenWindow);
    }
}
