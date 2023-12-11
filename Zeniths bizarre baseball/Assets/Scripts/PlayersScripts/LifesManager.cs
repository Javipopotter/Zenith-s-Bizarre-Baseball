using System;
using UnityEngine;
using UnityEngine.Events;

public class LifesManager : MonoBehaviour
{
    [SerializeField] float _lifes;
    public Stats stats;
    [HideInInspector] public Animator an;
    [HideInInspector] public Rigidbody2D rb;
    [SerializeField] UnityEvent DeathEvent;
    [HideInInspector] public UnityEvent<float> OnLifeChange;

    public virtual float lifes
    {
        get
        {
            return _lifes;
        }
        set
        {
            _lifes = value;

            OnLifeChange?.Invoke(value);

            if(transform.CompareTag("Player"))
            {
                if(value > stats.maxlifes){_lifes = stats.maxlifes;}
            }

            if(transform.name == "Claus")
            {
                GameManager.GM.UpdateBossLifeBar(_lifes, stats.maxlifes);
            }

            if(_lifes <= 0){an.SetTrigger("death");}
        }
    }

    public virtual void Awake() {
        stats = GetComponent<statsReference>().stats;
        an = GetComponent<Animator>();
        stats.maxlifes = lifes;
        rb = GetComponent<Rigidbody2D>();
    }

    public virtual void GetDmg(float dmg, Vector2 knockbackDir){
        if(GameManager.GM.paused){return;}

        AudioManager.instance.PlayOneShot("get_bat_hit");
        GameManager.GM.CameraShake(10);

        lifes -= dmg;
        rb.AddForce(knockbackDir * stats.poise, ForceMode2D.Impulse);

        an.Play("getDmg");
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
