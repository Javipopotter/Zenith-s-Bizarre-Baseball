using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Psychic_Bat : MonoBehaviour
{
    SpriteRenderer sr;
    float lifetime;
    [SerializeField] float maxLifetime = 0.5f;
    Rigidbody2D rb;
    [SerializeField] Stats stats;

    private void Awake() {
        lifetime = maxLifetime;
        sr = GetComponentInChildren<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.transform.CompareTag("Enemy"))
        {
            gameObject.SetActive(false);
            other.GetComponent<LifesManager>().GetDmg(stats.damage * stats.damage, 
            rb.velocity.normalized * stats.knockback * stats.modifiers["knockback"]);
        }
    }

    private void Update() {
        lifetime -= Time.deltaTime;
        sr.color = new Vector4(1 ,1 ,1 , lifetime / maxLifetime);
        if(lifetime <= 0){gameObject.SetActive(false);}
    }

    private void OnEnable() {
        rb.AddForce(transform.up * 40, ForceMode2D.Impulse);
    }

    private void OnDisable() {
        lifetime = maxLifetime;
    }
}
