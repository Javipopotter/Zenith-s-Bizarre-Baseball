using UnityEngine;

public class bat : MonoBehaviour
{
    [SerializeField]string target;
    bool hashit;
    [SerializeField] Stats stats;

    private void OnTriggerEnter2D(Collider2D other) {

        if(hashit){return;}

        if(other.transform.CompareTag("ball") || other.transform.CompareTag(target))
        {
            hashit = true;
            var rb = other.GetComponent<Rigidbody2D>();

            if(other.gameObject.TryGetComponent(out ball b)){
                AudioManager.instance.Play("hit_ball");  
                if(target == "Enemy"){
                    b.transform.position = transform.up * 0.5f + transform.position;
                    GameManager.GM.CameraShake(5);
                    b.SetProperties(1);
                }else if(target == "Player"){
                    b.SetProperties(2);
                    GetComponentInParent<Animator>().Play("hitBack");
                }
            }

            if(other.transform.CompareTag(target))
            {
                if(other.gameObject.TryGetComponent(out LifesManager lifesmanager))
                {
                    Vector2 knockbackDir = (other.transform.position - transform.parent.transform.position).normalized;
                    knockbackDir = knockbackDir * stats.knockback * stats.modifiers["knockback"];	
                    lifesmanager.GetDmg(stats.damage * stats.modifiers["damage"], knockbackDir);
                }
            }
        }
    }

    private void OnDisable() {
        hashit = false;
    }

    private void OnEnable() {
        AudioManager.instance.Play("hit_bat");
    }
}
