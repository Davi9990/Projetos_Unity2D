using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoPlayer : MonoBehaviour
{
    [Header("Configura��es de Movimento")]
    public float velocidade = 12f;
    public float jumpforce = 600f;
    private float moveX;
    private Rigidbody2D rb;
    private Animator animator;
    public SpriteRenderer spriteRenderer;
    public Transform PontodeAtaque; // Refer�ncia ao ponto de ataque
    private bool facingRight = true; // Vari�vel para controlar a dire��o que o personagem est� virado
    private bool atacando = false; // Vari�vel para controlar se o jogador est� atacando

    private float velocidadeOriginal; // Para armazenar a velocidade original

    [Header("Configura��es de Dash")]
    [SerializeField] private float dashSpeed = 10f; // Velocidade do dash
    [SerializeField] private float dashDuration = 0.2f; // Dura��o do dash
    [SerializeField] private float dashCooldown = 3f; // Tempo de cooldown do dash
    private bool isDashing = false; // Vari�vel para controlar se o jogador est� realizando um dash
    private bool canDash = true; // Vari�vel para controlar se o jogador pode realizar um dash
    private Vector2 dashDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); // Obt�m o componente Animator
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>(); // Obt�m o componente SpriteRenderer
        }
        if (PontodeAtaque == null)
        {
            PontodeAtaque = transform.Find("PontodeAtaque"); // Tenta encontrar o ponto de ataque
        }

        if (PontodeAtaque == null)
        {
            Debug.LogError("PontodeAtaque n�o atribu�do e n�o encontrado!");
        }

        velocidadeOriginal = velocidade; // Armazena a velocidade original
    }

    void Update()
    {
        GetInputs();
    }

    private void FixedUpdate()
    {
        if (!atacando && !isDashing) // Verifica se n�o est� atacando ou dashing
        {
            Mover();
        }
    }

    void GetInputs()
    {
        if (Input.GetButtonDown("Fire1") && !atacando) // Assumindo que o bot�o de ataque � "Fire1"
        {
            StartCoroutine(Ataque());
        }

        if (Input.GetKey(KeyCode.LeftShift) && canDash && !isDashing) // Verifica se o jogador est� pressionando o bot�o de dash e n�o est� atualmente dashing
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

        // Ajusta o flip do sprite baseado na dire��o do movimento
        if (moveX > 0 && !facingRight)
        {
            Flip();
        }
        else if (moveX < 0 && facingRight)
        {
            Flip();
        }

        // Ajusta as anima��es
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
            Debug.LogError("SpriteRenderer n�o atribu�do!");
        }

        // Inverte a posi��o do ponto de ataque em rela��o ao personagem
        if (PontodeAtaque != null)
        {
            Vector3 PontodeAtaqueLocalPosition = PontodeAtaque.localPosition;
            PontodeAtaqueLocalPosition.x *= -1;
            PontodeAtaque.localPosition = PontodeAtaqueLocalPosition;
        }
        else
        {
            Debug.LogError("PontodeAtaque n�o atribu�do!");
        }
    }

    public void ReduzirVelocidade(float fator, float duracao)
    {
        StartCoroutine(ReduzirVelocidadeCoroutine(fator, duracao));
    }

    private IEnumerator ReduzirVelocidadeCoroutine(float fator, float duracao)
    {
        velocidade *= fator; // Reduz a velocidade pelo fator fornecido
        yield return new WaitForSeconds(duracao); // Espera pela dura��o especificada
        velocidade = velocidadeOriginal; // Restaura a velocidade original
    }

    private IEnumerator Ataque()
    {
        atacando = true;
        animator.SetTrigger("Ataque"); // Assumindo que h� um trigger de anima��o chamado "Ataque"
        // Ativar l�gica de ataque (por exemplo, detectar colis�es com inimigos)
        yield return new WaitForSeconds(0.5f); // Dura��o do ataque (ajuste conforme necess�rio)
        atacando = false;
    }

    private IEnumerator DashCoroutine()
    {
        canDash = false; // Impede o jogador de realizar um novo dash enquanto est� dashing
        isDashing = true;
        animator.SetTrigger("Dash");
        animator.SetBool("Running", true); // Ativa a anima��o de Running

        rb.velocity = dashDirection * dashSpeed; // Aplica a velocidade do dash na dire��o atual
        yield return new WaitForSeconds(dashDuration);

        isDashing = false;
        rb.velocity = Vector2.zero; // Para o movimento ap�s o dash
        animator.SetBool("Running", false); // Desativa a anima��o de Running

        yield return new WaitForSeconds(dashCooldown); // Aguarda o tempo de cooldown

        canDash = true; // Permite que o jogador possa realizar um novo dash
    }
}
