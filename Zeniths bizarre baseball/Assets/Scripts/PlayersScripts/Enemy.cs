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
    public GameObject[] players;
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

    public override void Awake() {
        base.Awake();
        players = GameObject.FindGameObjectsWithTag("Player");
        player = GetRandomPlayer();
    }

    public override void GetDmg(float dmg, Vector2 knockbackDir)
    {
        knocked = true;
        base.GetDmg(dmg, knockbackDir);
        partyclesManager.FX.PlayEffect("bat_hit", transform.position + transform.up * 3, knockbackDir);
        AudioManager.instance.Play("get_bat_hit");
    }

    public override void Death()
    {
        base.Death();
        OnDeath();
    }

    public virtual void Update() {
        if(knocked || GameManager.GM.paused){return;}

        if(getNear)
        {
            GettingNear();
        }

        if ((player.transform.position.x - transform.position.x < 0 && transform.localScale.x > 0) || (player.transform.position.x - transform.position.x > 0 && transform.localScale.x < 0))
        {
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
    }

    public void GettingNear()
    {
        rb.velocity = GetPlayerDirection() * stats.speed * stats.modifiers["speed"];
    }

    public Vector2 GetPlayerDirection()
    {
        return (player.transform.position - transform.position).normalized;
    }

    public virtual void OnDeath()
    {
        Setlifes();
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

    private void OnEnable() {
        GameManager.GM.OnGameOver.AddListener(OnGameOver);
        player = GetRandomPlayer();
    }

    private void OnDisable() {
        GameManager.GM.OnGameOver.RemoveListener(OnGameOver);
    }

    void OnGameOver()
    {
        GetComponent<Animator>().enabled = false;
    }

    GameObject GetRandomPlayer()
    {
        return players[UnityEngine.Random.Range(0, players.Length)];
    }
}
