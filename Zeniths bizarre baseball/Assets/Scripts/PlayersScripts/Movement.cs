using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody2D rb;
    Animator an;
    Vector2 vel;
    [SerializeField]float rollCool = 0.5f;
    float attackCool = 1;
    [SerializeField] GameObject pointer;
    [SerializeField] bool atkTrigger;
    public bool blockControls;
    LifesManager lifesMan;
    [SerializeField] bool rolling;
    [SerializeField] SpriteRenderer sr;
    [SerializeField] bool IsTest = false;
    [SerializeField] Vector2 lastFacing;
    Stats stats;
    private void Awake() {
        stats = GetComponent<statsReference>().stats;
        rb = GetComponent<Rigidbody2D>();
        an = GetComponent<Animator>();
        lastFacing = Vector2.down;
        lifesMan = GetComponent<LifesManager>();
        // DontDestroyOnLoad(transform.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(!IsTest)
        {
            if(Time.timeScale == 0 || DialoguesManager.dialoguesManager.cinematic || blockControls || GameManager.paused){return;}
        }
        
        vel = new Vector2(Input.GetAxisRaw("horizontal"), Input.GetAxisRaw("vertical")) * stats.speed * stats.modifiers["speed"];

        if(rolling){
            if(Input.GetAxisRaw("horizontal") != 0 && Input.GetAxisRaw("vertical") != 0)
            {
                vel = vel * 4;
            }
            else
            {
                vel = lastFacing * vel.magnitude * 4;
            }
        }
        else
        {
            if(Input.GetAxisRaw("horizontal") != 0 || Input.GetAxisRaw("vertical") != 0)
            {
                lastFacing = new Vector2(Input.GetAxisRaw("horizontal"), Input.GetAxisRaw("vertical"));
            }
        }

        if(Input.GetAxisRaw("vertical") != 0 && Input.GetAxisRaw("horizontal") != 0)
        {
            vel = vel * 0.707f ;
        }
        
        if(!atkTrigger)
        {
            rb.velocity = vel;
            
            if(!rolling)
            {
                if(Input.GetAxis("horizontal") != 0 || Input.GetAxis("vertical") != 0){
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
            }
        }


        Roll();
        Attack();

    }

    void Roll()
    {
        if (Input.GetButtonDown("dodge") && rollCool <= 0 && vel != Vector2.zero){
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

        if(Input.GetMouseButtonDown(0) && attackCool <= 0)
        {
            attackCool = 0.25f;
            rb.velocity = Vector2.zero;
            rb.velocity = AngleToVector2(pointer.transform.rotation.eulerAngles.z + 90) * stats.knockback * stats.modifiers["knockback"];
            LookToMouse("attackRight", "attackDown", "attackLeft", "attackUp");
        }
        else if(!an.GetBool("moveDir") && !atkTrigger && !rolling)
        {
            LookToMouse("idleRight", "Idle", "idleLeft", "idleUp");
        }

    }

    Vector2 AngleToVector2(float angleInDegrees)
    {
        float angleInRadians = angleInDegrees * Mathf.Deg2Rad;
        return new Vector2(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians));
    }

    void LookToMouse(string right, string down, string left, string up)
    {
        if (pointer.transform.rotation.eulerAngles.z > 225 && pointer.transform.rotation.eulerAngles.z < 315)
        {
            an.Play(right);
        }
        else if (pointer.transform.rotation.eulerAngles.z > 135 && pointer.transform.rotation.eulerAngles.z < 225)
        {
            an.Play(down);
        }
        else if (pointer.transform.rotation.eulerAngles.z > 45 && pointer.transform.rotation.eulerAngles.z < 135)
        {
            an.Play(left);
        }
        else
        {
            an.Play(up);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Enemy")){
            if(!other.GetComponent<Enemy>().knocked){
                GetComponent<LifesManager>().GetDmg(1, Vector2.zero);
            }
        }
    }

    public void Restart()
    {
        // sr.sortingLayerID = SortingLayer.NameToID("Players");
        // lifesMan.Setlifes();
        // rb.isKinematic = false;
        // an.Play("init");
        // transform.position = new Vector2(0, -23);
        // an.Rebind();
    }

    public void EnterZone()
    {
        an.Rebind();
        an.Play("EnterZone");
    }

    public void OnDeath()
    {
        sr.sortingLayerID = SortingLayer.NameToID("OverUI");
        pointer.SetActive(false);
        rb.isKinematic = true;
        rb.velocity = Vector2.zero;
    }
}
