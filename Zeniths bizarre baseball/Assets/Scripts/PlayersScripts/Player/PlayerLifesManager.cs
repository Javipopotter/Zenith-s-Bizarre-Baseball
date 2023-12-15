using UnityEngine;

public class PlayerLifesManager : LifesManager
{
    public override void Awake()
    {
        base.Awake();
    }

    public override void Death()
    {
        base.Death();

        rb.isKinematic = true;
        rb.velocity = Vector2.zero;

        if(GameManager.GM != null) GameManager.GM.ChangeStateOfGame(GameStates.gameOver);
    }
}
