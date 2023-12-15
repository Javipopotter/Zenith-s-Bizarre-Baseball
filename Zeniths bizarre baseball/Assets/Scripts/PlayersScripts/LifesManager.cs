using System;
using UnityEngine;
using UnityEngine.Events;

public class LifesManager : MonoBehaviour
{
    [SerializeField] float _lifes;
    public Stats stats;
    [HideInInspector] public Animator an;
    [HideInInspector] public Rigidbody2D rb;
    public UnityEvent OnDeath;
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

            if(_lifes <= 0){an.SetTrigger("death");}
        }
    }

    public virtual void Awake() {
        an = GetComponent<Animator>();
        stats.maxlifes = lifes;
        rb = GetComponent<Rigidbody2D>();
    }

    public virtual void GetDmg(float dmg, Vector2 knockbackDir){
        AudioManager.instance.PlayOneShot("get_bat_hit");

        lifes -= dmg;
        rb.AddForce(knockbackDir * stats.poise, ForceMode2D.Impulse);

        an.Play("getDmg");
    }

    public virtual void Death(){
        OnDeath.Invoke();
    }

    public virtual void OnEnable() {
        lifes = stats.maxlifes;
    }
}
