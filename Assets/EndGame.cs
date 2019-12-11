using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndGame : MonoBehaviour
{
    [SerializeField] private GameState m_GameState;
    [SerializeField] private MenuFlow m_MenuFlow;
    [SerializeField] private GameObject m_EndGamePanel;
    [SerializeField] private TMP_Text m_HighScoreText;
    [SerializeField] private Player m_Player;

    private void Start()
    {
        m_GameState = GameObject.FindGameObjectWithTag("GameState").GetComponent<GameState>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (m_GameState.IsGameOver != true)
        {
            m_GameState.IsGameOver = true;
            StopGame(m_EndGamePanel);
        }
        
    }

    public void StopGame(GameObject conditionUI)
    {
        m_HighScoreText.text = string.Format("HighScore: {0}", m_Player.PlayerHighScore);
        conditionUI.SetActive(true);
    }
}
