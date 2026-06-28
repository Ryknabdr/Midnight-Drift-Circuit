using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject tutorialPanel;

    void Start()
    {
        // Tutorial hanya muncul sekali
        if (PlayerPrefs.GetInt("TutorialShown", 0) == 0)
        {
            tutorialPanel.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            tutorialPanel.SetActive(false);
        }
    }

    public void StartGame()
    {
        tutorialPanel.SetActive(false);

        PlayerPrefs.SetInt("TutorialShown", 1);
        PlayerPrefs.Save();

        Time.timeScale = 1f;
    }
}