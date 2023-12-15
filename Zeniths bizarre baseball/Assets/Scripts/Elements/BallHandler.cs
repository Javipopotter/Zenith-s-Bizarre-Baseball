using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ballTarget
{
    Player,
    Enemy
}

public class BallHandler : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sr;
    Sprite[] ballSprites;

    float damage;
    float knockback;
    public float lifeTime = 10;
    int _bounciness = 3;
    int bounciness
    {
        get{ return _bounciness; }
        set
        {
            _bounciness = value;
            if(value <= 0)
            {
                gameObject.SetActive(false);
            }
        }
    }

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other) {

        if(other.TryGetComponent(out LifesManager otherLifes)){
            RandomRebound();
            otherLifes.GetDmg(damage, rb.velocity.normalized * knockback);
        }

        if(other.TryGetComponent(out BallHandler ball)){
            Rebound((transform.position - other.transform.position).normalized);
        }
    }

    private void Update() {
        if(lifeTime <= 0){
            gameObject.SetActive(false);
        }else{
            lifeTime -= Time.deltaTime;
        }
    }

    private void OnEnable() {
        lifeTime = 10;
        bounciness = 3;
    }

    void SetVelocity(Vector2 velocity)
    {
        rb.velocity = velocity;
        if(rb.velocity.magnitude > 50){rb.velocity = rb.velocity.normalized * 50;}
    }

    void Rebound(Vector2 dir)
    {
        bounciness--;
        SetVelocity(dir * rb.velocity.magnitude);
    }

    void RandomRebound()
    {
        Rebound(GetRandomDirection());
    }

    Vector2 GetRandomDirection()
    {
        float angle = Random.Range(0f, 360f);
        return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)).normalized;
    }

    public void SetBall(ballTarget target, Vector2 velocity, float dmg, float knockBack)
    {
        damage = dmg;
        knockback = knockBack;
        sr.sprite = ballSprites[(int)target];
        SetVelocity(velocity);
    }

    public void SetBallRebound(ballTarget target, float angle, float dmg, float knockBack, float multiplier)
    {
        sr.sprite = ballSprites[(int)target];
        Vector2 velocity = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)).normalized;
        Rebound(velocity * multiplier);
    }
}
