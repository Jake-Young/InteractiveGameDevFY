using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class LevelFlow : MonoBehaviour
{
    [SerializeField] private Camera m_PlayerCam;
    [SerializeField] private Player m_Player;
    [SerializeField] private CinemachineTargetGroup m_TargetGroup;

    private int m_CurrentTargetFocus;
    private int m_PreviousTargetFocus = 0;

    private void FixedUpdate()
    {
        //Transform lowestObject = FindLowestDroplet(m_Player.m_HealthMonitors);
        //float playerCamX = m_PlayerCam.transform.position.x;
        //float playerCamZ = m_PlayerCam.transform.position.z;

        //m_PlayerCam.transform.position = new Vector3(lowestObject.position.x, lowestObject.position.y, playerCamZ);

        m_CurrentTargetFocus = FindLowestDroplet(m_Player.m_HealthMonitors);
        m_TargetGroup.m_Targets[m_CurrentTargetFocus].weight = 20;

        if (m_PreviousTargetFocus != m_CurrentTargetFocus)
        {
            m_TargetGroup.m_Targets[m_PreviousTargetFocus].weight = 1;
            m_PreviousTargetFocus = m_CurrentTargetFocus;
        }
    }

    private int FindLowestDroplet(GameObject[] droplets)
    {
        Transform lowestObject = droplets[0].transform;
        int chosenArrayPosistion = 0;

        for (int arrayPos = 0; arrayPos < droplets.Length; arrayPos++)
        {
            if (droplets[arrayPos].transform.position.y < lowestObject.transform.position.y)
            {
                chosenArrayPosistion = arrayPos;
                lowestObject = droplets[arrayPos].transform;
            }
        }
        Debug.Log(chosenArrayPosistion);
        return chosenArrayPosistion;
    }
}
