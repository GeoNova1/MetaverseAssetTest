using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLoseUI : MonoBehaviour
{
    public static WinLoseUI Instance { get; private set; }

    [Header("Win UI")]
    [SerializeField] GameObject winUI;
    [SerializeField] TMP_Text winTimer;
    [SerializeField] string timerPreface = "Time: ";

    [Header("Lose UI")]
    [SerializeField] GameObject loseUI;
    [SerializeField] TMP_Text loseReason;

    [Header("Other")]
    [SerializeField] List<GameObject> uiToDisable;

    private void Awake()
    {
        Instance = this;
        gameObject.SetActive(false);
    }

    public void Win()
    {
        gameObject.SetActive(true);
        winUI.SetActive(true);

        TimeSpan time = TimeSpan.FromSeconds(Time.timeSinceLevelLoad);
        winTimer.text = timerPreface + time.ToString(@"mm\:ss\.ff");

        foreach (var go in uiToDisable)
            go.SetActive(false);

        Time.timeScale = 0f;
    }

    public void Lose(string reason = "")
    {
        gameObject.SetActive(true);
        loseUI.SetActive(true);
        loseReason.text = reason;

        foreach (var go in uiToDisable)
            go.SetActive(false);

        Time.timeScale = 0f;
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
