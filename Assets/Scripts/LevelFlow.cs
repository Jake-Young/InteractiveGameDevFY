using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using Michsky.UI.ModernUIPack;

public class LevelFlow : MonoBehaviour
{
    [SerializeField] private Camera m_PlayerCam;
    [SerializeField] private Player m_Player;
    [SerializeField] private GameState m_GameState;
    [SerializeField] private CinemachineTargetGroup m_TargetGroup;
    [SerializeField] private Slider m_Slider;
    [SerializeField] private EndGame m_EndGameObject;
    [SerializeField] private GameObject m_EndGamePanel;

    private int m_CurrentTargetFocus;
    private int m_PreviousTargetFocus = 0;

    private void Start()
    {
        StartCoroutine(DecreaseSlider(m_Slider));
        m_GameState = GameObject.FindGameObjectWithTag("GameState").GetComponent<GameState>();
    }

    private void Update()
    {
        if (m_GameState.IsGameOver)
        {
            StopCoroutine(DecreaseSlider(m_Slider));
        }

        if (m_Slider.value == 0 && m_GameState.IsGameOver == false)
        {
            m_EndGameObject.StopGame(m_EndGamePanel);
            m_GameState.IsGameOver = true;
        }
    }

    private void FixedUpdate()
    {
        m_CurrentTargetFocus = FindLowestDroplet(m_Player.m_HealthMonitors);
        m_TargetGroup.m_Targets[m_CurrentTargetFocus].weight = 20;

        if (m_PreviousTargetFocus != m_CurrentTargetFocus)
        {
            m_TargetGroup.m_Targets[m_PreviousTargetFocus].weight = 0;
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
        return chosenArrayPosistion;
    }

    private IEnumerator DecreaseSlider(Slider slider)
    {
        if (slider != null)
        {
            float timeSlice = 1;
            while (slider.value >= 0)
            {
                slider.value -= timeSlice;
                yield return new WaitForSeconds(1);
                if (slider.value <= 0)
                    break;
            }
        }
        yield return null;
    }

}
