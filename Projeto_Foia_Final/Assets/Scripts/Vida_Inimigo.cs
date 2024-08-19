using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    public int vidaMaxima = 25; // Vida maxima do inimigo
    private int vidaAtual;
    private Animator animador;
    private SpriteRenderer renderizadorSprite;
    private AudioSource audioSource;
    public AudioClip efeitoMorrendo;
    public AudioClip efeitoLevandoDano;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        vidaAtual = vidaMaxima; // Inicializa a vida atual com a vida m�xima
        animador = GetComponent<Animator>(); // Tenta obter o Animator
        renderizadorSprite = GetComponent<SpriteRenderer>(); // Obt�m o SpriteRenderer
    }

    public void ReceberDano(int dano)
    {
        Debug.Log($"Inimigo recebeu dano: {dano}");
        vidaAtual -= dano; // Reduz a vida do inimigo
        audioSource.PlayOneShot(efeitoLevandoDano, 0.5f);
        Debug.Log($"Vida atual do inimigo: {vidaAtual}");

        if (vidaAtual <= 0)
        {
            Debug.Log("Inimigo morreu");
            Morrer();
            audioSource.PlayOneShot(efeitoMorrendo, 0.5f);
        }
        else
        {
            // Verifica se o Animator existe antes de ativar anima��es
            if (animador != null)
            {
                animador.SetBool("LevandoDano", true); // Ativa a anima��o de dano
                StartCoroutine(ResetarLevandoDano()); // Inicia o Coroutine para resetar a anima��o
            }

            StartCoroutine(PiscarVermelho()); // Inicia o Coroutine para piscar vermelho
        }
    }

    IEnumerator ResetarLevandoDano()
    {
        // Aguarda o final da anima��o de LevandoDano antes de voltar ao estado Idle
        if (animador != null)
        {
            yield return new WaitForSeconds(animador.GetCurrentAnimatorStateInfo(0).length);
            animador.SetBool("LevandoDano", false); // Reseta o estado de LevandoDano
        }
    }

    IEnumerator PiscarVermelho()
    {
        // Altera a cor para vermelho
        renderizadorSprite.color = Color.red;
        // Aguarda um curto per�odo de tempo
        yield return new WaitForSeconds(0.1f);
        // Altera a cor de volta para branco
        renderizadorSprite.color = Color.white;
    }

    void Morrer()
    {
        Destroy(gameObject); // Destr�i o objeto ao morrer
    }
}
