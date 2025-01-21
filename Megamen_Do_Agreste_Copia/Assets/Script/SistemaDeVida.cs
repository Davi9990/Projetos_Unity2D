using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SistemaDeVida : MonoBehaviour
{
    public int vida;  // Vida atual do jogador
    public int vidaMaxima;  // Vida m�xima poss�vel do jogador

    public Image[] Hits;  // Refer�ncias �s imagens de vida no HUD
    public Sprite cheio;  // Sprite para vida cheia
    public Sprite vazio;  // Sprite para vida vazia

    public GameObject hudPrefab; // Refer�ncia ao prefab da HUD
    private GameObject hudInstance; // Inst�ncia atual da HUD

    private void Awake()
    {
        DontDestroyOnLoad(gameObject); // Garante que o sistema de vida persista entre cenas
    }

    private void Start()
    {
        ConfigurarHUD(); // Certifica-se de que a HUD est� configurada
        AtualizarHudDeVida();
    }

    private void Update()
    {
        AtualizarHudDeVida();
        VerificarMorte();
    }

    private void ConfigurarHUD()
    {
        if (hudInstance != null) return;

        hudInstance = GameObject.FindGameObjectWithTag("HUD");

        if (hudInstance == null && hudPrefab != null)
        {
            hudInstance = Instantiate(hudPrefab);
            DontDestroyOnLoad(hudInstance);
        }

        if (hudInstance != null)
        {
            Hits = hudInstance.GetComponentsInChildren<Image>();
        }
    }

    void AtualizarHudDeVida()
    {
        if (Hits == null || Hits.Length == 0) return;

        if (vida > vidaMaxima) vida = vidaMaxima;

        for (int i = 0; i < Hits.Length; i++)
        {
            Hits[i].sprite = i < vida ? cheio : vazio;
            Hits[i].enabled = i < vidaMaxima;
        }
    }

    void VerificarMorte()
    {
        if (vida <= 0)
        {
            FindObjectOfType<PlayerSpawner>().PlayerMorreu();
            Destroy(gameObject); // Opcional: evita m�ltiplas execu��es
        }
    }

    public void GanharVida(int quantidade)
    {
        vida += quantidade;
        if (vida > vidaMaxima) vida = vidaMaxima;
        AtualizarHudDeVida();
    }
}
