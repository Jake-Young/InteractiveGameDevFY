using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsFlow : MonoBehaviour
{
    [SerializeField] private Slider m_VolumeSlider;
    [SerializeField] private TextMesh m_VolumeValue;
    [SerializeField] private Text m_ControlSchemeText;

    // Game Info and States
    private Settings m_Settings;

    private void Start()
    {
        m_Settings = GameObject.FindGameObjectWithTag("GameState").GetComponent<Settings>();
        CheckControlScheme();
    }

    private void FixedUpdate()
    {
        m_VolumeValue.text = string.Format("{0}%", ((int)(m_VolumeSlider.value * 100)));
    }

    public void ControlSchemeClick()
    {
        if (m_Settings.SelectedControlScheme != 2)
        {
            m_Settings.SelectedControlScheme++;
        }
        else
        {
            m_Settings.SelectedControlScheme = 0;
        }

        CheckControlScheme();
    }

    private void CheckControlScheme()
    {
        if (m_Settings.SelectedControlScheme == 0)
        {
            m_ControlSchemeText.text = "Tilt";
        }
        else if (m_Settings.SelectedControlScheme == 1)
        {
            m_ControlSchemeText.text = "Button";
        }
        else if (m_Settings.SelectedControlScheme == 2)
        {
            m_ControlSchemeText.text = "Slider";
        }

    }
}
