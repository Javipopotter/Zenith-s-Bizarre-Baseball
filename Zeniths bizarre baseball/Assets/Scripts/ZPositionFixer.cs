using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZPositionFixer : MonoBehaviour
{
    float _yPos;
    float yPos
    {
        get{return _yPos;}
        set
        {
            _yPos = value;
            float angle = GameManager.GM.backGrounds.rotation.x * Mathf.Deg2Rad * 100;
            float h = transform.position.y / Mathf.Cos(angle);
            transform.position = new Vector3(transform.position.x, transform.position.y, h * Mathf.Sin(angle));
        }
    }   
    // Update is called once per frame
    void Update()
    {
        if(yPos != transform.position.y){yPos = transform.position.y;}
    }
}
