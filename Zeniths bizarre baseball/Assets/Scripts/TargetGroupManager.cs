using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class TargetGroupManager : MonoBehaviour
{
    GameObject[] targets;
    float maxDist = 60;
    float minSize = 16;
    CinemachineVirtualCamera vcam;
    private void Awake() {
        vcam = FindObjectOfType<CinemachineVirtualCamera>();
        targets = GameObject.FindGameObjectsWithTag("Player");
    }

    private void Update() {
        float dis = Vector2.Distance(targets[0].transform.position, targets[1].transform.position);
        transform.position = Vector2.Lerp(targets[0].transform.position, targets[1].transform.position, 0.5f);

        if(dis <= maxDist)
        {
            vcam.m_Lens.OrthographicSize = minSize + (dis - minSize) * 0.1f;
        }

    }
}
