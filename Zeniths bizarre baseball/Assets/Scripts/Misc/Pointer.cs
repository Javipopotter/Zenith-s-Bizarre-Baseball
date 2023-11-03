using UnityEngine;

public class Pointer : MonoBehaviour
{
    Animator an;
    private void Update() 
    {
        if(Time.timeScale == 0){return;}
        var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }
}
