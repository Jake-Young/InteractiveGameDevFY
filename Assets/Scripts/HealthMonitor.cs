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
        //Debug.Log(GetClosestEnemy(m_Player.m_HealthMonitors));

        if (GetClosestDroplet(m_Player.m_HealthMonitors) >= m_Player.MinimumDistance && m_TakeawayHealth == false)
        {
            m_TakeawayHealth = true;
            Debug.Log("Takeaway");
            m_Player.PlayerHealth -= m_DropletHealth;
        }
        else if (GetClosestDroplet(m_Player.m_HealthMonitors) <= m_Player.MinimumDistance && m_TakeawayHealth == true)
        {
            m_TakeawayHealth = false;
            Debug.Log("Don't");
            m_Player.PlayerHealth += m_DropletHealth;
        }
    }

    private float GetClosestDroplet(Transform[] enemies)
    {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (Transform t in enemies)
        {
            float dist = Vector3.Distance(t.position, currentPos);
            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }
        //Debug.Log(minDist);
        return minDist;
    }


    #endregion

}