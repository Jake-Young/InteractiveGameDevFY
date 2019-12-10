using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthMonitor : MonoBehaviour
{
    #region Private Variables
    [SerializeField] private Slider m_HealthSlider;
    [SerializeField] private Player m_Player;
    [SerializeField] private float m_DropletHealth = 10;
    private bool m_TakeawayHealth = false;
    private float m_Distance = Mathf.Infinity;
    #endregion

    #region Getters and Setters
    // Getters and Setters
    #endregion

    #region Functions

    private void Awake()
    {
        m_Player = GetComponentInParent<Player>();
    }

    private void FixedUpdate()
    {
        //GetClosestDroplet(m_Player.m_HealthMonitors);

        //Debug.Log(m_Distance);

        //if (m_Distance >= m_Player.MinimumDistance && m_TakeawayHealth == false)
        //{
        //    m_TakeawayHealth = true;
        //    Debug.Log("Takeaway");
        //    m_Player.PlayerHealth -= m_DropletHealth;
        //}
        //else if (m_Distance <= m_Player.MinimumDistance && m_TakeawayHealth == true)
        //{
        //    m_TakeawayHealth = false;
        //    Debug.Log("Don't");
        //    m_Player.PlayerHealth += m_DropletHealth;
        //}
    }

    public GameObject GetClosestEnemy(GameObject[] droplets)
    {
        GameObject tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (GameObject droplet in droplets)
        {
            float dist = Vector3.Distance(droplet.transform.position, currentPos);
            if (dist < minDist)
            {
                tMin = droplet;
                minDist = dist;
            }
        }
        return tMin;
    }


    #endregion

}