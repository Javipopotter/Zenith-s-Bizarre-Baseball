using System.Collections.Generic;
using UnityEngine;

public class StagesSpawner : MonoBehaviour
{
    List<List<Stage>> createdStages = new List<List<Stage>>();
    [SerializeField] Stage[] stagesAssets;
    [SerializeField] Stage bossStage;
    int stagesNumber = 1;
    private void Start() {
        Stage boss = Instantiate(bossStage.gameObject).GetComponent<Stage>();
        boss.gameObject.SetActive(false);
        createdStages.Add(new List<Stage>());
        createdStages[0].Add(Instantiate(stagesAssets[Random.Range(0, stagesAssets.Length)], GameManager.GM.backGrounds));
        for(int i = 0; i < stagesNumber; i++)
        {
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
                        break;
                    }
                    Stage newStage = Instantiate(stagesAssets[Random.Range(0, stagesAssets.Length)], current.transform);
                    current.connectedStages.Add(newStage);
                    createdStages[i+1].Add(newStage);
                }
            }
        }
        GameManager.GM.SetStage(createdStages[0][0]);
    }
}
