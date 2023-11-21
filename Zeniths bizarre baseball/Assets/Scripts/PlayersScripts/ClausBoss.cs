using UnityEngine;

public class ClausBoss : MonoBehaviour
{
    public bool dangerTrigger;
    public bool moveTrigger;
    public bool attackTrigger;
    Animator an;
    GameObject player;
    float reactionTime;
    public int hitCount = 0;
    public Vector2 target;
    Rigidbody2D rb;
    LifesManager lifesMan;

    private void Awake() {
        an = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        lifesMan = GetComponent<LifesManager>();
    }

    private void OnEnable() {
        GameManager.GM.OnGameOver.AddListener(OnGameOver);
    }

    private void OnDisable() {
        GameManager.GM.OnGameOver.RemoveListener(OnGameOver);
    }

    void OnGameOver()
    {
        an.Play("unactive");
        enabled = false;
    }

    private void Update() {
        if(Time.timeScale == 0 || GameManager.GM.paused){return;}

        if(hitCount > 5)
        {
            hitCount = 0;
            dangerTrigger = false;
            an.Play("Stun");
        }

        if(reactionTime <= 0 && dangerTrigger)
        {
            dangerTrigger = false;
            var xdifer = target.x - transform.position.x;
            var ydifer = target.y - transform.position.y;

            if(Mathf.Abs(xdifer) >= Mathf.Abs(ydifer))
            {
                if(xdifer > 0){
                    an.Play("attackRight");
                }else{
                    an.Play("attackLeft");
                }
            }
            else
            {
                if(ydifer > 0){
                    an.Play("attackUp");
                }else{
                    an.Play("attackDown");
                }
            }
        }

        if(dangerTrigger)
        {
            if(dangerTrigger && an.GetCurrentAnimatorStateInfo(0).IsName("unactive"))
            {
                dangerTrigger = false;
            }
            else
            {
                reactionTime -= Time.deltaTime;
            }
        }

        if(moveTrigger && !attackTrigger) 
        {
            an.SetBool("moveDir", true);
            Vector2 dir = (player.transform.position - transform.position).normalized;
            rb.velocity = dir * 30;
            if(player.transform.position.x - transform.position.x > 0){
                an.Play("moveRight");
            }else{
                an.Play("moveLeft");
            }
        }
        else
        {
            an.SetBool("moveDir", false);
        }
    }

    public void hitBall()
    {
        var ball = GameManager.GM.GetObject("ball");
        hitCount = 0;
        ball.GetComponent<ball>().counterMod = 1.2f;
        Vector2 dir = (transform.position - player.transform.position).normalized;
        ball.transform.position = new Vector2(transform.position.x + dir.x * 2, transform.position.y + dir.y * 2);
        ball.GetComponent<Rigidbody2D>().velocity = dir * 10;
    }

    public void Dash(int power)
    {
        Vector2 dir = (player.transform.position - transform.position).normalized;
        rb.AddForce(dir * power);
    }

    public void Stop()
    {
        rb.velocity = Vector2.zero;
    }

    public void Teleport()
    {
        transform.position = player.transform.position + ((player.transform.position - transform.position).normalized * 4);
    }

    public void SetReactionTime(float reactTime)
    {
        reactionTime = reactTime;
    }

    public void OnDeath()
    {
        an.Rebind();
        DialoguesManager.dialoguesManager.ExecuteDialogViaKey("ClausDefeat");
        AudioManager.instance.Stop("Boss_theme");
        GameManager.GM.OpenGates();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.transform.CompareTag("bat") && an.GetCurrentAnimatorStateInfo(0).IsName("unactive"))
        {
            DialoguesManager.dialoguesManager.ExecuteDialogViaKey("Claus_GetsSurpriseAttack");
            an.Play("init");
        }
    }
}
