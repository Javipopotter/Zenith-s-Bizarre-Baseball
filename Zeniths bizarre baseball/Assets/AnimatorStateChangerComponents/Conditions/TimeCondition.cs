using UnityEngine;

public class TimeCondition : ConditionDad
{
    public float timerValue = 3;
    float timer;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        timer = timerValue;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer -= Time.deltaTime;
        if(timer <= 0) {animatorParameterSaver.value = true;}
    }
}
// teamorompebrazos