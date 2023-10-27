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
    Stats stats;
    Animator an;
    public float poise;
    [SerializeField] UnityEvent DeathEvent;

    public float lifes
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
    }

    public void GetDmg(int dmg){
        if(GameManager.paused){return;}
        GameManager.GM.CameraShake(10);
        lifes -= dmg;
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
        lifes = stats.maxlifes;
    }

    public void TimeSet(float time)
    {
        Time.timeScale = time;
    }

    private void OnEnable() {
        Setlifes();
    }
}
