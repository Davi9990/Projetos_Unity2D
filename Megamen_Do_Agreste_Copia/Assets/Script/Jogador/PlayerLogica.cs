using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLogica : MonoBehaviour
{
    // Movimentação
    public float speed;
    public bool isFacingRight = true;

    // Pulo
    public float JP;
    public bool CnJmp;
    private float CoyoteJump = 0.5f;
    private bool tempoacabando = false;

    // Tiro
    public Transform Hand;
    public GameObject Balas;
    public float speedBulllets;
    public float fireRate = 1f;
    private float nextFireTime = 0f;

    // Armas
    public MonoBehaviour[] scripts; // Array de scripts das armas
    public Button b1, b2, b3; // Botões para troca de armas
    private int armaSelecionada = 0; // Índice da arma selecionada

    // Pausa
    public GameObject pauseMenu; // Referência ao painel de pausa
    public bool isPaused = false;
    private KeyCode pauseKey = KeyCode.Escape; // Tecla para pausar o jogo

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sp;
    public bool isRunning = true;

    // Variáveis de poderes desbloqueados
    public bool Boitata = true, curupira = true, Iara = true;
    //public GameObject Bolha;

    void Start()
    {
        // Sincroniza os poderes do GameManager com o Player
        if (GameManager.Instance != null)
        {
            Boitata = GameManager.Instance.Boitata;
            curupira = GameManager.Instance.Curupira;
            Iara = GameManager.Instance.Iara;
        }
        else
        {
            Debug.LogWarning("GameManager não encontrado. Certifique-se de que está na cena inicial.");
        }

        // Inicialização dos componentes
        anim = GetComponent<Animator>();
        sp = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        AtualizarArmaSelecionada();
        ConfigurarBotoes();
        pauseMenu.SetActive(false); // Garante que o menu de pausa começa desativado
        //Bolha.SetActive(false);
    }

    void Update()
    {

        if (Input.GetKeyDown(pauseKey))
        {
            TogglePause();
        }

        if (!isPaused)
        {
            Movimentar();
            TrocarArmaRapidamente();

            if (Input.GetButtonDown("Jump") && CnJmp)
            {
                rb.velocity = new Vector2(rb.velocity.x, JP);
                CnJmp = false;
                anim.SetBool("Pulando", true);
            }

            if (tempoacabando)
            {
                CoyoteJump -= Time.deltaTime;
                if (CoyoteJump <= 0)
                {
                    CnJmp = false;
                    tempoacabando = false;
                }
            }

            if (Input.GetButtonUp("Jump") && rb.velocity.y > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
                anim.SetBool("Pulando", true);
            }

            if(isRunning)
            {
                anim.SetBool("Correndo", true);
            }
            else
            {
                anim.SetBool("Correndo", false);
            }

            if (Input.GetKeyDown(KeyCode.F)) // Ajuste para o botão de disparo
            {
                // Transição de animação para "Atirando" com base no estado de movimento
                if (isRunning)
                {
                    anim.SetTrigger("Correndo_Atirando");
                }
                else if (CnJmp == false)
                {
                    if (anim.GetBool("Pulando"))
                    {
                        anim.SetTrigger("Pulando_Atirando");
                    }
                }
                else
                {
                    anim.SetTrigger("Parado_Atirando");
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Chao"))
        {
            CnJmp = true;
            CoyoteJump = 0.5f;
            tempoacabando = false;
            anim.SetBool("Pulando", false);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Chao"))
        {
            tempoacabando = true;
        }
    }

    void Movimentar()
    {
        float movimento = Input.GetAxisRaw("Horizontal");
        transform.Translate(new Vector2(movimento * speed * Time.deltaTime, 0));

        if (movimento < 0 && isFacingRight)
        {
            Flip();
            anim.SetBool("Correndo", true);
            //anim.SetBool("Parado", false);
            isRunning = true;
        }
        else if (movimento > 0 && !isFacingRight)
        {
            Flip();
            anim.SetBool("Correndo", true);
            //anim.SetBool("Parado", false);
            isRunning = true;
        }
        else if(movimento == 0)
        {
            anim.SetBool("Correndo", false);
            //anim.SetBool("Parado", true);
            isRunning = false;
        }
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        sp.flipX = !sp.flipX;

        Vector3 localScale = Hand.localPosition;
        localScale.x *= -1;
        Hand.localPosition = localScale;
    }

    void TrocarArmaRapidamente()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            armaSelecionada = 0;
            AtualizarArmaSelecionada();
            sp.color = Color.white;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (Boitata)
            {
                armaSelecionada = 1;
                AtualizarArmaSelecionada();
                sp.color = Color.red;
            }
            else
            {
                Debug.Log("Poder Boitata ainda não está liberado!");
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (curupira)
            {
                armaSelecionada = 2;
                AtualizarArmaSelecionada();
                sp.color = Color.black;
            }
            else
            {
                Debug.Log("Poder Curupira ainda não está liberado!");
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (Iara)
            {
                armaSelecionada = 3;
                AtualizarArmaSelecionada();
                sp.color = Color.blue;
            }
            else
            {
                Debug.Log("Poder Iara ainda não está liberado!");
            }
        }
    }

    void AtualizarArmaSelecionada()
    {
        for (int i = 0; i < scripts.Length; i++)
        {
            scripts[i].enabled = (i == armaSelecionada); // Ativa apenas a arma selecionada
        }
    }

    void ConfigurarBotoes()
    {
        b1.onClick.AddListener(() => SelecionarArma(1));
        b2.onClick.AddListener(() => SelecionarArma(2));
        b3.onClick.AddListener(() => SelecionarArma(3));
    }

    void SelecionarArma(int index)
    {
        armaSelecionada = index;
        AtualizarArmaSelecionada();
    }

    void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0; // Pausa o jogo
            pauseMenu.SetActive(true); // Mostra o menu de pausa
        }
        else
        {
            Time.timeScale = 1; // Retorna o tempo ao normal
            pauseMenu.SetActive(false); // Esconde o menu de pausa
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Redemoinho"))
        {
            rb.velocity = Vector2.up * JP;
        }
    }
}
