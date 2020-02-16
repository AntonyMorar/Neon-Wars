using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform playerSpawn;

    CinemachineVirtualCamera vcam;
    CinemachineFramingTransposer vcamBody;
    Transform player;

    void Start()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
        vcamBody = vcam.GetCinemachineComponent<CinemachineFramingTransposer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (vcam.Follow == null && GameObject.Find("Player"))
        {
            AttatchPlayer();
        }
        else if (vcam.Follow == null)
        {
            DetachPlayer();
        }
    }

    public void AttatchPlayer()
    {
        vcamBody.m_DeadZoneWidth = 0.17f;
        vcamBody.m_DeadZoneHeight = 0.22f;
        vcamBody.m_XDamping = 1f;
        vcamBody.m_YDamping = 1f;
        player = GameObject.FindGameObjectsWithTag("Player")[0].transform;
        vcam.Follow = player;
    }

    void DetachPlayer()
    {
        vcamBody.m_DeadZoneWidth = 0f;
        vcamBody.m_DeadZoneHeight = 0f;
        vcamBody.m_XDamping = 7f;
        vcamBody.m_YDamping = 7f;
        vcam.Follow = playerSpawn;
    }
}
