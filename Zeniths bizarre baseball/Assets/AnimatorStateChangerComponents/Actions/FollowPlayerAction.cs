using UnityEngine;

public class FollowPlayerAction : StateMachineBehaviour
{
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(animator.TryGetComponent(out EnemyLifesManager enemy))
        {
            // enemy.GettingNear();
        }
        else
        {
            Debug.LogError("FollowPlayerAction: No se ha encontrado el componente Enemy en el animator");
        }
    }
}
