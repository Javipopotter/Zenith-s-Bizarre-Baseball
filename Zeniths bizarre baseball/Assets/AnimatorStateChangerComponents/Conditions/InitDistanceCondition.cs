using UnityEngine;

public class InitDistanceCondition : DistanceCondition
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        targetPosition = GameObject.FindGameObjectWithTag(targetTag).transform.position;
    }

    public override bool GetCondition()
    {
        return Vector2.Distance(targetPosition, targetPosition) < distanceOffset;
    }
}
