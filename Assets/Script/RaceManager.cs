using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class RaceManager : MonoBehaviour
{
    public TMP_Text timeText;
    public TMP_Text lapText;
    public TMP_Text finishTimeText;

    public GameObject finishPanel;

    private int totalLap;
    private int currentLap = 0;
    private float timer;

    private bool raceStarted = false;
    private bool raceFinished = false;

    void Start()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        if (sceneName == "mobil")
            totalLap = 2;
        else if (sceneName == "mobillvl2")
            totalLap = 3;

        finishPanel.SetActive(false);

        UpdateTimeText();
        UpdateLapText();
    }

    void Update()
    {
        if (!raceStarted || raceFinished) return;

        timer += Time.deltaTime;
        UpdateTimeText();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

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