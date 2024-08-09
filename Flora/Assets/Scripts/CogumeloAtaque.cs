using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CogumeloAtaque : MonoBehaviour
{
    public Transform jogador; // Refer�ncia ao transform do jogador
    public float distanciaSeguimento = 10f; // Dist�ncia em que o inimigo come�a a seguir o jogador
    public float velocidade = 2f; // Velocidade de movimento do inimigo
    public int dano = 1; // Dano que o inimigo causa ao jogador (ajustado para 1)
    public float tempoRecargaAtaque = 3f; // Tempo de recarga entre ataques
    private float ultimoTempoAtaque;
    private float velocidadeOriginal; // Para armazenar a velocidade original
    private bool podeAtacar = true; // Controla se o inimigo pode atacar

    private Rigidbody2D rb;
    private SpriteRenderer renderizadorSprite; // Refer�ncia ao SpriteRenderer para o flip
    private Animator animador;
    private bool estaMovendo = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animador = GetComponent<Animator>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D n�o encontrado no inimigo!");
        }

        renderizadorSprite = GetComponent<SpriteRenderer>();
        if (renderizadorSprite == null)
        {
            Debug.LogError("SpriteRenderer n�o encontrado no inimigo!");
        }

        velocidadeOriginal = velocidade; // Armazena a velocidade original
    }

    void Update()
    {
        // Calcula a dist�ncia entre o inimigo e o jogador
        float distanciaParaJogador = Vector2.Distance(transform.position, jogador.position);

        // Se a dist�ncia for menor ou igual � distanciaSeguimento, o inimigo segue o jogador
        if (distanciaParaJogador <= distanciaSeguimento)
        {
            // Move o inimigo na dire��o do jogador
            Vector2 direcao = (jogador.position - transform.position).normalized;
            transform.position += (Vector3)(direcao * velocidade * Time.deltaTime);

            if (!estaMovendo)
            {
                estaMovendo = true;
                animador.SetBool("Walking", true);
            }

            // Ajusta o flip do sprite baseado na dire��o do movimento
            if (direcao.x > 0 && renderizadorSprite.flipX)
            {
                Virar();
            }
            else if (direcao.x < 0 && !renderizadorSprite.flipX)
            {
                Virar();
            }
        }
        else
        {
            if (estaMovendo)
            {
                estaMovendo = false;
                animador.SetBool("Walking", false);
            }
        }
    }

    void Virar()
    {
        renderizadorSprite.flipX = !renderizadorSprite.flipX;
    }

    void OnCollisionEnter2D(Collision2D colisao)
    {
        if (colisao.gameObject.CompareTag("Jogador") && podeAtacar)
        {
            AplicarDano(colisao);
        }
    }

    void OnCollisionStay2D(Collision2D colisao)
    {
        if (colisao.gameObject.CompareTag("Jogador") && podeAtacar)
        {
            AplicarDano(colisao);
        }
    }

    void AplicarDano(Collision2D colisao)
    {
        HealthSystem vida = colisao.gameObject.GetComponent<HealthSystem>();
        MovimentoPlayer movimento = colisao.gameObject.GetComponent<MovimentoPlayer>();
        if (vida != null && movimento != null)
        {
            Debug.Log("Player alcan�ado");
            vida.ReceberDano(dano);
            movimento.ReduzirVelocidade(0.5f, 1f); // Reduz a velocidade do jogador pela metade por 1 segundo
            podeAtacar = false;
            Debug.Log("Ataque aplicado, cooldown iniciado.");
            StartCoroutine(RecargaAtaque());
        }
        else
        {
            if (vida == null)
            {
                Debug.LogError("Componente HealthSystem n�o encontrado no jogador.");
            }
            if (movimento == null)
            {
                Debug.LogError("Componente MovimentoPlayer n�o encontrado no jogador.");
            }
        }
    }

    IEnumerator RecargaAtaque()
    {
        yield return new WaitForSeconds(tempoRecargaAtaque);
        podeAtacar = true;
        Debug.Log("Cooldown de ataque finalizado.");
    }
}
