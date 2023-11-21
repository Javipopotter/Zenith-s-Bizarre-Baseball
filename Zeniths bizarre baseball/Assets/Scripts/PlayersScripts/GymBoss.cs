using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GymBoss : Enemy
{
    readonly string STATE = "state";
    readonly string PATH = "path";
    float idleTime = 4f;
    float weightTime = 4f;
    [SerializeField] GameObject weightObject;
    Vector2 landPos;

    public override void Update() {
        base.Update();

        if(an.GetInteger(STATE) == 0)
        {
            ManageState(ref idleTime, 1);
        }
        else if(an.GetInteger(STATE) == 1)
        {
            if(an.GetInteger(PATH) == 0) {
                ManageState(ref weightTime, 0);
            }
            else if(an.GetInteger(PATH) == 1 && rb.velocity == Vector2.zero){
                rb.AddForce(GetPlayerDirection() * 30, ForceMode2D.Impulse);
                landPos = player.transform.position;
            }
            else
            {
                Rushing();
            }
        }
    }

    private void ManageState(ref float time, int nextState)
    {
        if (time <= 0)
        {
            time = Random.Range(3f, 4f);
            an.SetInteger(STATE, nextState);
            an.SetInteger(PATH, Random.Range(0, 2));
        }
        else
        {
            time -= Time.deltaTime;
        }
    }

    public void ThrowWeight()
    {
        an.speed = Random.Range(0.5f, 1.25f);
        //GameObject weight = Instantiate(weightObject, transform.position, Quaternion.identity);
        //weight.GetComponent<Rigidbody2D>().AddForce(GetPlayerDirection() * 20, ForceMode2D.Impulse);

        GameObject b = GameManager.GM.GetObject("ball");
        b.GetComponent<ball>().counterMod = 4;
        b.transform.position = transform.position;
        b.GetComponent<Rigidbody2D>().velocity = GetPlayerDirection() * 25 * Random.Range(1,1.6f);
    }

    void Rushing()
    {
        if(Vector2.Distance(landPos, transform.position) < 1)
        {
            an.SetInteger(STATE, 0);
            rb.velocity = Vector2.zero;
        }
    }

    public override void OnDeath()
    {
        an.Rebind();
        enabled = false;
        DialoguesManager.dialoguesManager.ExecuteDialogViaKey("GymBossDefeat");
        AudioManager.instance.Stop("Boss_theme");
        GameManager.GM.OpenGates();
    }
}
