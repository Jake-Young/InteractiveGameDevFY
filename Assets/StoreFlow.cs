using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreFlow : MonoBehaviour
{
    [SerializeField] private Image[] m_IconArray = new Image[4];

    private Store m_Store;
    private int m_PreviousSkin;

    private void Start()
    {
        m_Store = GameObject.FindGameObjectWithTag("GameState").GetComponent<Store>();
        //m_PreviousSkin = 3;
        CheckActiveCharacterSkin();
    }

    public void OnBlueClick()
    {
        m_Store.ActiveCharacterSkin = 0;
        CheckActiveCharacterSkin();
    }

    public void OnRedClick()
    {
        m_Store.ActiveCharacterSkin = 1;
        CheckActiveCharacterSkin();
    }

    public void OnGreenClick()
    {
        m_Store.ActiveCharacterSkin = 2;
        CheckActiveCharacterSkin();
    }

    public void OnPurpleClick()
    {
        m_Store.ActiveCharacterSkin = 3;
        CheckActiveCharacterSkin();
    }

    private void CheckActiveCharacterSkin()
    {
        if (m_Store.ActiveCharacterSkin == 0)
        {
            //Blue
            ActivateSelectedCharacterSkin();
        }
        else if (m_Store.ActiveCharacterSkin == 1)
        {
            //Red
            ActivateSelectedCharacterSkin();
        }
        else if (m_Store.ActiveCharacterSkin == 2)
        {
            //Green
            ActivateSelectedCharacterSkin();
        }
        else if (m_Store.ActiveCharacterSkin == 3)
        {
            //Purple
            ActivateSelectedCharacterSkin();
        }
    }

    private void ActivateSelectedCharacterSkin()
    {
        for (int i = 0; i < m_IconArray.Length; i++)
        {
            if (i != m_Store.ActiveCharacterSkin)
            {
                m_IconArray[i].enabled = false;
            }
            else
            {
                m_IconArray[m_Store.ActiveCharacterSkin].enabled = true;
            }
        }
    }
}
