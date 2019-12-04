using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthMonitor : MonoBehaviour
{

    [SerializeField] private Slider m_HealthSlider;

    private void OnCollisionStay2D(Collision2D collision)
    {
        
    }
}
