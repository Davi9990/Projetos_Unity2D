using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Game rules")]
    public int maxLives = 3;
    public int coinsToWin = 50;

    [Header("References")]
    public UIManager uiManager;

    int currentLives;
    int coinsCollected = 0;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        currentLives = maxLives;
    }

    void Start()
    {
        UpdateUI();
    }

    public void PlayerHit()
    {
        currentLives--;
        UpdateUI();
        if (currentLives <= 0) GameOver();
    }

    public void AddCoin(int amount = 1)
    {
        coinsCollected += amount;
        UpdateUI();
        if (coinsCollected >= coinsToWin)
        {
            Win();
        }
    }

    void UpdateUI()
    {
        if (uiManager != null)
            uiManager.UpdateLivesAndCoins(currentLives, coinsCollected);
    }

    void GameOver()
    {
        if (uiManager != null) uiManager.ShowGameOver();
        // pause game
        Time.timeScale = 0f;
    }

    void Win()
    {
        if (uiManager != null) uiManager.ShowWin();
        Time.timeScale = 0f;
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
