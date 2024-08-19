using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoPlayer : MonoBehaviour
{
    [Header("Configurações de Movimento")]
    public float velocidade = 12f;
    private float moveX;
    private Rigidbody2D rb;
    private Animator animator;
    public SpriteRenderer spriteRenderer;
    public Transform groundCheck; // GroundCheck é um filho do Player
    public LayerMask chao0;
    public Transform PontoDeAtaque; // PontoDeAtaque também é um filho do Player
    public ParticleSystem dust;
    private AudioSource efeitosAudioSource; // Fonte de áudio para efeitos sonoros
    private AudioSource musicaAudioSource;  // Fonte de áudio para a música da fase
    public AudioClip efeitoPulo;
    public AudioClip efeitoDash;
    public AudioClip musicaFase;
    private bool facingRight = true;

    private float velocidadeOriginal;
    private bool chao1;
    public int pulosMax = 2; // Número máximo de pulos
    private int pulosRestantes; // Número de pulos restantes

    [Header("Configurações de Pulo")]
    public float jumpForce = 10f;
    public float groundCheckRadius = 0.2f; // Ajuste do raio de verificação do chão

    [Header("Configurações de Dash")]
    [SerializeField] private float dashSpeed = 10f;
    [SerializeField] private float dashDuration = 0.2f;
    [SerializeField] private float dashCooldown = 3f;
    private bool isDashing = false;
    private bool canDash = true;
    private Vector2 dashDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // Adicionando fontes de áudio
        efeitosAudioSource = gameObject.AddComponent<AudioSource>();
        musicaAudioSource = gameObject.AddComponent<AudioSource>();

        // Tocando a música da fase na fonte correta
        musicaAudioSource.clip = musicaFase;
        musicaAudioSource.volume = 0.1f;
        musicaAudioSource.loop = true;
        musicaAudioSource.Play();

        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        if (groundCheck == null)
        {
            Debug.LogError("GroundCheck não atribuído! Certifique-se de ter um GroundCheck como filho do Player.");
        }

        if (PontoDeAtaque == null)
        {
            Debug.LogError("PontoDeAtaque não atribuído! Certifique-se de ter um PontoDeAtaque como filho do Player.");
        }

        velocidadeOriginal = velocidade;
        pulosRestantes = pulosMax; // Inicializa com o número máximo de pulos
    }

    void Update()
    {
        GetInputs();
        if (!isDashing)
        {
            Pular();
        }
        else if (Input.GetButtonDown("Jump")) // Cancela o dash e pula se o jogador pressionar o botão de pulo durante o dash
        {
            CancelarDashEExecutarPulo();
        }
    }

    private void FixedUpdate()
    {
        if (!isDashing)
        {
            Mover();
        }
    }

    void GetInputs()
    {
        // Dash logic
        if (Input.GetKey(KeyCode.LeftShift) && canDash && !isDashing)
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

        if (moveX > 0 && !facingRight)
        {
            Flip();
        }
        else if (moveX < 0 && facingRight)
        {
            Flip();
        }

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

    void Pular()
    {
        // Verificação do chão feita no Update para garantir responsividade
        chao1 = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, chao0);

        // Se o jogador está no chão, resetamos o número de pulos
        if (chao1)
        {
            pulosRestantes = pulosMax;
            animator.SetBool("Jumping", false);
            animator.SetBool("Idle", moveX == 0);
            animator.SetBool("Falling", false);
            animator.SetBool("Walking", moveX != 0);
            animator.SetBool("Running", false);
        }

        // Lógica de pulo: permite pular apenas se houver pulos restantes
        if (Input.GetButtonDown("Jump") && pulosRestantes > 0)
        {
            efeitosAudioSource.PlayOneShot(efeitoPulo, 0.1f);
            rb.velocity = new Vector2(rb.velocity.x, jumpForce); // Aplica apenas força no eixo Y
            animator.SetTrigger("Jump");
            CreateDust();
            pulosRestantes--; // Reduz o número de pulos restantes
        }

        // Atualiza animações de queda
        if (!chao1 && rb.velocity.y < 0)
        {
            animator.SetBool("Falling", true);
        }
        else if (rb.velocity.y > 0)
        {
            animator.SetBool("Jumping", true);
            animator.SetBool("Falling", false);
            animator.SetBool("Walking", false);
            animator.SetBool("Running", false);

        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        spriteRenderer.flipX = !spriteRenderer.flipX;
        if (PontoDeAtaque != null)
        {
            CreateDust();
            Vector3 PontoDeAtaqueLocalPosition = PontoDeAtaque.localPosition;
            PontoDeAtaqueLocalPosition.x *= -1;
            PontoDeAtaque.localPosition = PontoDeAtaqueLocalPosition;
        }
    }

    public void ReduzirVelocidade(float fator, float duracao)
    {
        StopCoroutine(ReduzirVelocidadeCoroutine(fator, duracao)); // Garante que não haja coroutines múltiplas de lentidão rodando
        StartCoroutine(ReduzirVelocidadeCoroutine(fator, duracao));
    }

    private IEnumerator ReduzirVelocidadeCoroutine(float fator, float duracao)
    {
        float velocidadeAtual = velocidade;
        velocidade = velocidadeOriginal * fator; // Aplica a redução da velocidade

        yield return new WaitForSeconds(duracao); // Aguarda a duração especificada (5 segundos, por exemplo)

        velocidade = velocidadeOriginal; // Restaura a velocidade original após a duração
    }


    private IEnumerator DashCoroutine()
    {
        canDash = false;
        isDashing = true;
        animator.SetTrigger("Running"); // Inicia a animação de Dash
        efeitosAudioSource.PlayOneShot(efeitoDash, 0.5f);
        rb.velocity = dashDirection * dashSpeed; // Inicia o movimento de Dash

        yield return new WaitForSeconds(dashDuration); // Espera a duração do Dash

        isDashing = false;
        rb.velocity = Vector2.zero; // Para o movimento após o Dash

        yield return new WaitForSeconds(dashCooldown); // Espera o cooldown do Dash
        canDash = true; // Permite que o Dash seja usado novamente
    }

    private void CancelarDashEExecutarPulo()
    {
        isDashing = false; // Cancela o Dash
        rb.velocity = new Vector2(rb.velocity.x, jumpForce); // Executa o pulo após cancelar o Dash
        animator.SetTrigger("Running" +
            ""); // Inicia a animação de pulo
        pulosRestantes--; // Reduz o número de pulos restantes
    }

    public void CreateDust()
    {
        dust.Play();
    }
}
