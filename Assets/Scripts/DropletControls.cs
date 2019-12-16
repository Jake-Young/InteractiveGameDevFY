using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DropletControls : MonoBehaviour {

    #region Private Variables
    private Rigidbody2D m_DropletRigidBody;
    private float m_DirectionX;
    [SerializeField] private float m_UIControlMoveSpeed = 15.0f;
    [SerializeField] private float m_TiltMoveSpeed = 20f;
    [SerializeField] private float m_JoystickMoveSpeed = 12.5f;
    [SerializeField] private Settings m_GameSettings;
    [SerializeField] private DynamicJoystick m_VariableJoystick;
    [SerializeField] private GameObject m_JoystickObject;
    [SerializeField] private GameObject m_Buttons;
    #endregion

    #region Public Variables
    //public TMP_Text m_TestTextUI;
    #endregion

    void Start() {
        m_DropletRigidBody = GetComponent<Rigidbody2D>();
        m_GameSettings = GameObject.FindGameObjectWithTag("GameState").GetComponent<Settings>();
    }

    void FixedUpdate()
    {
        if (m_GameSettings.SelectedControlScheme == 0)
        {
            m_JoystickObject.SetActive(false);
            m_Buttons.SetActive(false);
            MovePlayerWithTilt();
        }
        else if (m_GameSettings.SelectedControlScheme == 1)
        {
            m_Buttons.SetActive(false);
            m_JoystickObject.SetActive(true);
            MovePlayerWithJoyStick();
        }
        else if (m_GameSettings.SelectedControlScheme == 2)
        {
            m_JoystickObject.SetActive(false);
            m_Buttons.SetActive(true);
        }
    }

    private void MovePlayerWithTilt()
    {
        m_DirectionX = Input.acceleration.x * m_TiltMoveSpeed;
        m_DropletRigidBody.AddForce(new Vector2(m_DirectionX, 0.0f));
        //m_TestTextUI.text = string.Format("Direction X: {0}", m_DirectionX);
    }

    private void MovePlayerWithJoyStick()
    {
        float direction = m_VariableJoystick.Horizontal * m_TiltMoveSpeed;
        m_DropletRigidBody.AddForce(new Vector2(direction, 0.0f));
    }

    public void MovePlayerRight()
    {
        m_DropletRigidBody.AddForce(new Vector2(m_UIControlMoveSpeed, 0.0f));
    }

    public void MovePlayerLeft()
    {
        m_DropletRigidBody.AddForce(new Vector2(-m_UIControlMoveSpeed, 0.0f));
    }

}
