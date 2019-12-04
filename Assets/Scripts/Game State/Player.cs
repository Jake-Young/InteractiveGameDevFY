using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Private Variables

    private float m_PlayerHealth;
    private bool m_IsAlive;
    private int m_PlayerHighScore;

    #endregion

    #region Getters and Setters
    // Player States
    public float PlayerHealth
    {
        get { return m_PlayerHealth; } 
        set { m_PlayerHealth = value; } 
    }
    public bool IsAlive 
    {
        get { return m_IsAlive; }
        set { m_IsAlive = value; } 
    }

    // Player Level States
    public int PlayerHighScore 
    {
        get { return m_PlayerHighScore; }
        set { m_PlayerHighScore = value; } 
    }
    #endregion
}
