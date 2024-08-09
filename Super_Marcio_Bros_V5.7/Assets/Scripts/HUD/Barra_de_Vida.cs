using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Resolvers;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Diagnostics;

public class Barra_de_Vida : MonoBehaviour
{
    public int maxHealth = 300;
    public int currentHealth;
    public int defense = 10;
    public TextMeshProUGUI vidasText;
    private bool isDead = false;
    public Cronometro countdownTimer;
    public Camera cameraController;
    private PlayerPowerManeger powerManager;

    void Start()
    {
        currentHealth = maxHealth;
        powerManager = GetComponent<PlayerPowerManeger>();
        UpdateVidasText();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Update()
    {
        if (currentHealth <= 0 && !isDead)
        {
            Die();
        }
    }

    public void TakeDamage(int damage)
    {
        int realDamage = Mathf.Max(damage - defense, 0);
        currentHealth -= realDamage;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
        else
        {
            if (powerManager != null)
            {
                powerManager.RevertPlayer();
            }
        }
    }

    public void Die()
    {
        if (isDead) return;
        isDead = true;
        GameManeger2.vidas--;
        UpdateVidasText();

        if (GameManeger2.vidas > 0)
        {
            Respawn();
        }
        else
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
        }
    }

    public void IncreaseHealth(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public void UpdateVidasText()
    {
        if (vidasText != null)
        {
            vidasText.text = GameManeger2.vidas.ToString();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Checkpoint"))
        {
            CheckPointManager.Instance.RespawnPosition = other.transform.position;
        }
    }

    private void Respawn()
    {
        transform.position = CheckPointManager.Instance.RespawnPosition;
        currentHealth = maxHealth;
        isDead = false;

        if (cameraController != null)
        {
            cameraController.GetComponent<Camera>().UpdatePlayer(transform);
        }

        if (countdownTimer != null)
        {
            countdownTimer.ResetTimer(countdownTimer.timeRemaining);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        UpdateVidasText();

        if (CheckPointManager.Instance != null)
        {
            transform.position = CheckPointManager.Instance.RespawnPosition;
        }
    }
}