using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        // Garante que o sistema de vida e a HUD n�o ser�o destru�dos ao carregar novas cenas
        DontDestroyOnLoad(gameObject);
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
        // Se a HUD j� foi criada, n�o faz nada
        if (hudInstance != null)
            return;

        // Procura a HUD na cena
        hudInstance = GameObject.FindGameObjectWithTag("HUD");

        // Se a HUD n�o foi encontrada, instancia o prefab
        if (hudInstance == null && hudPrefab != null)
        {
            hudInstance = Instantiate(hudPrefab);
            DontDestroyOnLoad(hudInstance); // Preserva a HUD entre cenas
        }

        // Configura o array de Hits (imagens de vida) com base na HUD
        if (hudInstance != null)
        {
            Hits = hudInstance.GetComponentsInChildren<Image>();
        }
    }

    void AtualizarHudDeVida()
    {
        if (Hits == null || Hits.Length == 0)
            return;

        if (vida > vidaMaxima)
        {
            vida = vidaMaxima;
        }

        for (int i = 0; i < Hits.Length; i++)
        {
            if (i < vida)
            {
                Hits[i].sprite = cheio;
            }
            else
            {
                Hits[i].sprite = vazio;
            }

            Hits[i].enabled = i < vidaMaxima;
        }
    }

    void VerificarMorte()
    {
        if (vida <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void GanharVida(int quantidade)
    {
        vida += quantidade;
        if (vida > vidaMaxima)
        {
            vida = vidaMaxima;
        }
        AtualizarHudDeVida();
    }
}
