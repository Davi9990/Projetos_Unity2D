using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabocloDaAgua : MonoBehaviour
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

    private Rigidbody2D rb;
    private Transform player;
    private SistemaDeVida vida;
    private bool IsAttack = false;
    private bool causouDano = false; // Controle para garantir que o dano só seja aplicado uma vez
    private float lastAttackTime;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0; // Inimigo "voa" no começo
        player = GameObject.FindWithTag("Player").transform; // Localiza o jogador automaticamente
        vida = player.GetComponent<SistemaDeVida>(); // Acessa o sistema de vida do jogador
    }

    void Update()
    {
        if (!IsAttack)
        {
            // Seguir o jogador apenas horizontalmente (eixo x)
            Vector2 targetPosition = new Vector2(player.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, velocidade * Time.deltaTime);

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

    // Corrotina para simular o ataque e movimento de descida/subida
    private IEnumerator SubindoEDescendo()
    {
        IsAttack = true;
        causouDano = false; // Reseta a variável para permitir dano no próximo ataque

        // Aumenta a gravidade para fazê-lo cair rapidamente
        rb.gravityScale = gravidadeAlta;

        // Desce rapidamente para atacar
        rb.velocity = Vector2.down * velocidadeDeDescida;

        // Espera até atingir o jogador (ou passar por ele)
        while (transform.position.y > player.position.y)
        {
            yield return null;
        }

        // Pausa brevemente para simular o ataque
        rb.velocity = Vector2.zero; // Zera a velocidade após descer
        yield return new WaitForSeconds(0.1f);

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
            if (vida != null)
            {
                vida.vida -= dano;
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
    }
}
