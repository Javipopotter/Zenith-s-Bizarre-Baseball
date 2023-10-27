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
        an.enabled = false;
        sr = transform.GetChild(0).GetComponentsInChildren<SpriteRenderer>();
        transform.rotation = new Quaternion(0, transform.rotation.y, transform.rotation.z, transform.rotation.w);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        an.enabled = true;
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
        if(!other.transform.CompareTag("ball"))
        {
            SetSortingLayer(other.transform);
        }
    }

    private void OnTriggerExit(Collider other) {
        an.enabled = false;
        foreach(SpriteRenderer r in sr)
        {
            r.sortingOrder = -1;
        }
    }

    void SetSortingLayer(Transform other)
    {
        foreach(SpriteRenderer r in sr)
        {
            if(r.transform.position.y < other.position.y)
            {
                r.sortingOrder = 1;
            }
            else
            {
                r.sortingOrder = -1;
            }
        }
    }
}
