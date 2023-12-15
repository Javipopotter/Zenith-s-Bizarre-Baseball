using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLifeBarUpdater : UIBarUpdater
{
    BossLifesManager currentBoss;
    private void Start() {
        StagesManager.stagesManager.OnStageSet.AddListener(CheckForBosses);
    }

    void CheckForBosses(Stage stage)
    {
        if(stage.settings.Boss != "")
        {
            gameObject.SetActive(true);
            currentBoss = FindObjectOfType<BossLifesManager>();
            currentBoss.OnLifeChange.AddListener(UpdateUI);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private void OnDisable() {
        if(currentBoss != null) currentBoss.OnLifeChange.RemoveListener(UpdateUI);
    }

    public override void UpdateUI(float value)
    {
        value /= currentBoss.stats.maxlifes;
        base.UpdateUI(value);
    }
}
