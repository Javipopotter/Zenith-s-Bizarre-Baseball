using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    public bool getNear;
    public GameObject player;
    [SerializeField] float vel;
    public bool knocked;
    Rigidbody2D rb;
    [SerializeField] UnityEvent OnPlayerHitEvent;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start() {
        if(GameObject.FindWithTag("Player"))
            player = GameObject.FindGameObjectWithTag("Player");
        gameObject.SetActive(false);
    }

    private void Update() {
        if(DialoguesManager.dialoguesManager.cinematic || knocked){return;}

        if(getNear){
            rb.velocity = GetPlayerDirection() * vel;
        }else{
            rb.velocity = Vector2.zero;
        }

        if((player.transform.position.x - transform.position.x < 0 && transform.localScale.x > 0) || (player.transform.position.x - transform.position.x > 0 && transform.localScale.x < 0))
        {
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
    }

    // private void OnTriggerEnter2D(Collider2D other) {
    //     if(!other.TryGetComponent(out Enemy en)){return;}
    //     if(en.knocked){GetComponent<LifesManager>().GetDmg();}
    // }

    Vector2 GetPlayerDirection()
    {
        return (player.transform.position - transform.position).normalized;
    }

    public void OnDeath()
    {
        Spawner.sp.KillCount++;
        Spawner.sp.enemyCount--;
        gameObject.SetActive(false);
    }

    public void GetKnocked()
    {
        knocked = true;
    }

    public void GetUnKnocked()
    {
        knocked = false;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player"))
        {
            OnPlayerHitEvent.Invoke();
        }
    }
}
