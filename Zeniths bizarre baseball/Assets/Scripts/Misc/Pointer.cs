using UnityEngine;
using UnityEngine.Events;

public class Pointer : MonoBehaviour
{
    public UnityEvent<float> onRotChange;
    public void SetPointer(Vector2 vector)
    {
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg - 90);
        onRotChange?.Invoke(transform.rotation.eulerAngles.z);
    }

    private void OnDestroy() {
        onRotChange.RemoveAllListeners();
    }
}
