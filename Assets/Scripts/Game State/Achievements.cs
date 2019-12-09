using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Achievements : MonoBehaviour
{

    #region Private Variables

    private bool m_Achieve10Distance = false;
    private bool m_Achieve50Distance = false;
    private bool m_Achieve100Distance = false;
    private bool m_Achieve250Distance = false;
    private bool m_Achieve500Distance = false;
    private bool m_Achieve1000Distance = false;
    private bool m_Find1Collectible = false;
    private bool m_Find2Collectible = false;
    private bool m_Find3Collectible = false;
    private bool m_Find5Collectible = false;
    private bool m_Find10Collectible = false;
    private bool m_SecretWayFound = false;

    #endregion

    #region Getters and Setters

    public bool Achieve10Distance
    {
        get { return m_Achieve10Distance; }
        set { m_Achieve10Distance = value; }
    }

    public bool Achieve50Distance
    {
        get { return m_Achieve50Distance; }
        set { m_Achieve50Distance = value; }
    }

    public bool Achieve100Distance
    {
        get { return m_Achieve100Distance; }
        set { m_Achieve100Distance = value; }
    }

    public bool Achieve250Distance
    {
        get { return m_Achieve250Distance; }
        set { m_Achieve250Distance = value; }
    }

    public bool Achieve500Distance
    {
        get { return m_Achieve500Distance; }
        set { m_Achieve500Distance = value; }
    }

    public bool Achieve1000Distance
    {
        get { return m_Achieve1000Distance; }
        set { m_Achieve1000Distance = value; }
    }

    public bool Find1Collectible
    {
        get { return m_Find1Collectible; }
        set { m_Find1Collectible = value; }
    }

    public bool Find2Collectible
    {
        get { return m_Find2Collectible; }
        set { m_Find2Collectible = value; }
    }

    public bool Find3Collectible
    {
        get { return m_Find3Collectible; }
        set { m_Find3Collectible = value; }
    }

    public bool Find5Collectible
    {
        get { return m_Find5Collectible; }
        set { m_Find5Collectible = value; }
    }

    public bool Find10Collectible
    {
        get { return m_Find10Collectible; }
        set { m_Find10Collectible = value; }
    }

    public bool SecretWayFound
    {
        get { return m_SecretWayFound; }
        set { m_SecretWayFound = value; }
    }

    #endregion

}
