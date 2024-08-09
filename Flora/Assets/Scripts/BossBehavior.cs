using System.Collections;
using UnityEngine;

public class BossBehavior : MonoBehaviour
{
    public float velocidadeDash = 10.0f; // Velocidade do dash
    public float duracaoDash = 0.2f; // Duração do dash e da animação de dash
    public float tempoRecargaDash = 5.0f; // 5 segundos de recarga entre dashes
    public float forcaKnockback = 5.0f;
    public Transform pontoDeteccao;
    public float raioDeteccao = 2.0f;
    public Transform pontoDisparo;
    public float raioDisparo = 5.0f; // Segundo OverlapCircle para detecção de tiro
    public GameObject prefabProjetil; // Prefab do projétil
    public float velocidadeProjetil = 10.0f;

    private Rigidbody2D rb;
    private bool estaDashando = false;
    private bool jogadorDetectado = false;
    private bool jogadorNaAreaDeDisparo = false;
    private Transform jogador;
    private float temporizadorRecargaDash = 0f;

    private Animator animador;
    private bool viradoParaDireita = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D não encontrado!");
        }

        animador = GetComponent<Animator>();
        if (animador == null)
        {
            Debug.LogError("Animator não encontrado!");
        }

        // Encontrar o jogador no início da fase
        jogador = GameObject.FindGameObjectWithTag("Jogador").transform;
        if (jogador == null)
        {
            Debug.LogError("Jogador não encontrado!");
        }

        StartCoroutine(LoopDash());
    }

    void Update()
    {
        DetectarJogador();
        DetectarAreaDeDisparo();
        AtualizarFlip();
    }

    void DetectarJogador()
    {
        Collider2D[] objetosDetectados = Physics2D.OverlapCircleAll(pontoDeteccao.position, raioDeteccao);
        jogadorDetectado = false; // Reset detection flag
        foreach (Collider2D objetoDetectado in objetosDetectados)
        {
            if (objetoDetectado.CompareTag("Jogador"))
            {
                jogadorDetectado = true;
                Debug.Log("Jogador detectado para dash!");
                break;
            }
        }
    }

    void DetectarAreaDeDisparo()
    {
        Collider2D[] objetosDetectados = Physics2D.OverlapCircleAll(pontoDisparo.position, raioDisparo);
        bool jogadorEstavaNaAreaDeDisparo = jogadorNaAreaDeDisparo;
        jogadorNaAreaDeDisparo = false; // Reset detection flag
        foreach (Collider2D objetoDetectado in objetosDetectados)
        {
            if (objetoDetectado.CompareTag("Jogador"))
            {
                jogadorNaAreaDeDisparo = true;
                break;
            }
        }

        if (jogadorEstavaNaAreaDeDisparo && !jogadorNaAreaDeDisparo)
        {
            // Jogador saiu da área de tiro
            StartCoroutine(DispararProjeteis());
        }
    }

    IEnumerator LoopDash()
    {
        while (true)
        {
            if (jogadorDetectado && !estaDashando && temporizadorRecargaDash <= 0)
            {
                yield return StartCoroutine(CarregarEDash());
                temporizadorRecargaDash = tempoRecargaDash;
            }
            temporizadorRecargaDash -= Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator CarregarEDash()
    {
        estaDashando = true;
        rb.velocity = Vector2.zero;
        animador.SetTrigger("Charge"); // Inicia a animação de Charge
        yield return new WaitForSeconds(2.0f); // Tempo de carregamento de 2 segundos

        if (jogadorDetectado)
        {
            Vector2 direcaoDash = (jogador.position - transform.position).normalized;
            animador.SetTrigger("Dash"); // Inicia a animação de Dash

            yield return new WaitForSeconds(0.5f); // Espera um pequeno período para sincronizar com a animação de dash

            rb.velocity = direcaoDash * velocidadeDash;

            Debug.Log($"Executando dash na direção: {direcaoDash}");

            // Espera pela duração da animação de dash
            yield return new WaitForSeconds(duracaoDash);
            rb.velocity = Vector2.zero;
            estaDashando = false;
            animador.ResetTrigger("Dash"); // Certifica-se de que a animação de Dash é resetada

            // Verifica se o jogador ainda está dentro do alcance e aplica knockback
            Collider2D[] jogadoresAtingidos = Physics2D.OverlapCircleAll(pontoDeteccao.position, raioDeteccao);
            foreach (Collider2D jogadorAtingido in jogadoresAtingidos)
            {
                if (jogadorAtingido.CompareTag("Jogador"))
                {
                    AplicarKnockback(jogadorAtingido.transform);
                    jogadorAtingido.GetComponent<HealthSystem>().ReceberDano(1); // Aplica dano ao jogador
                    AplicarKnockback(transform);
                    break;
                }
            }
        }
        else
        {
            estaDashando = false;
        }
    }

    void AplicarKnockback(Transform alvo)
    {
        Rigidbody2D rbAlvo = alvo.GetComponent<Rigidbody2D>();
        if (rbAlvo != null)
        {
            Vector2 direcaoKnockback = (alvo.position - transform.position).normalized;
            rbAlvo.AddForce(direcaoKnockback * forcaKnockback, ForceMode2D.Impulse);
        }
    }

    IEnumerator DispararProjeteis()
    {
        for (int i = 0; i < 1; i++)
        {
            GameObject projetil = Instantiate(prefabProjetil, pontoDisparo.position, Quaternion.identity);
            Rigidbody2D rbProjetil = projetil.GetComponent<Rigidbody2D>();
            Vector2 direcao = (jogador.position - pontoDisparo.position).normalized;
            rbProjetil.velocity = direcao * velocidadeProjetil;

            Debug.Log("Disparando projétil na direção: " + direcao);

            yield return new WaitForSeconds(1.5f);
        }
    }

    void AtualizarFlip()
    {
        if (jogador != null)
        {
            // Verifica constantemente a posição do jogador e ajusta o flip
            if ((jogador.position.x < transform.position.x && !viradoParaDireita) ||
                (jogador.position.x > transform.position.x && viradoParaDireita))
            {
                Flip();
            }
        }
    }

    void Flip()
    {
        viradoParaDireita = !viradoParaDireita;
        Vector3 escalaLocal = transform.localScale;
        escalaLocal.x *= -1;
        transform.localScale = escalaLocal;
    }

    void OnCollisionEnter2D(Collision2D colisao)
    {
        if (colisao.gameObject.CompareTag("Jogador"))
        {
            colisao.gameObject.GetComponent<HealthSystem>().ReceberDano(1); // Aplica dano ao jogador
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(pontoDeteccao.position, raioDeteccao);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(pontoDisparo.position, raioDisparo);
    }
}
