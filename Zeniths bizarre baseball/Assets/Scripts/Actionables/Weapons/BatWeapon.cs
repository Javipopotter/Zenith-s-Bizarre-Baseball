using UnityEngine;
using UnityEngine.Events;

public class BatWeapon : Weapon
{
    public UnityEvent onBallHit;
    float hitMultiplier = 1;
    [SerializeField] ballTarget target;
    [SerializeField] float ballDamageMultiplier = 1.5f;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.TryGetComponent(out EnemyLifesManager enemy))
        {
            enemy.GetDmg(damage, (other.transform.position - transform.position).normalized * knockBack);
        }

        if(other.TryGetComponent(out BallHandler ball))
        {
            AudioManager.instance.Play("hit_ball");  
            onBallHit?.Invoke();
            ball.SetBallRebound(target, transform.eulerAngles.z, damage * ballDamageMultiplier, knockBack, hitMultiplier);
        }
    }
}
