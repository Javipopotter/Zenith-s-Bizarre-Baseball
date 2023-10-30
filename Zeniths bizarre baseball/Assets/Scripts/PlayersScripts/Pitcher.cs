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
    [SerializeField] GameObject ball;
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
        
        // switch(me.level)
        // {
        //     case 0:
        //     ballVel = 8;
        //     throwRate = 2.5f;
        //     break;
        //     case 1:
        //     ballVel = 10;
        //     throwRate = 2f;
        //     break;
        //     case 2:
        //     ballVel = 14;
        //     throwRate = 1.75f;
        //     break;
        // }
    }

    public void Throw()
    {
        var vall = GameManager.GM.GetObject("ball");
        vall.GetComponent<ball>().counterMod = 4;
        vall.transform.position = transform.position;
        vall.GetComponent<Rigidbody2D>().velocity = (player.transform.position - vall.transform.position).normalized * ballVel * Random.Range(1,1.6f);
    }
}
