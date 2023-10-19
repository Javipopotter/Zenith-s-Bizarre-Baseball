using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements.Experimental;

public class LifesManager : MonoBehaviour
{
    [SerializeField] int _lifes;
    int maxLife;
    Animator an;
    public float poise;
    [SerializeField] UnityEvent DeathEvent;

    public int lifes
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

            if(_lifes <= 0){an.SetTrigger("death");}
        }
    }

    private void Awake() {
        an = GetComponent<Animator>();
        maxLife = lifes;
    }

    public void GetDmg(){
        if(GameManager.paused){return;}
        GameManager.GM.CameraShake(10);
        lifes--;
        an.Play("getDmg");
    }

    public void Death(){
        DeathEvent.Invoke();
    }

    public void LayerChange(string layer)
    {
        gameObject.layer = LayerMask.NameToLayer(layer);
    }

    public void Setlifes () 
    {
        lifes = maxLife;
    }

    public void TimeSet(float time)
    {
        Time.timeScale = time;
    }

    private void OnEnable() {
        Setlifes();
    }
}
