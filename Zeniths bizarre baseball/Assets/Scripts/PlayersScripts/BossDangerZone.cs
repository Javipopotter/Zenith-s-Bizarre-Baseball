using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDangerZone : MonoBehaviour
{
    ClausBoss clausBoss;
    private void Awake() {
        clausBoss = transform.parent.GetComponent<ClausBoss>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player") || other.CompareTag("ball"))
        {
            if(other.CompareTag("ball")){
                clausBoss.hitCount++;
                clausBoss.dangerTrigger = true;
                clausBoss.target = other.transform.position;
            }else if(other.CompareTag("Player")){
                clausBoss.hitCount = 0;
                clausBoss.dangerTrigger = true;
                clausBoss.target = other.transform.position;
            }
        }
    }
}
