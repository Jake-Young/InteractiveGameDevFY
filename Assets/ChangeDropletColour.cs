using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeDropletColour : MonoBehaviour
{

    public Store m_GameStore;
    public Player m_Player;

    private Color32 m_Blue = new Color32(0, 244, 255, 255);
    private Color32 m_Red = new Color32(255, 92, 92, 255);
    private Color32 m_Green = new Color32(91, 255, 127, 255);
    private Color32 m_Purple = new Color32(157, 91, 255, 255);

    private void Awake()
    {
        m_GameStore = GameObject.FindGameObjectWithTag("GameState").GetComponent<Store>();
    }

    private void Start()
    {

        if (m_GameStore.ActiveCharacterSkin == 0)
        {
            this.GetComponent<Renderer>().material.color = m_Blue;

            foreach (var droplet in m_Player.m_HealthMonitors)
            {
                droplet.GetComponent<SpriteRenderer>().color = m_Blue;
            }
        }
        else if (m_GameStore.ActiveCharacterSkin == 1)
        {
            this.GetComponent<Renderer>().material.color = m_Red;

            foreach (var droplet in m_Player.m_HealthMonitors)
            {
                droplet.GetComponent<SpriteRenderer>().color = m_Red;
            }
        }
        else if (m_GameStore.ActiveCharacterSkin == 2)
        {
            this.GetComponent<Renderer>().material.color = m_Green;

            foreach (var droplet in m_Player.m_HealthMonitors)
            {
                droplet.GetComponent<SpriteRenderer>().color = m_Green;
            }
        }
        else if (m_GameStore.ActiveCharacterSkin == 3)
        {
            this.GetComponent<Renderer>().material.color = m_Purple;

            foreach (var droplet in m_Player.m_HealthMonitors)
            {
                droplet.GetComponent<SpriteRenderer>().color = m_Purple;
            }
        }
    }
}
