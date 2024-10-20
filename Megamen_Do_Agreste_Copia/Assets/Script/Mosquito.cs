using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mosquito : MonoBehaviour
{
    public int dano = 1;                       // Dano que o mosquito causa
    public float velocidade = 5f;               // Velocidade do mosquito
    public float TempoChupando = 3f;            // Tempo que o mosquito chupa o jogador
    public float razanteHeight = 2f;            // Altura do razante
    public float distanciaParaRazante = 5f;     // Dist�ncia para iniciar o razante

    private bool chupando = false;              // Se o mosquito est� chupando
    private Rigidbody2D rb;                     // Rigidbody do mosquito
    public Transform jogador;                   // Refer�ncia ao transform do jogador
    private SistemaDeVida vida;                 // Sistema de vida do jogador

    private bool podeAgarrar = false; // Controla se o mosquito pode agarrar o jogador


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (jogador == null) return; // Certifique-se de que o jogador est� atribu�do

        float distancia = Vector2.Distance(transform.position, jogador.position);

        // Mova em dire��o ao jogador se estiver longe ou se estiver chupando
        if (!chupando && distancia > distanciaParaRazante)
        {
            SeguirJogador();
        }
        else if (chupando)
        {
            // Se o mosquito est� chupando, mova-o em dire��o ao jogador
            AgarrarJogador(jogador);
        }
        else if (podeAgarrar && distancia <= distanciaParaRazante)
        {
            // Apenas agarre o jogador se ele estiver em contato e se o mosquito puder agarrar
            AgarrarJogador(jogador);
        }
    }

    void SeguirJogador()
    {
        // Move o mosquito na dire��o do jogador
        Vector2 targetPosition = jogador.position;
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, velocidade * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Ativar a possibilidade de agarrar o jogador
            podeAgarrar = true;
            jogador = other.transform; // Armazena a refer�ncia do jogador
            vida = jogador.GetComponent<SistemaDeVida>(); // Obt�m o sistema de vida do jogador

            if (vida != null && !chupando)
            {
                // Inicia o razante e o ataque ao jogador
                StartCoroutine(Razante());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Se o jogador sair da colis�o, limpa a refer�ncia e para o chupando
            jogador = null; // Limpa a refer�ncia do jogador
            chupando = false; // Reseta o estado de chupando
            podeAgarrar = false; // Reseta a possibilidade de agarrar

            // Inicia a persegui��o ao jogador imediatamente ap�s solt�-lo
            StartCoroutine(PerseguirJogador());
        }
    }

    private IEnumerator PerseguirJogador()
    {
        while (true) // Loop infinito para seguir o jogador
        {
            if (jogador == null) // Se n�o houver jogador, procura novamente
            {
                GameObject jogadorEncontrado = GameObject.FindWithTag("Player");
                if (jogadorEncontrado != null)
                {
                    jogador = jogadorEncontrado.transform; // Atualiza a refer�ncia do jogador
                }
            }

            if (jogador != null)
            {
                SeguirJogador(); // Chama a fun��o para seguir o jogador
            }

            yield return null; // Espera um frame antes de verificar novamente
        }
    }

    private IEnumerator Razante()
    {
        chupando = true; // Define que o mosquito est� chupando

        // Agarrar o jogador imediatamente ao colidir
        AgarrarJogador(jogador);

        Vector2 startPosition = transform.position; // Posi��o inicial do razante
        Vector2 targetPosition = jogador.position; // Posi��o do jogador
        float progress = 0f; // Progresso do movimento

        // Executa o razante
        while (progress <= 1f)
        {
            progress += Time.deltaTime; // Ajuste para dura��o do razante
            Vector2 straightLine = Vector2.Lerp(startPosition, targetPosition, progress);
            float curve = Mathf.Sin(progress * Mathf.PI) * razanteHeight; // Eleva a altura do movimento
            transform.position = new Vector2(straightLine.x, straightLine.y + curve);
            yield return null; // Espera o pr�ximo frame
        }

        // Aplica dano enquanto o mosquito est� chupando
        yield return StartCoroutine(ChupandoJogador());

        // Libertar o jogador ap�s o tempo de chupada
        LibertarJogador();
        chupando = false; // Reseta o estado de chupando

        // Verifica se ainda h� um jogador para seguir
        if (jogador != null)
        {
            SeguirJogador();
        }
    }

    private IEnumerator ChupandoJogador()
    {
        float tempoPassado = 0f;

        // Enquanto o tempo de chupada n�o acabar
        while (tempoPassado < TempoChupando)
        {
            if (vida == null) // Verifica se o sistema de vida n�o est� nulo
            {
                vida = jogador.GetComponent<SistemaDeVida>();
            }

            if (vida != null)
            {
                vida.vida -= dano; // Aplica dano ao jogador
            }

            tempoPassado += 1f; // Conta 1 segundo
            yield return new WaitForSeconds(1f); // Espera 1 segundo entre os danos
        }
    }

    private void AgarrarJogador(Transform jogador)
    {
        if (jogador != null)
        {
            jogador.SetParent(transform); // Torna o jogador filho do mosquito
            Rigidbody2D playerRb = jogador.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                playerRb.velocity = Vector2.zero; // Para qualquer movimento do jogador
                playerRb.constraints = RigidbodyConstraints2D.FreezeAll; // Congela o jogador
            }
        }
    }

    private void LibertarJogador()
    {
        if (jogador != null)
        {
            jogador.SetParent(transform); // Torna o jogador filho do mosquito
            Rigidbody2D playerRb = jogador.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                playerRb.velocity = Vector2.zero; // Para qualquer movimento do jogador
                playerRb.constraints = RigidbodyConstraints2D.FreezeAll; // Congela o jogador
            }
            chupando = true; // Inicia o estado de chupando
        }
    }
}
