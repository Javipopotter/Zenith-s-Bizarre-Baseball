using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    Rigidbody2D rb;
    Animator an;
    Vector2 vel;
    [SerializeField]float rollCool = 0.5f;
    float attackCool = 1;
    [SerializeField] bool atkTrigger;
    public bool blockControls;
    [SerializeField] bool rolling;
    Stats stats;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        an = GetComponent<Animator>();
    }

    public void SetSpeed(Vector2 velocity)
    {
        vel = velocity * stats.speed * stats.modifiers["speed"];
    }

    void Update()
    {
        if(!atkTrigger)
        {
            if(rolling){
                rb.velocity = vel * 4;
            }
            else{
                rb.velocity = vel;
            }
                
            
            if(!rolling)
            {
                if(vel != Vector2.zero){
                    an.SetBool("moveDir", true);
                    if(vel.x > 0){
                        an.Play("moveRight");
                    }else if(vel.x < 0){
                        an.Play("moveLeft");
                    }else if(vel.y > 0){
                        an.Play("moveUp");
                    }else if(vel.y < 0){
                        an.Play("moveDown");
                    }
                }
                else
                {
                    an.SetBool("moveDir", false);
                }
            }
        }

        if (rollCool >= 0){
           rollCool -= Time.deltaTime;
        }

        if (attackCool >= 0){
           attackCool -= Time.deltaTime;
        }

    }

    public void Dash()
    {
        if (rollCool <= 0 && vel != Vector2.zero){
            rollCool = 0.5f;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Enemy")){
            GetComponent<LifesManager>().GetDmg(1, Vector2.zero);
        }
    }

    public void EnterZone()
    {
        an.Rebind();
        an.Play("EnterZone");
    }
}
