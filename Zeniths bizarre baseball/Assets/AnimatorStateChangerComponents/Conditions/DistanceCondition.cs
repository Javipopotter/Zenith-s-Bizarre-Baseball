using UnityEngine;
public class DistanceCondition : ConditionDad
{
    public string targetTag;
    [HideInInspector] public Vector2 targetPosition;
    public float distanceOffset;

    public override bool GetCondition()
    {
        targetPosition = GameObject.FindGameObjectWithTag(targetTag).transform.position;
        return Vector2.Distance(an.transform.position, targetPosition) < distanceOffset;
    }
}
