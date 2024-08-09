using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class HealthSystem : MonoBehaviour
{
    public int vidaMaxima = 10; // Máximo de vidas
    public TextMeshProUGUI textoVidas; // Referência para o TextMeshPro que exibirá as vidas

    private int vidaAtual; // Vidas atuais
    private Vector3 posicaoCheckpoint; // Posição de checkpoint do jogador
    private SpriteRenderer renderizadorSprite;
    private Animator animador;
    private MovimentoPlayer movimentoJogador;

    private bool invencivel = false;
    private int revives = 1; // Contador de revives

    void Start()
    {
        // Inicializa o checkpoint na posição inicial do jogador
        posicaoCheckpoint = transform.position;
        vidaAtual = vidaMaxima; // Inicializa com o máximo de vidas
        AtualizarTextoVidas();

        renderizadorSprite = GetComponent<SpriteRenderer>();
        if (renderizadorSprite == null)
        {
            Debug.LogError("SpriteRenderer não encontrado no jogador!");
        }

        animador = GetComponent<Animator>();
        if (animador == null)
        {
            Debug.LogError("Animator não encontrado no jogador!");
        }

        movimentoJogador = GetComponent<MovimentoPlayer>();
        if (movimentoJogador == null)
        {
            Debug.LogError("Script MovimentoPlayer não encontrado no jogador!");
        }
    }

    void Update()
    {
        AtualizarTextoVidas();
    }

    public void AtualizarTextoVidas()
    {
        textoVidas.text = "" + vidaAtual;
    }

    void GameOver()
    {
        SceneManager.LoadScene("Game_Over");
    }

    public void ReceberDano(int dano)
    {
        if (!invencivel)
        {
            Debug.Log("Recebendo dano: " + dano);
            vidaAtual -= dano;
            AtualizarTextoVidas();
            animador.SetTrigger("TomarDano");
            if (vidaAtual <= 0)
            {
                Debug.Log("Vida do jogador esgotada.");
                Reviver();
            }
            else
            {
                movimentoJogador.ReduzirVelocidade(0.5f, 1f); // Reduz a velocidade do jogador pela metade por 1 segundo
                StartCoroutine(Invencibilidade(2f));
            }
            StartCoroutine(PiscarSprite());
        }
        else
        {
            Debug.Log("Jogador está invencível. Dano não aplicado.");
        }
    }

    public void GanharVida(int quantidade)
    {
        vidaAtual += quantidade;
        if (vidaAtual > vidaMaxima)
        {
            vidaAtual = vidaMaxima;
        }
        AtualizarTextoVidas();
    }

    private IEnumerator PiscarSprite()
    {
        if (renderizadorSprite != null)
        {
            for (int i = 0; i < 10; i++) // Piscar rapidamente por 1 segundo
            {
                renderizadorSprite.color = Color.yellow; // Muda a cor para amarelo
                yield return new WaitForSeconds(0.1f); // Espera um curto período de tempo
                renderizadorSprite.color = Color.white; // Muda a cor de volta para branco
                yield return new WaitForSeconds(0.1f); // Espera um curto período de tempo
            }
        }
    }

    private IEnumerator Invencibilidade(float duracao)
    {
        invencivel = true;
        Debug.Log("Jogador está invencível.");
        yield return new WaitForSeconds(duracao);
        invencivel = false;
        Debug.Log("Invencibilidade do jogador terminou.");
    }

    public void SetCheckpoint(Vector3 novaPosicaoCheckpoint)
    {
        posicaoCheckpoint = novaPosicaoCheckpoint;
        Debug.Log("Checkpoint atualizado para: " + posicaoCheckpoint);
    }

    private void Reviver()
    {
        if (revives > 0)
        {
            revives--; // Reduzir o contador de revives
            transform.position = posicaoCheckpoint;
            vidaAtual = vidaMaxima;
            AtualizarTextoVidas();
            Debug.Log("Jogador revivido no checkpoint: " + posicaoCheckpoint);
        }
        else
        {
            GameOver();
        }
    }
}
