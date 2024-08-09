using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    public int vidaMaxima = 25; // Vida maxima do inimigo
    private int vidaAtual;
    private Animator animador;
    private SpriteRenderer renderizadorSprite;

    void Start()
    {
        vidaAtual = vidaMaxima; // Inicializa a vida atual com a vida máxima
        animador = GetComponent<Animator>();
        renderizadorSprite = GetComponent<SpriteRenderer>();
    }

    public void ReceberDano(int dano)
    {
        Debug.Log($"Inimigo recebeu dano: {dano}");
        vidaAtual -= dano; // Reduz a vida do inimigo
        Debug.Log($"Vida atual do inimigo: {vidaAtual}");

        if (vidaAtual <= 0)
        {
            Debug.Log("Inimigo morreu");
            Morrer();
        }
        else
        {
            animador.SetBool("LevandoDano", true); // Usa uma bool para ativar a animação de dano
            StartCoroutine(ResetarLevandoDano()); // Inicia o Coroutine para resetar a animação
            StartCoroutine(PiscarVermelho()); // Inicia o Coroutine para piscar vermelho
        }
    }

    IEnumerator ResetarLevandoDano()
    {
        // Aguarda o final da animação de LevandoDano antes de voltar ao estado Idle
        yield return new WaitForSeconds(animador.GetCurrentAnimatorStateInfo(0).length);
        animador.SetBool("LevandoDano", false); // Reseta o estado de LevandoDano
    }

    IEnumerator PiscarVermelho()
    {
        // Altera a cor para vermelho
        renderizadorSprite.color = Color.red;
        // Aguarda um curto período de tempo
        yield return new WaitForSeconds(0.1f);
        // Altera a cor de volta para branco
        renderizadorSprite.color = Color.white;
    }

    void Morrer()
    {
        
        Destroy(gameObject);
    }
}
