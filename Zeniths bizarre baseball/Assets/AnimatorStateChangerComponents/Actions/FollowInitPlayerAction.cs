using UnityEngine;

public class FollowInitPlayerAction : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(animator.TryGetComponent(out Enemy enemy))
        {
            enemy.GettingNear();
        }
        else
        {
            Debug.LogError("FollowPlayerAction: No se ha encontrado el componente Enemy en el animator");
        }
    }
}
