using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PsychicBatManager : MonoBehaviour
{
    [SerializeField] GameObject psychicBat;
    List<GameObject> psychicBats = new List<GameObject>();

    private void Awake() {
        for(int i = 0; i < 5; i++)
        {
            psychicBats.Add(Instantiate(psychicBat, transform));
            psychicBats[i].SetActive(false);
        }
    }

    public void Activate(Vector2 pos, Quaternion rot)
    {
        foreach(GameObject bat in psychicBats)
        {
            if(!bat.activeSelf)
            {
                bat.transform.position = pos;
                bat.transform.rotation = rot;
                bat.SetActive(true);
                break;
            }
        }
    }
}
