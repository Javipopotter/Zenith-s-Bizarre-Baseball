using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pitcher : MonoBehaviour
{
    public float coolDown = 2f;
    float throwRate = 2.5f;
    float ballVel = 8;
    GameObject player;
    Enemy me;
    private void OnEnable() {
        me = GetComponent<Enemy>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(DialoguesManager.dialoguesManager.cinematic || me.knocked || GameManager.paused){return;}
        
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
        b.GetComponent<Rigidbody2D>().velocity = (player.transform.position - b.transform.position).normalized * ballVel * Random.Range(1,1.6f);
    }
}
