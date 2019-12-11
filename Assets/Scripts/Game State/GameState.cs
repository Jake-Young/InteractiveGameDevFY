using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour 
{

    #region Private Variables

    private bool m_IsGameOver = false;
    private bool m_IsGamePaused = false;

    #endregion

    #region Getters and Setters

    public bool IsGameOver
    {
        get { return m_IsGameOver; }
        set { m_IsGameOver = value; }
    }

    public bool IsGamePaused
    {
        get { return m_IsGamePaused; }
        set { m_IsGamePaused = value; }
    }

    #endregion

    #region
    private void Awake()
    {
        m_IsGameOver = false;
    }

    #endregion

}
