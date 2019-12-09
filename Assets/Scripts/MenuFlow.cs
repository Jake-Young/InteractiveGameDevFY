using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuFlow : MonoBehaviour
{
    public void OnPlayClick()
    {
        SceneManager.LoadScene(1);
    }

    public void OnSettingsClick()
    {
        SceneManager.LoadScene(2);
    }

    public void OnStoreClick()
    {
        SceneManager.LoadScene(3);
    }
}
