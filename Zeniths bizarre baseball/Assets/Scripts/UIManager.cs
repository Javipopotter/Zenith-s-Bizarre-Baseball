using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    Animator an;
    [SerializeField] CinemachineVirtualCamera cineMachineCamera;
    [SerializeField] Slider lifeBar;
    [SerializeField] GameObject _lifesContainer;
    public GameObject _skipText;
    [SerializeField] Sprite[] _heartSprites;
    Canvas gameUICanvas;
    
    private void Awake() {
        an = GameObject.Find("GameUI").GetComponent<Animator>();
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

    public void SetSortingLayerInFront(bool inFront)
    {
        if(inFront) gameUICanvas.sortingLayerID = SortingLayer.NameToID("GameUI(General)");
        else gameUICanvas.sortingLayerID = SortingLayer.NameToID("GameUI(Behind)");
    }

    public IEnumerator CameraShake(int num)
    {
        cineMachineCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = num;
        cineMachineCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = num;
        yield return new WaitForSecondsRealtime(0.2f);
        cineMachineCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
        cineMachineCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0;
    }

    public void UpdateLifeBar(float n1, float n2)
    {
        lifeBar.value = n1/n2;
    }

    public void UpdateLifes(int i)
    {
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

    public void ShowSkipText(bool show)
    {
        _skipText.SetActive(show);
    }
}
