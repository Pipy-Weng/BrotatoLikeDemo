using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject gameOverPanel;

    public static GameManager Instance;

    bool gameRunning = true;

    private void Awake()
    {
        if (Instance == null) { Instance = this; }
    }

    public bool IsGameRunning() => gameRunning;

    public void GameOver()
    {
        gameRunning = false;
        gameOverPanel.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1.0f;
    }

}
