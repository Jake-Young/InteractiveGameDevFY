using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour
{

    #region Private Variables

    protected enum m_CharacterSkins { DefaultSkin = 0, RedSkin = 1, GreenSkin = 2, PurpleSkin = 3 };
    private int m_ActiveCharacterSkin = 0;

    #endregion

    #region Getters and Setters

    public int ActiveCharacterSkin
    {
        get { return m_ActiveCharacterSkin; }
        set { m_ActiveCharacterSkin = value; }
    }

    #endregion

}
