using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ClausBoss : MonoBehaviour
{
    public bool dangerTrigger;
    public float ballVel;
    public bool moveTrigger;
    public bool attackTrigger;
    Animator an;
    GameObject player;
    float reactionTime = 0.1f;
    public Vector2 target;
    Rigidbody2D rb;
    LifesManager lifesMan;

    private void Awake() {
        an = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        lifesMan = GetComponent<LifesManager>();
    }

    private void Update() {
        if(Time.timeScale == 0 || DialoguesManager.dialoguesManager.cinematic || GameManager.paused){return;}

        if(reactionTime <= 0)
        {
            dangerTrigger = false;
            reactionTime = 0.1f;
            var xdifer = target.x - transform.position.x;
            var ydifer = target.y - transform.position.y;

            if(Mathf.Abs(xdifer) >= Mathf.Abs(ydifer))
            {
                if(xdifer > 0){
                    an.Play("attackRight");
                }else{
                    an.Play("attackLeft");
                }
            }
            else
            {
                if(ydifer > 0){
                    an.Play("attackUp");
                }else{
                    an.Play("attackDown");
                }
            }
        }

        if(dangerTrigger)
        {
            reactionTime -= Time.deltaTime;
        }

        if(moveTrigger && !attackTrigger) 
        {
            an.SetBool("moveDir", true);
            Vector2 dir = (player.transform.position - transform.position).normalized;
            rb.velocity = dir * 30;
            if(player.transform.position.x - transform.position.x > 0){
                an.Play("moveRight");
            }else{
                an.Play("moveLeft");
            }
        }
        else
        {
            an.SetBool("moveDir", false);
        }

        if(an.GetCurrentAnimatorStateInfo(1).IsName("getDmg") && ballVel > 40)
        {
            ballVel = 0;
            an.Play("Stun");
        }
    }

    public void hitBall()
    {
        var ball = GameManager.GM.GetObject("ball");
        ball.GetComponent<ball>().counterMod = 1.2f;
        Vector2 dir = (transform.position - player.transform.position).normalized;
        ball.transform.position = new Vector2(transform.position.x + dir.x * 2, transform.position.y + dir.y * 2);
        ball.GetComponent<Rigidbody2D>().velocity = dir * 10;
    }

    public void Dash(int power)
    {
        Vector2 dir = (player.transform.position - transform.position).normalized;
        rb.AddForce(dir * power);
    }

    public void Stop()
    {
        rb.velocity = Vector2.zero;
    }

    public void Teleport()
    {
        transform.position = player.transform.position + ((player.transform.position - transform.position).normalized * 4);
    }

    public void Restart()
    {
        transform.position = new Vector2 (-0.06f,4.66f);
        lifesMan.Setlifes();
    }

    private void OnEnable() {
        Spawner.sp.enemyCount++;
    }

    private void OnDisable() {
        Spawner.sp.enemyCount--;
    }

    public void SetReactionTime(float reactTime)
    {
        reactionTime = reactTime;
    }
}
