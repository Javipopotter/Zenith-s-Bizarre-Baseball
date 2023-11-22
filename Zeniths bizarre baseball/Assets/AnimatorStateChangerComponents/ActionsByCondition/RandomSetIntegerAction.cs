using UnityEngine;

public class RandomSetIntegerAction : SetIntegerAction
{
    [SerializeField] int minValue;
    [SerializeField] int maxValue;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        value = Random.Range(minValue, maxValue + 1); // +1 because maxValue is exclusive
    }
}
