using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text livesText;
    public Text coinsText;
    public GameObject gameOverPanel;
    public GameObject winPanel;
    public Button restartButton;
    public Button jumpUIButton; // optional: set to call Player.JumpFromButton()

    void Start()
    {
        restartButton.onClick.AddListener(OnRestart);
        gameOverPanel.SetActive(false);
        winPanel.SetActive(false);
    }

    public void UpdateLivesAndCoins(int lives, int coins)
    {
        if (livesText) livesText.text = $"Vidas: {lives}";
        if (coinsText) coinsText.text = $"Moedas: {coins}";
    }

    public void ShowGameOver()
    {
        gameOverPanel.SetActive(true);
    }

    public void ShowWin()
    {
        winPanel.SetActive(true);
    }

    public void OnRestart()
    {
        GameManager.Instance.Restart();
    }
}
