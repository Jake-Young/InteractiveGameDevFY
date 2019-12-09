using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    #region Private Variables
    // Player State
    private float m_PlayerHealth = 100.0f;
    private bool m_IsAlive = true;
    [SerializeField] private Slider m_PlayerHealthSlider;
    [SerializeField] private float m_MinimumDistanceBetweenDroplets = 3.0f;
    // Player Level State
    private int m_PlayerHighScore;
    #endregion

    #region Public Variables

    public Transform[] m_HealthMonitors = new Transform[9];

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

    public float MinimumDistance
    {
        get { return m_MinimumDistanceBetweenDroplets; }
    }
    #endregion

    #region Function

    private void FixedUpdate()
    {
        //Debug.Log($"Health: {m_PlayerHealth}");
        m_PlayerHealthSlider.value = m_PlayerHealth;
    }

    #endregion
}
