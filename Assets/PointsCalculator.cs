using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointsCalculator : MonoBehaviour
{
    [SerializeField] private Player m_Player;
    [SerializeField] private TMP_Text m_ScoreText;

    public int m_ScoreToGive;

    private void LateUpdate()
    {
        m_ScoreText.text = string.Format("{0}", m_Player.PlayerHighScore);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Droplet")
        {
            m_Player.PlayerHighScore += m_ScoreToGive;
        }
    }
}
