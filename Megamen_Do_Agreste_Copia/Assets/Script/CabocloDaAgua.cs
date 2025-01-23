using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CabocloDaAgua : Todos
{
    public int dano = 1; // Dano causado ao jogador
    public float velocidade = 1; // Velocidade de perseguição horizontal
    public float velocidadeDeDescida = 5f; // Velocidade de descida ao atacar
    public float velocidadeDeSubida = 2f; // Velocidade de subida após o ataque
    public float TempoSubindo = 2f; // Tempo que leva para subir após o ataque
    public float attackCooldown = 2f; // Tempo de espera entre ataques
    public float tempoParadoNoChao = 2f; // Tempo que o inimigo fica parado no chão
    public float gravidadeAlta = 20f; // Gravidade alta durante a descida
    public Transform groundCheck;
    public float groundCheckRadius = 0.1f; // Raio para detectar o chão
    public string groundTag = "Chao"; // Tag para identificar o chão

    public float distanciaDePerseguicao = 5f; // Distância para começar a perseguir o jogador

    private Transform player;
    private SistemaDeVida vid;
    private bool IsAttack = false;
    private bool causouDano = false; // Controle para garantir que o dano só seja aplicado uma vez
    private float lastAttackTime;

    void Start()
    {
        anim = GetComponent<Animator>();
        sp = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        rb.gravityScale = 0; // Inimigo "voa" no começo

        // Inicializa a referência ao jogador
        AtualizarReferenciaPlayer();
    }

    void Update()
    {
        // Atualiza a referência caso o player tenha sido recriado
        if (player == null || vid == null)
        {
            AtualizarReferenciaPlayer();
            return; // Aguarda até que a referência seja atualizada
        }

        // Verifica se o jogador está dentro da distância de perseguição
        if (Vector2.Distance(transform.position, player.position) <= distanciaDePerseguicao)
        {
            if (!IsAttack)
            {
                // Seguir o jogador apenas horizontalmente (eixo x)
                Vector2 targetPosition = new Vector2(player.position.x, transform.position.y);
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, velocidade * Time.deltaTime);
                anim.SetBool("Movendo", true);
                anim.SetBool("Estocada", false);

                // Verifica se o inimigo está alinhado com o jogador no eixo x para atacar
                if (Mathf.Abs(player.position.x - transform.position.x) < 0.5f && Time.time > lastAttackTime + attackCooldown)
                {
                    StartCoroutine(SubindoEDescendo());
                }
            }

            // Verifica se o inimigo está encostando no chão
            if (IsTouchingGround() && !IsAttack)
            {
                StartCoroutine(PararNoChao());
            }
        }
        else
        {
            // Se o jogador estiver fora da distância de perseguição, interrompe o movimento
            anim.SetBool("Movendo", false);
        }
    }

    // Método para atualizar a referência ao Player
    private void AtualizarReferenciaPlayer()
    {
        GameObject jogador = GameObject.FindWithTag("Player");
        if (jogador != null)
        {
            player = jogador.transform; // Atualiza o transform do jogador
            vid = jogador.GetComponent<SistemaDeVida>(); // Atualiza o sistema de vida
        }
    }

    // Corrotina para simular o ataque e movimento de descida/subida
    private IEnumerator SubindoEDescendo()
    {
        IsAttack = true;
        causouDano = false; // Reseta a variável para permitir dano no próximo ataque

        // Aumenta a gravidade para fazê-lo cair rapidamente
        rb.gravityScale = gravidadeAlta;

        // Desce rapidamente para atacar
        rb.velocity = Vector2.down * velocidadeDeDescida;

        anim.SetBool("Movendo", false);
        anim.SetBool("Estocada", true);

        // Espera até atingir o jogador (ou passar por ele)
        while (transform.position.y > player.position.y)
        {
            yield return null;
        }

        // Pausa brevemente para simular o ataque
        rb.velocity = Vector2.zero; // Zera a velocidade após descer
        yield return new WaitForSeconds(0.1f);

        anim.SetBool("Movendo", true);
        anim.SetBool("Estocada", false);

        // Reseta a gravidade para zero para voar de volta para cima
        rb.gravityScale = 0;

        // Sobe lentamente de volta após o ataque
        rb.velocity = Vector2.up * velocidadeDeSubida;

        // Espera o tempo de subida antes de voltar à perseguição
        yield return new WaitForSeconds(TempoSubindo);

        // Retorna à perseguição horizontal
        rb.velocity = Vector2.zero; // Para de se mover verticalmente
        IsAttack = false;
    }

    // Detecta se o inimigo está tocando o chão com base na tag
    private bool IsTouchingGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundCheckRadius);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag(groundTag)) // Verifica se a tag do objeto é igual à tag do chão
            {
                return true;
            }
        }
        return false;
    }

    // Corrotina para fazer o inimigo parar no chão por um tempo
    private IEnumerator PararNoChao()
    {
        Debug.Log("Encostou no chão, ficando estático.");

        // Aumenta ainda mais a gravidade para garantir que ele fique no chão
        rb.gravityScale = gravidadeAlta;

        yield return new WaitForSeconds(tempoParadoNoChao);

        Debug.Log("Voltando a ser dinâmico.");
        // Reseta a gravidade para zero para voltar ao comportamento de voo
        rb.gravityScale = 0;
    }

    // Aplica o dano ao jogador ao colidir durante o ataque
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && IsAttack && !causouDano)
        {
            if (vid != null)
            {
                vid.vida -= dano;
                causouDano = true; // Garante que o dano só seja aplicado uma vez por ataque
            }
        }
    }

    // Desenha o círculo no editor para visualizar o groundCheck
    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, distanciaDePerseguicao);
    }
}
