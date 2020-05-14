using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Begin : MonoBehaviour
{
    private GameObject UI_begin;
    private GameObject UI_settings;
    private void Start()
    {
        UI_begin = GameObject.FindWithTag("UI_Begin");
        UI_settings = GameObject.FindWithTag("UI_Settings");
        UI_settings.SetActive(false);
    }
    public void LoadGame()
    {
        SceneManager.LoadScene(1);
    }
    public void SettingsOnClick()
    {
        UI_begin.SetActive(false);
        UI_settings.SetActive(true);
    }
    public void BackOnClick()
    {
        UI_begin.SetActive(true);
        UI_settings.SetActive(false);
    }
    public void ExitOnClick()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
