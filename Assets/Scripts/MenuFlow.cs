using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuFlow : MonoBehaviour
{

    private enum m_LoadedScenes { MainMenu = 0, Settings = 1, Store = 2, Level_1 = 3};

    public void OnPlayClick()
    {
        SceneManager.LoadScene((int)m_LoadedScenes.Level_1);
    }

    public void OnSettingsClick()
    {
        SceneManager.LoadScene((int)m_LoadedScenes.Settings);
    }

    public void OnStoreClick()
    {
        SceneManager.LoadScene((int)m_LoadedScenes.Store);
    }

    public void OnBackClick()
    {
        SceneManager.LoadScene((int)m_LoadedScenes.MainMenu);
    }

    public void OnRetryClick()
    {
        SceneManager.LoadScene((int)m_LoadedScenes.Level_1);
    }
}
