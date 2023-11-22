using UnityEngine;

[RequireComponent(typeof(ReboundComponent))]
public class RebounderOnCollision : MonoBehaviour
{
    [SerializeField] string targetTag = "Obstacle";
    ReboundComponent reboundComponent;

    private void Awake() {
        reboundComponent = GetComponent<ReboundComponent>();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.transform.CompareTag(targetTag))
        {
            reboundComponent.Rebound(other.GetContact(0).normal);
        }
    }
}
