using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Panel Popup")]
    public GameObject levelSelectPanel;
    public GameObject settingsPanel;

    public void Play()
    {
        levelSelectPanel.SetActive(true);
    }

    public void CloseLevelSelect()
    {
        levelSelectPanel.SetActive(false);
    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
    }

    public void Level1()
    {
        SceneManager.LoadScene("mobil");
    }

    public void Level2()
    {
        SceneManager.LoadScene("mobillvl2");
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Keluar dari game");
    }
}