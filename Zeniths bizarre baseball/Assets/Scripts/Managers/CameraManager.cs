using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera vcam;

    void SetCameraSizeAndLimit(float cameraSize, Collider2D cameraLimit)
    {
        vcam.GetComponent<CinemachineConfiner2D>().m_BoundingShape2D = cameraLimit;
        vcam.m_Lens.OrthographicSize = cameraSize;
    }
}
