using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Panel Popup")]
    public GameObject levelSelectPanel;
    public GameObject settingsPanel;
    public GameObject resetConfirmPanel;
    public GameObject tutorialPanel;

    [Header("Level Button")]
    public Button level2Button;

    [Header("Level 2 Blur")]
    public GameObject level2Blur;

    // Menyimpan level yang dipilih
    private string selectedLevel;

    void Start()
    {
        levelSelectPanel.SetActive(false);
        settingsPanel.SetActive(false);
        resetConfirmPanel.SetActive(false);
        tutorialPanel.SetActive(false);

        int unlockedLevel = SaveGame.GetUnlockedLevel();

        bool unlocked = unlockedLevel >= 2;

        level2Button.interactable = unlocked;
        level2Blur.SetActive(!unlocked);
    }

    // =========================
    // PLAY
    // =========================
    public void Play()
    {
        levelSelectPanel.SetActive(true);
    }

    public void CloseLevelSelect()
    {
        levelSelectPanel.SetActive(false);
    }

    // =========================
    // SETTINGS
    // =========================
    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
    }

    // =========================
    // PILIH LEVEL
    // =========================
    public void Level1()
{
    selectedLevel = "mobil";
    Debug.Log("Level dipilih: " + selectedLevel);
    tutorialPanel.SetActive(true);
}

public void Level2()
{
    selectedLevel = "mobillvl2";
    Debug.Log("Level dipilih: " + selectedLevel);
    tutorialPanel.SetActive(true);
}

public void StartSelectedLevel()
{
    Debug.Log("Load Scene: " + selectedLevel);

    tutorialPanel.SetActive(false);
    SceneManager.LoadScene(selectedLevel);
}

    public void CloseTutorial()
    {
        tutorialPanel.SetActive(false);
    }

    // =========================
    // RESET SAVE
    // =========================
    public void OpenResetConfirm()
    {
        resetConfirmPanel.SetActive(true);
    }

    public void CancelReset()
    {
        resetConfirmPanel.SetActive(false);
    }

    public void ResetProgress()
    {
        SaveGame.ResetSave();

        level2Button.interactable = false;
        level2Blur.SetActive(true);

        resetConfirmPanel.SetActive(false);

        Debug.Log("Progress berhasil direset.");
    }

    // =========================
    // EXIT
    // =========================
    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Keluar dari game");
    }
}