using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    private CinemachineVirtualCamera mainCam;
    private CinemachineVirtualCamera fallenWarriorCamera;
    private CinemachineVirtualCamera bossCamera;

    private void Start()
    {
        mainCam = GameObject.Find("CameraFollowingPlayer").GetComponent<CinemachineVirtualCamera>();
        fallenWarriorCamera = GameObject.Find("CameraFollowingFallenWarrior").GetComponent<CinemachineVirtualCamera>();
        bossCamera = GameObject.Find("CameraFollowingDeathBringer").GetComponent<CinemachineVirtualCamera>();

        mainCam.Priority = 20;
        fallenWarriorCamera.Priority = 10;
    }

    public void SwitchCamera(bool playerInRadius)
    {
        if(playerInRadius)
        {
            mainCam.Priority = 10;
            fallenWarriorCamera.Priority = 20;
        }
        else
        {
            mainCam.Priority = 20;
            fallenWarriorCamera.Priority = 10;
        }
    }


}
