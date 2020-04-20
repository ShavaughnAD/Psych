using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject levelSelect;
    [SerializeField] GameObject settings;
    [SerializeField] GameObject credits;
    [SerializeField] GameObject quitPrompt;

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void GoToLevelName(string _sceneName)
    {
        SceneManager.LoadScene(_sceneName);
    }

    public void GoToLevelIndex(int _buildIndex)
    {
        SceneManager.LoadScene(_buildIndex);
    }

    public void EndGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    public void OpenLevelSelect()
    {
        mainMenu.SetActive(false);
        levelSelect.SetActive(true);
        settings.SetActive(false);
        credits.SetActive(false);
        quitPrompt.SetActive(false);
    }
    public void OpenSettings()
    {
        mainMenu.SetActive(false);
        levelSelect.SetActive(false);
        settings.SetActive(true);
        credits.SetActive(false);
        quitPrompt.SetActive(false);
    }
    public void OpenCredits()
    {
        mainMenu.SetActive(false);
        levelSelect.SetActive(false);
        settings.SetActive(false);
        credits.SetActive(true);
        quitPrompt.SetActive(false);
    }
    public void OpenQuitPrompt()
    {
        mainMenu.SetActive(false);
        levelSelect.SetActive(false);
        settings.SetActive(false);
        credits.SetActive(false);
        quitPrompt.SetActive(true);
    }
    public void ClosePrompts()
    {
        mainMenu.SetActive(true);
        levelSelect.SetActive(false);
        settings.SetActive(false);
        credits.SetActive(false);
        quitPrompt.SetActive(false);
    }
}
