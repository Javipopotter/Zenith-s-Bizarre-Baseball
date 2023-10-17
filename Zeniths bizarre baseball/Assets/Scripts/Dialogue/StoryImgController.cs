using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryImgController : MonoBehaviour
{
    [SerializeField] Sprite[] imgs;
    [SerializeField] Image img;
    public int num;

    public void SetImg()
    {
        img.sprite = imgs[num];
    }
}
