using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody2D rb;
    Animator an;
    public float vel;
    float originalVel;
    [SerializeField]float rollCool = 0.5f;
    float attackCool = 1;
    [SerializeField] GameObject pointer;
    [SerializeField] bool atkTrigger;
    public bool blockControls;
    LifesManager lifesMan;
    [SerializeField] bool rolling;
    // Start is called before the first frame update
    void Start()
    {
        originalVel = vel;
        lifesMan = GetComponent<LifesManager>();
        // DontDestroyOnLoad(transform.gameObject);
        rb = GetComponent<Rigidbody2D>();
        an = GetComponent<Animator>();
        an.Play("EnterZone");
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.timeScale == 0 || DialoguesManager.dialoguesManager.cinematic || blockControls || GameManager.paused){return;}
        
        rb.velocity = new Vector2(Input.GetAxisRaw("horizontal") * vel, Input.GetAxisRaw("vertical") * vel);

        
        if((Input.GetAxis("horizontal") != 0 || Input.GetAxis("vertical") != 0) && !atkTrigger){
            an.SetBool("moveDir", true);
            if(Input.GetAxis("horizontal") > 0){
                an.Play("moveRight");
            }else if(Input.GetAxis("horizontal") < 0){
                an.Play("moveLeft");
            }else if(Input.GetAxis("vertical") > 0){
                an.Play("moveUp");
            }else if(Input.GetAxis("vertical") < 0){
                an.Play("moveDown");
            }
        }
        else
        {
            an.SetBool("moveDir", false);
        }

        if(Input.GetAxisRaw("vertical") != 0 && Input.GetAxis("horizontal") != 0)
        {
            vel = originalVel * 0.707f;
        }
        else
        {
            vel = originalVel;
        }


        Roll();
        Attack();

        if(rolling){
            rb.velocity = new Vector2(Input.GetAxisRaw("horizontal"), Input.GetAxisRaw("vertical")) * vel * 4;
        }
    }

    void Roll()
    {
        if (Input.GetKeyDown(KeyCode.Space) && rollCool <= 0){
            an.Play("roll");
            rollCool = 0.5f;
        }

        if (rollCool >= 0){
           rollCool -= Time.deltaTime;
        }
    }

    void Attack()
    {
        if (attackCool >= 0){
           attackCool -= Time.deltaTime;
        }

       if(Input.GetMouseButtonDown(0) && attackCool <= 0){
            attackCool = 0.25f;
           if(pointer.transform.rotation.eulerAngles.z > 225 && pointer.transform.rotation.eulerAngles.z < 315){
              an.Play("attackRight");
           }else if(pointer.transform.rotation.eulerAngles.z > 135 && pointer.transform.rotation.eulerAngles.z < 225){
              an.Play("attackDown");
           }else if(pointer.transform.rotation.eulerAngles.z > 45 && pointer.transform.rotation.eulerAngles.z < 135){
              an.Play("attackLeft");
           }else{
              an.Play("attackUp");
           }
       }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Enemy")){
            if(!other.GetComponent<Enemy>().knocked){
                GetComponent<LifesManager>().GetDmg();
            }
        }

        if(other.CompareTag("wall"))
        {
            GameManager.GM.StartLevel();
        }
    }

    public void Restart()
    {
        pointer.SetActive(true);
        rb.isKinematic = false;
        an.Play("init");
        gameObject.SetActive(true);
        lifesMan.Setlifes();
        transform.position = new Vector2(0, -25);
        an.Rebind();
        an.Play("EnterZone");
        gameObject.layer = LayerMask.NameToLayer("Default");
        rolling = false;
    }

    public void OnDeath()
    {
        rb.isKinematic = true;
        rb.velocity = Vector2.zero;
        pointer.SetActive(false);
    }
}
