using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball : MonoBehaviour
{
    bool _hit = false;
    public bool hit
    {
        get{return _hit;}
        set
        {
            _hit = value;
            LookTowards(rb.velocity);
        }
    }
    public float lifeTime = 10;
    Rigidbody2D rb;
    public bool homing;
    public Sprite[] sprites;
    [SerializeField] SpriteRenderer sr;
    GameObject player;
    [SerializeField]Animator an;
    public float counterMod = 1;
    int bounciness = 2;
    [HideInInspector] public int ball_Type;

    private void Awake() {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.transform.CompareTag("Player") && !hit){
            other.GetComponent<LifesManager>().GetDmg(1, rb.velocity.normalized * 5);
            gameObject.SetActive(false);
        }

        if(other.transform.CompareTag("Enemy") && hit){
            LookTowards(rb.velocity);
            if(bounciness > 0)
            {
                bounciness--;
                homing = false;
                Rebound();
            }
            else
            {
                gameObject.SetActive(false);
                hit = false;
            }
            other.GetComponent<LifesManager>().GetDmg(1, rb.velocity.normalized * 5);
        }

        if(other.transform.CompareTag("ball") && hit){
            LookTowards(rb.velocity);
            Rebound();
            ball b = other.GetComponent<ball>();
            Rigidbody2D b_Rb = other.GetComponent<Rigidbody2D>();
            GameManager.GM.CameraShake(5);
            b.hit = true;
            b.SetProperties(1);
            b_Rb.velocity = -rb.velocity.normalized * rb.velocity.magnitude;
        }

        if(other.transform.CompareTag("psychicBat"))
        {
            Rebound();
            other.gameObject.SetActive(false);
            GameManager.GM.CameraShake(5);
        }
    }

    void Rebound()
    {
        float random = Random.Range(0f, 260f);
        rb.velocity = new Vector2(Mathf.Cos(random), Mathf.Sin(random)) * rb.velocity.magnitude;
    }

    void LookTowards(Vector2 dir)
    {
        sr.gameObject.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg);
        if(hit){an.Play("deformation");}
    }
    private void Update() {
        if(lifeTime <= 0){
            gameObject.SetActive(false);
        }else{
            lifeTime -= Time.deltaTime;
        }

        if(homing)
        {
            if(!hit){
                rb.velocity = (player.transform.position - transform.position).normalized * rb.velocity.magnitude;
            }else{
                rb.velocity = (GameObject.Find("Claus").transform.position - transform.position).normalized * rb.velocity.magnitude;
            }
        }

        // if(homingDelay > 0){homingDelay -= Time.deltaTime;}
    }

    private void OnDisable() {
        hit = false;
        lifeTime = 10;
        SetProperties(0);
        homing = false;
        bounciness = 2;
    }

    public void SetProperties(int num)
    {
        ball_Type = num;
        lifeTime = 10f;
        sr.sprite = sprites[num];
        Vector2 dir = new Vector2(0,0);
        switch(num)
        {
            case 0:
            hit = false;
            homing = false;
            break;

            case 1:
            hit = true;
            dir = (Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position)).normalized;
            break;

            case 2:
            hit = false;
            homing = true;
            dir = (player.transform.position - transform.position).normalized;
            break;
        }

        rb.velocity = dir * rb.velocity.magnitude * counterMod;
    }
}
