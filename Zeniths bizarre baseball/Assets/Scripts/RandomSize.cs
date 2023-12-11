using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSize : MonoBehaviour
{
    [SerializeField] Transform parent;
    [SerializeField] Vector2 minMaxSize;

    private void Start() {
        foreach(Transform child in parent)
        {
            float random = Random.Range(minMaxSize.x, minMaxSize.y);
            child.localScale = new Vector2(random, random);
        }
    }
}
