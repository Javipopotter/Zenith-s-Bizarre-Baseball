using UnityEngine;

public class RandomTimeCondition : TimeCondition
{
    [SerializeField] float minTime = 1;
    [SerializeField] float maxTime = 4;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        timerValue = Random.Range(minTime, maxTime);
        base.OnStateEnter(animator, animatorStateInfo, layerIndex);
    }
}
