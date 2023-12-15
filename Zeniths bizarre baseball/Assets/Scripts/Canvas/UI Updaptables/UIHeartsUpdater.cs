using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class UIHeartsUpdater : MonoBehaviour, IUpdaptable
{
    Sprite[] heartSprites;

    private void Awake() {
        transform.GetChild(transform.childCount - 1).GetComponent<Animator>().Play("beat");
        transform.GetChild(0).GetComponent<Animator>().speed = 2;
    }

    private void OnEnable() {
        FindObjectOfType<PlayerLifesManager>().OnLifeChange.AddListener(UpdateUI);
    }

    private void OnDestroy() {
        FindObjectOfType<PlayerLifesManager>().OnLifeChange.RemoveListener(UpdateUI);
    }

    void UpdateUI(float value)
    {
       UpdateLifes(value);
    }

    void UpdateLifes(float lifes)
    {
        int hp = (int)MathF.Round(lifes);

        for(int i = 0; i < transform.childCount; i++)
        {
            Transform heart = transform.GetChild(i);
            Image heartImage = heart.GetComponent<Image>();
            Animator heartAnimator = heart.GetComponent<Animator>();

            if(i < hp)
            {
                heartImage.sprite = heartSprites[0];
                heartAnimator.Play("stop");
            }
            else if(i > hp)
            {
                heartImage.sprite = heartSprites[1];
                heartAnimator.Play("stop");
            }
            else
            {
                heart.gameObject.SetActive(true);
                heartImage.sprite = heartSprites[0];
                heartAnimator.Play("beat");
            }
        }
    }
}
