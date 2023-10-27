using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bat : MonoBehaviour
{
    [SerializeField]string target;
    bool hashit;
    private void OnTriggerEnter2D(Collider2D other) {

        if(hashit){return;}

        var rb = other.GetComponent<Rigidbody2D>();
        var vall = other.GetComponent<ball>();

        if(other.transform.CompareTag("ball")){
            hashit = true;
            if(target == "Enemy"){
                GameManager.GM.CameraShake(5);
                vall.SetProperties(1);
            }else{
                vall.SetProperties(2);
                GetComponentInParent<Animator>().Play("hitBack");
            }
        }

        if(other.transform.CompareTag(target))
        {
            hashit = true;
            other.GetComponent<LifesManager>().GetDmg(1);
            rb.AddForce((other.transform.position - transform.parent.transform.position).normalized * 2000 * other.GetComponent<LifesManager>().poise);
            // rb.velocity = (other.transform.position - transform.parent.transform.position).normalized * 20 * other.GetComponent<LifesManager>().poise;
        }
    }

    private void OnDisable() {
        hashit = false;
    }
}
