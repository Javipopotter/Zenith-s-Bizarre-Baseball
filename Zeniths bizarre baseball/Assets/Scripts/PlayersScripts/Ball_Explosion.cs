
using UnityEngine;

public class Ball_Explosion : MonoBehaviour
{
    int _hitType;
    public void Explode()
    {
        GameManager.GM.BallExplosion(transform.position, 18, true, 7, 10, _hitType);
        _hitType = 0;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(_hitType != 0){return;}
        if(other.TryGetComponent(out ball b))
        {
            _hitType = b.ball_Type;
        }
    }
}
