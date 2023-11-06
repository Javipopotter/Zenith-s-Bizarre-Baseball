using System.Collections.Generic;
using UnityEngine;

public class StagesSpawner : MonoBehaviour
{
    List<List<Stage>> createdStages = new List<List<Stage>>();
    [SerializeField] Stage[] stagesAssets;
    [SerializeField] Stage[] shopStages;
    [SerializeField] Stage bossStage;
    [SerializeField] int stagesNumber = 10;
    private void Start() {
        ResetStages();
        Stage boss = Instantiate(bossStage.gameObject).GetComponent<Stage>();
        boss.gameObject.SetActive(false);
        createdStages.Add(new List<Stage>());
        createdStages[0].Add(Instantiate(stagesAssets[Random.Range(0, stagesAssets.Length)], GameManager.GM.backGrounds));
        for(int i = 0; i < stagesNumber; i++)
        {
            print(stagesNumber);
            createdStages.Add(new List<Stage>());
            for(int o = 0; o < createdStages[i].Count; o++)
            {
                Stage current = createdStages[i][o];
                current.gameObject.SetActive(false);
                for(int a = 0; a < current.gates.Length; a++)
                {
                    if(i == stagesNumber - 1)
                    {
                        current.connectedStages.Add(boss);
                    }
                    else if(i % 3 == 0 && i != 0 && a == 0)
                    {
                        CreateStage(i, current, shopStages[Random.Range(0, shopStages.Length)]);
                    }
                    else
                    {
                        CreateStage(i, current, stagesAssets[Random.Range(0, stagesAssets.Length)]);
                    }
                }
            }
        }
        GameManager.GM.SetStage(createdStages[0][0]);
    }

    void CreateStage(int i, Stage current, Stage stage)
    {
        Stage newStage = Instantiate(stage, GameManager.GM.backGrounds);
        current.connectedStages.Add(newStage);
        createdStages[i + 1].Add(newStage);
    }

    void ResetStages()
    {
        foreach(Stage stage in stagesAssets)
        {
            stage.settings.Reset();
        }
    }
    
}
