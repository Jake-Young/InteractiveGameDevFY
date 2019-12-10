using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFlow : MonoBehaviour
{
    [SerializeField] private Camera m_PlayerCam;
    [SerializeField] private Player m_Player;

    private void Update()
    {
        Transform lowestObject = FindLowestDroplet(m_Player.m_HealthMonitors);
        float playerCamX = m_PlayerCam.transform.position.x;
        float playerCamZ = m_PlayerCam.transform.position.z;

        m_PlayerCam.transform.position = new Vector3(lowestObject.position.x, lowestObject.position.y, playerCamZ);
    }

    private Transform FindLowestDroplet(Transform[] droplets)
    {
        Transform lowestObject = droplets[0].transform;

        foreach (var droplet in droplets)
        {
            if (droplet.transform.position.y < lowestObject.transform.position.y)
            {
                lowestObject = droplet;
            }
        }

        return lowestObject;
    }
}
