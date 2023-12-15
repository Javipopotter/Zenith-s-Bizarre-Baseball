using Unity.VisualScripting;
using UnityEngine;

public class ThrowerWeapon : Weapon
{
    float throwPower;
    ballTarget target;

    public override void Action()
    {
        base.Action();

        BallHandler b = ObjectPooler.pooler.GetObject(entity.ball, transform.position).GetComponent<BallHandler>();

        float angle = transform.eulerAngles.z;
        Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)).normalized;
        
        b.SetBall(target, direction * throwPower, damage, knockBack);

        Action();
    }
}
