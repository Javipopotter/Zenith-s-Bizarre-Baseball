using UnityEngine;
using UnityEngine.Events;

public class AttackHandler : MonoBehaviour
{
    Weapon selectedWeapon;

    float useCoolDown = 0;
    float coolDownReductionFactor = 1;

    public UnityEvent OnUseWeapon;
    public UnityEvent OnWeaponChange;

    public void UseWeapon()
    {
        if(selectedWeapon != null)
        {
            if(useCoolDown <= 0)
            {
                OnUseWeapon?.Invoke();
                selectedWeapon.Action();
                useCoolDown = selectedWeapon.baseCoolDown * coolDownReductionFactor;
            }
        }
    }

    private void Update() {
        if(useCoolDown > 0)
        {
            useCoolDown -= Time.deltaTime;
        }
    }

    private void OnDestroy() {
        OnUseWeapon.RemoveAllListeners();
    }
}
