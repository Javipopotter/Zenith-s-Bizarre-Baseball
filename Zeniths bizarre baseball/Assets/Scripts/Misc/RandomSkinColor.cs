using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSkinColor : MonoBehaviour
{
    Material mat;

    private void Awake() {
        mat = GetComponent<Renderer>().material;
    }

    private void OnEnable() {
        mat.SetFloat("_num", Random.Range(0f,1f));
        mat.SetFloat("_num3", Random.Range(0f,1f));
    }
}
