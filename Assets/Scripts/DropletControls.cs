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
    [SerializeField] private Settings m_GameSettings;
    #endregion

    #region Public Variables
    //public TMP_Text m_TestTextUI;
    #endregion

    void Start() {
        m_DropletRigidBody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (m_GameSettings.SelectedControlScheme == 0)
        {
            MovePlayerWithTilt();
        }
    }

    private void MovePlayerWithTilt()
    {
        m_DirectionX = Input.acceleration.x * m_TiltMoveSpeed;
        m_DropletRigidBody.AddForce(new Vector2(m_DirectionX, 0.0f));
        //m_TestTextUI.text = string.Format("Direction X: {0}", m_DirectionX);
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
