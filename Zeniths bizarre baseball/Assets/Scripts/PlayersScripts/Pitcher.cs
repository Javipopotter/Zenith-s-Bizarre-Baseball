using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pitcher : MonoBehaviour
{
    public float coolDown = 2f;
    float throwRate = 2.5f;
    float ballVel = 8;
    Enemy me;

    private void OnEnable() {
        me = GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        if(me.knocked || GameManager.GM.paused){return;}
        
        coolDown -= Time.deltaTime;
        if(coolDown <= 0)
        {
            coolDown = throwRate + Random.Range(-0.5f, 0.5f);
            GetComponent<Animator>().Play("ThrowBall");
        }
    }

    public void Throw()
    {
        GameObject b = GameManager.GM.GetObject("ball");
        b.GetComponent<ball>().counterMod = 4;
        b.transform.position = transform.position;
        b.GetComponent<Rigidbody2D>().velocity = me.GetPlayerDirection() * ballVel * Random.Range(1,1.6f);
    }
}
