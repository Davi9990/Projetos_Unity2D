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

    void Start()
    {
        AtualizarHudDeVida();  // Atualiza o HUD assim que o jogo come�a
    }

    void Update()
    {
        AtualizarHudDeVida();
        VerificarMorte();
    }

    void AtualizarHudDeVida()
    {
        // Se a vida estiver maior que a vida m�xima, ajusta para a vida m�xima
        if (vida > vidaMaxima)
        {
            vida = vidaMaxima;
        }

        // Atualiza cada barra de vida no HUD
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

            // Ativa ou desativa a barra de vida dependendo do valor da vida m�xima
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
}
