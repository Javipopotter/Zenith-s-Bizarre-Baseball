using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
using System;
using TMPro;

public class UIManager : MonoBehaviour
{
    Animator an;
    [SerializeField] CinemachineVirtualCamera cineMachineCamera;
    [SerializeField] Slider lifeBar;
    [SerializeField] GameObject _lifesContainer;
    public GameObject _skipText;
    [SerializeField] Sprite[] _heartSprites;
    Canvas gameUICanvas;
    CinemachineBasicMultiChannelPerlin channelPerlin;
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] TextMeshProUGUI moneyText;
    
    private void Awake() {
        an = GameObject.Find("GameUI").GetComponent<Animator>();
        channelPerlin = cineMachineCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        gameUICanvas = GameObject.Find("GameUI").GetComponent<Canvas>();
        _lifesContainer.transform.GetChild(_lifesContainer.transform.childCount - 1).GetComponent<Animator>().Play("beat");
        _lifesContainer.transform.GetChild(0).GetComponent<Animator>().speed = 2;
    }

    public void PlayAn(string name, int n, float n2)
    {
        an.Play(name, n, n2);
    }

    public void PlayAn(string name)
    {
        an.Play(name);
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        an.Play("GameOver");
    }

    public void Restart()
    {
        gameOverScreen.SetActive(false);
        an.Play("Restart");
    }

    public void SetSortingLayerInFront(bool inFront)
    {
        if(inFront) gameUICanvas.sortingLayerID = SortingLayer.NameToID("GameUI(General)");
        else gameUICanvas.sortingLayerID = SortingLayer.NameToID("GameUI(Behind)");
    }

    public IEnumerator CameraShake(int num)
    {
        channelPerlin.m_AmplitudeGain = num;
        channelPerlin.m_FrequencyGain = num;
        yield return new WaitForSecondsRealtime(0.2f);
        channelPerlin.m_AmplitudeGain = 0;
        channelPerlin.m_FrequencyGain = 0;
    }

    public void UpdateLifes(float lifes)
    {
        int i = (int)MathF.Round(lifes);
        for(int a = 0; a <= i; a++)
        {
            _lifesContainer.transform.GetChild(a).GetComponent<Image>().sprite = _heartSprites[0];
        }

        if(i > -1)
        { 
            _lifesContainer.transform.GetChild(i).GetComponent<Animator>().Play("beat");
        }

        if(i + 1 >= _lifesContainer.transform.childCount){return;}

        Image sr = _lifesContainer.transform.GetChild(i + 1).GetComponent<Image>();
        _lifesContainer.transform.GetChild(i + 1).GetComponent<Animator>().Play("stop");
        sr.sprite = _heartSprites[1];
    }

    public void UpdateBossLifeBar(float value, float maxValue)
    {
        lifeBar.maxValue = maxValue;
        lifeBar.value = value;
    }

    public void BossLifeBarIsActive(bool enabled)
    {
        lifeBar.gameObject.SetActive(enabled);
    }
    public void ShowSkipText(bool show)
    {
        _skipText.SetActive(show);
    }

    public void UpdateMoney()
    {
        moneyText.text = GameManager.GM.money.ToString();
    }
}
