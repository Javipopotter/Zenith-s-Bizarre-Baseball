using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField] int gateNum;
    Stage stage;

    private void Awake() {
        stage = GetComponentInParent<Stage>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.transform.CompareTag("Player"))
        {
            stage.SetStage(gateNum);
        }
    }
}
