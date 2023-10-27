using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicPlayerController : MonoBehaviour
{
    Animator an;
    public bool movingMotion;
    public string loopAn;
    public float vel;
    public Vector2 moveDir;
    Rigidbody2D rb;
    public string dialog = "";

    private void Start() {
        an = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    public void PlayAn(string name)
    {
        an.Play(name);
    }

    public void StartDialog()
    {
        if(dialog != ""){
            DialoguesManager.dialoguesManager.ExecuteDialog(Dialogues.dialogues.texts[dialog]);
        }else{
            CinematicSwitch();
        }
    }

    public void ActivateDialog(string key)
    {
        DialoguesManager.dialoguesManager.ExecuteDialog(Dialogues.dialogues.texts[key]);
    }

    public void CinematicSwitch()
    {
        DialoguesManager.dialoguesManager.cinematic = !DialoguesManager.dialoguesManager.cinematic;
    }

    private void Update() {
        DialoguesManager.dialoguesManager.cinematic = true;
        if(movingMotion)
        {
            rb.velocity = moveDir * vel;
            an.Play(loopAn);
            an.SetBool("moveDir", true);
        }else{
            rb.velocity = Vector2.zero;
            an.SetBool("moveDir", false);
        }
    }

}
