using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skater : Enemy
{
    float waitTime = 3f;
    [SerializeField] float maxWaitTime = 3f;
    readonly string STATE = "state";
    Vector2 direction;

    public override void Awake() {
        waitTime = maxWaitTime;
        base.Awake();
    }

    public override void Update()
    {
        base.Update();

        if(an.GetInteger(STATE) == 0)
        {
            if(waitTime <= 0)
            {
                waitTime = maxWaitTime;
                direction = GetPlayerDirection();
                an.SetInteger(STATE, 1);

                rb.AddForce(direction * 40, ForceMode2D.Impulse);
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.transform.CompareTag("wall"))
        {
            an.SetInteger(STATE, 0);
            rb.velocity = Vector2.zero;
        }
    }
}
