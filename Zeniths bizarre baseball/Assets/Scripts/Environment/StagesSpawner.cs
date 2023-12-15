using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StagesManager))]
public class StagesSpawner : MonoBehaviour
{
    List<List<Stage>> createdStages = new List<List<Stage>>();
    [SerializeField] Stage[] stagesAssets;
    [SerializeField] Stage[] shopStages;
    [SerializeField] Stage[] bossStages;
    [SerializeField] int shopUnFrequency = 4;
    public int stagesNumber = 36;
    private void Start() {
        createdStages.Add(new List<Stage>());
        createdStages[0].Add(Instantiate(stagesAssets[Random.Range(0, stagesAssets.Length)], GameManager.GM.backGrounds));
        // createdStages[0].Add(Instantiate(shopStages[0], GameManager.GM.backGrounds));
        for(int i = 0; i < stagesNumber; i++)
        {
            createdStages.Add(new List<Stage>());
            for(int o = 0; o < createdStages[i].Count; o++)
            {
                Stage current = createdStages[i][o];
                current.gameObject.SetActive(false);
                for(int a = 0; a < current.gates.Length; a++)
                {
                    if((i % (stagesNumber/3)) == 0 && i != 0)
                    {
                        CreateStage(i, current, bossStages[ i / (stagesNumber/3) ]);
                    }
                    else if(i % shopUnFrequency == 0 && i != 0 && a == 0)
                    {
                        var rnd = shopStages[Random.Range(0, shopStages.Length)];
                        CreateStage(i, current, rnd);
                    }
                    else
                    {
                        CreateStage(i, current, stagesAssets[Random.Range(0, stagesAssets.Length)]);
                    }
                }
            }
        }

        StagesManager.stagesManager.SetStage(createdStages[0][0]);

        for(int i = 0; i < createdStages.Count; i++)
        {
            for(int o = 0; o < createdStages[i].Count; o++)
            {
                createdStages[i][o].stageNum = i;
            }
        }
    }

    void CreateStage(int i, Stage current, Stage stage)
    {
        Stage newStage = Instantiate(stage, GameManager.GM.backGrounds);
        current.connectedStages.Add(newStage);
        createdStages[i + 1].Add(newStage);
    }
}
