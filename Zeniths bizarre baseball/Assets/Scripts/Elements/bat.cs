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

            if(other.gameObject.TryGetComponent(out ball vall)){
                if(target == "Enemy" && !vall.hit){
                    GameManager.GM.CameraShake(5);
                    vall.SetProperties(1);
                }else if(target == "Player" && vall.hit){
                    vall.SetProperties(2);
                    GetComponentInParent<Animator>().Play("hitBack");
                }
            }

            if(other.transform.CompareTag(target))
            {
                other.GetComponent<LifesManager>().GetDmg(1 * stats.modifiers["damage"]);
                rb.AddForce((other.transform.position - transform.parent.transform.position).normalized * 2000 * other.GetComponent<LifesManager>().poise);
            }
        }
    }

    private void OnDisable() {
        hashit = false;
    }
}
