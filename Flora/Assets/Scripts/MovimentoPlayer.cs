using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoPlayer : MonoBehaviour
{
    [Header("Configurações de Movimento")]
    public float velocidade = 12f;
    public float jumpforce = 600f;
    private float moveX;
    private Rigidbody2D rb;
    private Animator animator;
    public SpriteRenderer spriteRenderer;
    public Transform PontodeAtaque; // Referência ao ponto de ataque
    private bool facingRight = true; // Variável para controlar a direção que o personagem está virado
    private bool atacando = false; // Variável para controlar se o jogador está atacando

    private float velocidadeOriginal; // Para armazenar a velocidade original

    [Header("Configurações de Dash")]
    [SerializeField] private float dashSpeed = 10f; // Velocidade do dash
    [SerializeField] private float dashDuration = 0.2f; // Duração do dash
    [SerializeField] private float dashCooldown = 3f; // Tempo de cooldown do dash
    private bool isDashing = false; // Variável para controlar se o jogador está realizando um dash
    private bool canDash = true; // Variável para controlar se o jogador pode realizar um dash
    private Vector2 dashDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); // Obtém o componente Animator
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>(); // Obtém o componente SpriteRenderer
        }
        if (PontodeAtaque == null)
        {
            PontodeAtaque = transform.Find("PontodeAtaque"); // Tenta encontrar o ponto de ataque
        }

        if (PontodeAtaque == null)
        {
            Debug.LogError("PontodeAtaque não atribuído e não encontrado!");
        }

        velocidadeOriginal = velocidade; // Armazena a velocidade original
    }

    void Update()
    {
        GetInputs();
    }

    private void FixedUpdate()
    {
        if (!atacando && !isDashing) // Verifica se não está atacando ou dashing
        {
            Mover();
        }
    }

    void GetInputs()
    {
        if (Input.GetButtonDown("Fire1") && !atacando) // Assumindo que o botão de ataque é "Fire1"
        {
            StartCoroutine(Ataque());
        }

        if (Input.GetKey(KeyCode.LeftShift) && canDash && !isDashing) // Verifica se o jogador está pressionando o botão de dash e não está atualmente dashing
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                dashDirection = Vector2.left;
                if (facingRight)
                {
                    Flip();
                }
                StartCoroutine(DashCoroutine());
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                dashDirection = Vector2.right;
                if (!facingRight)
                {
                    Flip();
                }
                StartCoroutine(DashCoroutine());
            }
        }
    }

    void Mover()
    {
        moveX = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveX * velocidade, rb.velocity.y);

        // Ajusta o flip do sprite baseado na direção do movimento
        if (moveX > 0 && !facingRight)
        {
            Flip();
        }
        else if (moveX < 0 && facingRight)
        {
            Flip();
        }

        // Ajusta as animações
        if (Mathf.Abs(moveX) > 0)
        {
            animator.SetBool("Walking", true);
            animator.SetBool("Idle", false);
        }
        else
        {
            animator.SetBool("Walking", false);
            animator.SetBool("Idle", true);
        }
    }

    void Flip()
    {
        facingRight = !facingRight;

        if (spriteRenderer != null)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }
        else
        {
            Debug.LogError("SpriteRenderer não atribuído!");
        }

        // Inverte a posição do ponto de ataque em relação ao personagem
        if (PontodeAtaque != null)
        {
            Vector3 PontodeAtaqueLocalPosition = PontodeAtaque.localPosition;
            PontodeAtaqueLocalPosition.x *= -1;
            PontodeAtaque.localPosition = PontodeAtaqueLocalPosition;
        }
        else
        {
            Debug.LogError("PontodeAtaque não atribuído!");
        }
    }

    public void ReduzirVelocidade(float fator, float duracao)
    {
        StartCoroutine(ReduzirVelocidadeCoroutine(fator, duracao));
    }

    private IEnumerator ReduzirVelocidadeCoroutine(float fator, float duracao)
    {
        velocidade *= fator; // Reduz a velocidade pelo fator fornecido
        yield return new WaitForSeconds(duracao); // Espera pela duração especificada
        velocidade = velocidadeOriginal; // Restaura a velocidade original
    }

    private IEnumerator Ataque()
    {
        atacando = true;
        animator.SetTrigger("Ataque"); // Assumindo que há um trigger de animação chamado "Ataque"
        // Ativar lógica de ataque (por exemplo, detectar colisões com inimigos)
        yield return new WaitForSeconds(0.5f); // Duração do ataque (ajuste conforme necessário)
        atacando = false;
    }

    private IEnumerator DashCoroutine()
    {
        canDash = false; // Impede o jogador de realizar um novo dash enquanto está dashing
        isDashing = true;
        animator.SetTrigger("Dash");
        animator.SetBool("Running", true); // Ativa a animação de Running

        rb.velocity = dashDirection * dashSpeed; // Aplica a velocidade do dash na direção atual
        yield return new WaitForSeconds(dashDuration);

        isDashing = false;
        rb.velocity = Vector2.zero; // Para o movimento após o dash
        animator.SetBool("Running", false); // Desativa a animação de Running

        yield return new WaitForSeconds(dashCooldown); // Aguarda o tempo de cooldown

        canDash = true; // Permite que o jogador possa realizar um novo dash
    }
}
