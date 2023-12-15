using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    public enum type{
        hordes, shop, boss
    }

    public type typeOfStage;
    public Collider2D[] gates;
    public List<Stage> connectedStages = new List<Stage>();
    public GameObject spawners;
    public PolygonCollider2D cameraLimit;
    public StageSettings settings;
    public GameObject startPos;
    [SerializeField] GameObject altMaps;
    public int stageNum;

    private void Awake() {
        for(int i = 0; i < altMaps.transform.childCount; i++)
        {
            float rand = Random.Range(0, 100);
            if(rand > 50)
            {
                altMaps.transform.GetChild(i).gameObject.SetActive(true);
            }
        }
    }

    public void SetStage(int stage)
    {
        StagesManager.stagesManager.SetStage(connectedStages[stage]);
    }

    public void OpenGates()
    {
        foreach(Collider2D col in gates)
        {
            col.isTrigger = true;
        }
    }
}
