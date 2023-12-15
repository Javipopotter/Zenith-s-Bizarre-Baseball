using UnityEngine;

public class Pitcher : MonoBehaviour
{
    AttackHandler attackHandler;
    Pointer pointer;
    GameObject target;

    private void Awake() {
        pointer = GetComponentInChildren<Pointer>();
        target = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        pointer.SetPointer((target.transform.position - transform.position).normalized);
    }

    public void Throw()
    {
        attackHandler.UseWeapon();
    }
}
