using System;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : LifesManager
{
    bool _getNear;
    public override float lifes { get => base.lifes; set => base.lifes = value; }
    bool getNear
    {
        get{return _getNear;}
        set
        {
            _getNear = value;
            if(!value)
            {
                rb.velocity = Vector2.zero;
            }
        }
    }
    public GameObject player;
    bool _knocked;
    public bool knocked
    {
        get{return _knocked;}
        private set
        {
            _knocked = value;
            if(!value)
            {
                rb.velocity = Vector2.zero;
            }
        }
    }
    [SerializeField] UnityEvent OnPlayerHitEvent;

    private void Start() {
        if(GameObject.FindWithTag("Player"))
            player = GameObject.FindGameObjectWithTag("Player");
    }

    public override void GetDmg(float dmg, Vector2 knockbackDir)
    {
        knocked = true;
        base.GetDmg(dmg, knockbackDir);
    }

    public override void Death()
    {
        base.Death();
        OnDeath();
    }

    private void Update() {
        if(DialoguesManager.dialoguesManager.cinematic || knocked || GameManager.paused){return;}

        if(getNear){
            rb.velocity = GetPlayerDirection() * stats.speed;
        }

        if((player.transform.position.x - transform.position.x < 0 && transform.localScale.x > 0) || (player.transform.position.x - transform.position.x > 0 && transform.localScale.x < 0))
        {
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
    }


    Vector2 GetPlayerDirection()
    {
        return (player.transform.position - transform.position).normalized;
    }

    public void OnDeath()
    {
        AudioManager.instance.Play("enemy_death");
        Spawner.sp.KillCount++;
        Spawner.sp.enemyCount--;
        gameObject.SetActive(false);
    }

    public void GetKnocked()
    {
        knocked = true;
    }

    public void GetUnKnocked()
    {
        knocked = false;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player"))
        {
            OnPlayerHitEvent.Invoke();
        }
    }

    public void GetNear()
    {
        getNear = true;
    }

    public void GetAway()
    {
        getNear = false;
    }
}
