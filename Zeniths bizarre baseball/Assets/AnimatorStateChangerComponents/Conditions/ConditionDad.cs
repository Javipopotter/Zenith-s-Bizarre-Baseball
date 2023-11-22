using UnityEngine;

public class ConditionDad : StateMachineBehaviour
{
    public AnimatorParameterSaver animatorParameterSaver;
    public Animator an;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(animatorParameterSaver != null)
        {
            animatorParameterSaver.value = false;
        }
    }

    public virtual bool GetCondition()
    {
        if(animatorParameterSaver == null) {return true;}
        return animatorParameterSaver.value;
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(animatorParameterSaver != null)
        {
            animatorParameterSaver.value = false;
        }
    }
}
