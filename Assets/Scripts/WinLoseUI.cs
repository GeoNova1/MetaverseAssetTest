using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLoseUI : MonoBehaviour
{
    public static WinLoseUI Instance { get; private set; }

    [SerializeField] GameObject winUI;
    [SerializeField] GameObject loseUI;

    private void Awake()
    {
        Instance = this;
        gameObject.SetActive(false);
    }

    public void Win()
    {
        gameObject.SetActive(true);
        winUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Lose()
    {
        gameObject.SetActive(true);
        loseUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
