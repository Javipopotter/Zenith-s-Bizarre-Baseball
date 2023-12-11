using UnityEngine;

public class ReboundComponent : MonoBehaviour
{
    Rigidbody2D rb;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Rebound(Vector2 normal)
    {
        print("Rebound");
        Vector2 newDir = Vector2.Reflect(rb.velocity.normalized, normal);
        rb.AddForce(newDir * rb.velocity.magnitude * 2, ForceMode2D.Impulse);
    }
}
