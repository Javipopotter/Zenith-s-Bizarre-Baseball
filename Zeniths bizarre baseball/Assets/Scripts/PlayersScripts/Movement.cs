using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

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

    public bool veganism = false;
    public bool hunter = false;

    bool _indebted;
    public bool indebted
    {
        get{return _indebted;}
        set
        {
            _indebted = value;

            if(value)
            {
                rollType = "roll";
            }
            else
            {
                rollType = "dash";
            }
        }
    }

    string rollType = "dash";
    PsychicBatManager psychicBat;

    private void Awake() {
        psychicBat = GetComponent<PsychicBatManager>();
        stats = GetComponent<statsReference>().stats;
        rb = GetComponent<Rigidbody2D>();
        an = GetComponent<Animator>();
        lastFacing = Vector2.down;
        lifesMan = GetComponent<LifesManager>();
        // DontDestroyOnLoad(transform.gameObject);
    }

    public void SetSpeed(Vector2 velocity)
    {
        vel = velocity * stats.speed * stats.modifiers["speed"];
    }

    // Update is called once per frame
    void Update()
    {
        if(!IsTest)
        {
            if(Time.timeScale == 0 || blockControls || GameManager.GM.paused){return;}
        }
        
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
            an.Play(rollType);
            rollCool = 0.5f;
        }
    }

    public void Attack()
    {
        if(attackCool <= 0)
        {
            attackCool = 0.3f;
            LookToMouse("attackRight", "attackDown", "attackLeft", "attackUp");
            if(veganism){psychicBat.Activate(pointer.transform.position + pointer.transform.up * 2, pointer.transform.rotation);}
        }
    }

    public void SetPointer(Vector2 vector)
    {
        if(!atkTrigger)
        {
            pointer.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg - 90);
            
            if(!an.GetBool("moveDir") && !rolling)
            {
                LookToMouse("idleRight", "Idle", "idleLeft", "idleUp");
            }
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
        if(hunter){lifesMan.lifes++;}
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
