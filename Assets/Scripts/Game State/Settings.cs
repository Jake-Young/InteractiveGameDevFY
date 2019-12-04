using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    #region Private Variables

    // Control States
    private enum m_ControlSchemes { TiltControl = 0, ButtonControl = 1, SlideControl = 2 };
    private int m_SelectedControlSchemes = 0;
    private int m_Volume = 100;

    #endregion

    #region Getters and Setters
    public int SelectedControlScheme 
    {
        get { return m_SelectedControlSchemes; }
        set { m_SelectedControlSchemes = value; }
    }

    public int Volume 
    {
        get { return m_Volume; }
        set { m_Volume = value; } 
    }

    #endregion
}
