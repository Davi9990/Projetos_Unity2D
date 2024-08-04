using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Resolvers;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Barra_de_Vida : MonoBehaviour
{
    public int maxHealth = 300;
    public int currentHealth;
    public int defense = 10;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    public Slider healthSlider; // Referência à barra de vida (UI Slider)
    public Transform Barra_de_vida;

    void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;

        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }
    }

    private void Update()
    {
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        SceneManager.LoadScene("Game_Over");
    }

    public void TakeDamage(int damage)
    {
        int realDamage = Mathf.Max(damage - defense, 0);
        currentHealth -= realDamage;

        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
        else
        {
            StartCoroutine(FlashRed());
        }
    }

    IEnumerator FlashRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = originalColor;
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        UpdataHealthBar();
    }

    void UpdataHealthBar()
    {
        if(Barra_de_vida != null)
        {
            Vector3 healthScale = Barra_de_vida.localScale;
            healthScale.x = (float)currentHealth / maxHealth;
            Barra_de_vida.localScale = healthScale;
        }
    }
}
