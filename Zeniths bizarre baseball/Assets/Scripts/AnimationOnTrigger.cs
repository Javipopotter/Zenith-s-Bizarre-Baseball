using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class AnimationOnTrigger : MonoBehaviour
{
    Animator an;
    SpriteRenderer[] sr;
    private void Awake() {
        an = GetComponent<Animator>();
        sr = transform.GetChild(0).GetComponentsInChildren<SpriteRenderer>();
        transform.rotation = new Quaternion(0, transform.rotation.y, transform.rotation.z, transform.rotation.w);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(transform.tag != "bat")
        {
            an.SetTrigger("contact");
        }
        else
        {
            an.SetTrigger("hit");
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(other.transform.CompareTag("Player")){
            SetSortingLayer(other.transform);
        }
    }

    private void OnTriggerExit(Collider other) {
        foreach(SpriteRenderer r in sr)
        {
            r.sortingLayerID = SortingLayer.NameToID("ScenaryElement(Under)");
        }
    }

    void SetSortingLayer(Transform other)
    {
        foreach(SpriteRenderer r in sr)
        {
            if(r.transform.position.y < other.position.y)
            {
                r.sortingLayerID = SortingLayer.NameToID("ScenaryElement(Over)");
            }
            else
            {
                r.sortingLayerID = SortingLayer.NameToID("ScenaryElement(Under)");
            }
        }
    }
}
