using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class RaceManager : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text timeText;
    public TMP_Text lapText;
    public TMP_Text finishTimeText;
    public TMP_Text countdownText;

    public GameObject finishPanel;

    [Header("Player")]
    public mobil playerCar;

    [Header("NPC")]
    public AICarController[] npcCars;

    private int totalLap;
    private int currentLap = 0;
    private float timer;

    private bool raceStarted = false;
    private bool raceFinished = false;
    private bool countdownFinished = false;

    void Start()
{
    Cursor.visible = true;
    Cursor.lockState = CursorLockMode.None;

    string sceneName = SceneManager.GetActiveScene().name;

    if (sceneName == "mobil")
        totalLap = 2;
    else if (sceneName == "mobillvl2")
        totalLap = 3;

    finishPanel.SetActive(false);

    UpdateTimeText();
    UpdateLapText();

    StartCoroutine(StartCountdown());
}

    void Update()
    {
        if (!countdownFinished)
            return;

        if (!raceStarted || raceFinished)
            return;

        timer += Time.deltaTime;
        UpdateTimeText();
    }

    IEnumerator StartCountdown()
    {
        // Matikan kontrol Player
        if (playerCar != null)
            playerCar.enabled = false;

        // Matikan semua NPC
        foreach (AICarController npc in npcCars)
        {
            if (npc != null)
                npc.enabled = false;
        }

        countdownText.gameObject.SetActive(true);

        countdownText.text = "3";
        yield return new WaitForSeconds(1f);

        countdownText.text = "2";
        yield return new WaitForSeconds(1f);

        countdownText.text = "1";
        yield return new WaitForSeconds(1f);

        countdownText.text = "GO!";
        yield return new WaitForSeconds(1f);

        countdownText.gameObject.SetActive(false);

        // Aktifkan Player
        if (playerCar != null)
            playerCar.enabled = true;

        // Aktifkan semua NPC
        foreach (AICarController npc in npcCars)
        {
            if (npc != null)
                npc.enabled = true;
        }

        countdownFinished = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!countdownFinished)
            return;

        if (!other.CompareTag("Player"))
            return;

        if (!raceStarted)
        {
            raceStarted = true;
            return;
        }

        if (currentLap < totalLap)
        {
            currentLap++;
            UpdateLapText();
        }
        else
        {
            FinishRace();
        }
    }

    void FinishRace()
    {
        raceFinished = true;

        // Unlock Level 2 setelah menyelesaikan Level 1
        if (SceneManager.GetActiveScene().name == "mobil")
        {
            SaveGame.UnlockLevel(2);
        }

        lapText.text = "FINISH!";
        finishPanel.SetActive(true);

        int minutes = Mathf.FloorToInt(timer / 60);
        int seconds = Mathf.FloorToInt(timer % 60);

        finishTimeText.text = "Time : " + minutes.ToString("00") + ":" + seconds.ToString("00");

        Time.timeScale = 0f;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void NextLevel()
    {
        Time.timeScale = 1f;

        if (SceneManager.GetActiveScene().name == "mobil")
            SceneManager.LoadScene("mobillvl2");
        else
            SceneManager.LoadScene("mainmenu");
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("mainmenu");
    }

    void UpdateTimeText()
    {
        int minutes = Mathf.FloorToInt(timer / 60);
        int seconds = Mathf.FloorToInt(timer % 60);

        timeText.text = "Time : " + minutes.ToString("00") + ":" + seconds.ToString("00");
    }

    void UpdateLapText()
    {
        lapText.text = "Lap : " + currentLap + "/" + totalLap;
    }
}