using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventOnHit : MonoBehaviour
{
    [SerializeField] UnityEvent Event;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("bat")){
            Event.Invoke();
        }
    }
}
