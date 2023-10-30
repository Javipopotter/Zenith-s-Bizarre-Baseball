using UnityEngine;

public class Shadowscript : MonoBehaviour
{
    [SerializeField] SpriteRenderer imitate;
    [SerializeField] bool imitateRot = true;
    SpriteRenderer sr;
    Vector2 _shadow_pos;
    Vector2 shadow_pos
    {
        get{return _shadow_pos;}
        set
        {
            if(_shadow_pos == value){return;}
            _shadow_pos = value;
            transform.localPosition = -value;
        }
    }
    Quaternion _shadow_rotation;
    Quaternion shadow_rotation
    {
        get{return _shadow_rotation;}
        set
        {
            if(_shadow_rotation == value){return;}
            if(imitateRot == false){return;}
            _shadow_rotation = value;
            transform.localRotation = value;
            transform.Rotate(new Vector3(40, 0, 0));
        }
    }

    private void Awake() {
        sr = GetComponent<SpriteRenderer>();
    }
    private void Update() {
        shadow_pos = imitate.transform.localPosition;
        shadow_rotation = imitate.transform.localRotation;
        sr.sprite = imitate.sprite;
        sr.flipX = imitate.flipX;
    }
}
