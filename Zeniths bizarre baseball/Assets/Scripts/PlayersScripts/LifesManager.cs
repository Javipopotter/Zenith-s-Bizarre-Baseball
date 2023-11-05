using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements.Experimental;

public class LifesManager : MonoBehaviour
{
    [SerializeField] float _lifes;
    public Stats stats;
    Animator an;
    public Rigidbody2D rb;
    public float poise;
    [SerializeField] UnityEvent DeathEvent;

    public virtual float lifes
    {
        get
        {
            return _lifes;
        }
        set
        {
            _lifes = value;

            if(transform.CompareTag("Player"))
            {
                GameManager.GM.OnPlayerLifeChange(_lifes - 1);
            }

            if(transform.name == "Claus")
            {
                GameManager.GM.UpdateBossLifeBar(_lifes, stats.maxlifes);
            }

            if(_lifes <= 0){an.SetTrigger("death");}
        }
    }

    private void Awake() {
        stats = GetComponent<statsReference>().stats;
        an = GetComponent<Animator>();
        stats.maxlifes = lifes;
        rb = GetComponent<Rigidbody2D>();
    }

    public virtual void GetDmg(float dmg, Vector2 knockbackDir){
        if(GameManager.paused){return;}
        GameManager.GM.CameraShake(10);
        lifes -= dmg;
        an.Play("getDmg");
        rb.AddForce(knockbackDir * stats.poise, ForceMode2D.Impulse);
    }

    public virtual void Death(){
        DeathEvent.Invoke();
    }

    public void LayerChange(string layer)
    {
        gameObject.layer = LayerMask.NameToLayer(layer);
    }

    public void Setlifes () 
    {
        lifes = stats.maxlifes;
    }

    private void OnEnable() {
        Setlifes();
    }
}
