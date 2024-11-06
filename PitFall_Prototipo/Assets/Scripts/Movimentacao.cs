using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Movimentacao : MonoBehaviour
{
    public static int pontuacao = 0;
    public static bool n1 = false, n2 = false, n3 = false;
    //Do jeito que está agora, isso nunca irá parar de somar. Ou seja, a pontuação nunca irá resetar


    // Movimentação
    public float velocidade;
    private Rigidbody2D rb;
    private bool isFacingRight = true;
    private bool moveLeft, moveRight, moveDown, moveUp; // Variáveis para controlar os botões de movimento

    // Pulos
    public Transform VerificarChao;
    public bool EstaNoChao;
    public LayerMask chao;
    public float JumpForce;

    // Escadas
    private float vertical;
    private bool escada;
    private bool escalando;
    bool comecouEscalar = true;
    float tocar = 0;

    // Sons
    public AudioSource pulo;
    public AudioSource Escada;
    public AudioSource Coletar;
    public AudioSource Upar;

    // UI Buttons 
    public Button buttonLeft;
    public Button buttonRight;
    public Button buttonJump;
    public Button buttonDown;
    public Button buttonUp;

    //Animator
    private Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        // Associa as funções de movimentação aos eventos de pressionar e soltar os botões
        AddButtonListeners();
    }

    void Update()
    {
        if (escada && (moveUp || moveDown)) // Verifica se está escalando
        {
            escalando = true;
        }
    }

    private void FixedUpdate()
    {
        Move(); // Movimenta o jogador baseado nos inputs

        if (escalando)
        {
            tocar += Time.deltaTime;
            if(tocar >= 0.2)
            {
                Escada.Play();
                tocar = 0;
            }
            rb.gravityScale = 0;
            rb.velocity = new Vector2(rb.velocity.x, vertical * velocidade); // Move para cima ou para baixo na escada
        }
        else
        {
            Escada.Stop();
            tocar = 0;
            //Debug.Log("Parou de escalar " + tocar);
            rb.gravityScale = 2f;
        }
    }

    // Adiciona listeners para pressionar e soltar os botões
    private void AddButtonListeners()
    {
        // Botão de movimentação para a esquerda
        EventTrigger triggerLeft = buttonLeft.gameObject.AddComponent<EventTrigger>();
        AddEventTrigger(triggerLeft, EventTriggerType.PointerDown, () => MoveLeft(true));
        AddEventTrigger(triggerLeft, EventTriggerType.PointerUp, () => MoveLeft(false));

        // Botão de movimentação para a direita
        EventTrigger triggerRight = buttonRight.gameObject.AddComponent<EventTrigger>();
        AddEventTrigger(triggerRight, EventTriggerType.PointerDown, () => MoveRight(true));
        AddEventTrigger(triggerRight, EventTriggerType.PointerUp, () => MoveRight(false));

        // Botão de pulo (um clique)
        buttonJump.onClick.AddListener(() => Jump());

        // Botão de descida
        EventTrigger triggerDown = buttonDown.gameObject.AddComponent<EventTrigger>();
        AddEventTrigger(triggerDown, EventTriggerType.PointerDown, () => MoveDown(true));
        AddEventTrigger(triggerDown, EventTriggerType.PointerUp, () => MoveDown(false));

        // Botão de subida
        EventTrigger triggerUp = buttonUp.gameObject.AddComponent<EventTrigger>();
        AddEventTrigger(triggerUp, EventTriggerType.PointerDown, () => MoveUp(true));
        AddEventTrigger(triggerUp, EventTriggerType.PointerUp, () => MoveUp(false));
    }

    // Função utilitária para adicionar eventos de clique
    private void AddEventTrigger(EventTrigger trigger, EventTriggerType eventType, System.Action action)
    {
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = eventType;
        entry.callback.AddListener((eventData) => { action(); });
        trigger.triggers.Add(entry);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "chaoteorico")
        {
            Escada.Stop();
            tocar = 0;
        }
    }

    // Funções chamadas pelos botões
    public void MoveLeft(bool isPressed)
    {
        moveLeft = isPressed;
        //Debug.Log("MoveLeft: " + isPressed);
    }

    public void MoveRight(bool isPressed)
    {
        moveRight = isPressed;
        //Debug.Log("MoveRight: " + isPressed);
    }

    public void MoveDown(bool isPressed)
    {
        if (escada && isPressed)
        {
            moveDown = true;
            vertical = -1; // Configura para descer a escada
        }
        else
        {
            moveDown = false;
        }
    }

    public void MoveUp(bool isPressed)
    {
        if (escada && isPressed)
        {
            moveUp = true;
            vertical = 1; // Configura para subir a escada
        }
        else
        {
            moveUp = false;
        }
    }

    public void Jump()
    {
        EstaNoChao = Physics2D.OverlapCircle(VerificarChao.position, 0.1f, chao);

        if (EstaNoChao)
        {
            pulo.Play();
            rb.velocity = Vector2.up * JumpForce;
        }
    }

    // Movimenta o jogador de acordo com os botões pressionados
    private void Move()
    {
        float xAxis = 0;

        if (moveLeft)
        {
            xAxis = -1;
        }
        else if (moveRight)
        {
            xAxis = 1;
        }

        // Atualiza a velocidade no eixo X, mantendo a velocidade Y existente
        rb.velocity = new Vector2(xAxis * velocidade, rb.velocity.y);
        

        // Logando os valores
        //Debug.Log("xAxis: " + xAxis + ", rb.velocity: " + rb.velocity);

        // Atualiza a direção do movimento e inverte o sprite se necessário
        if (xAxis > 0 && !isFacingRight)
        {
            Flip();
            anim.SetBool("Andando", true);
        }
        else if (xAxis < 0 && isFacingRight)
        {
            Flip();
            anim.SetBool("Andando", true);
        }

        else if (xAxis <= 0)
        {
            anim.SetBool("Andando", false);
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    // Verifica se o jogador está numa escada
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Escada"))
        {
            escada = true;
            buttonDown.interactable = true; // Habilita o botão de descer ao estar na escada
            buttonUp.interactable = true;   // Habilita o botão de subir ao estar na escada
        }

        if (collision.gameObject.tag == "item")
        {
            Coletar.Play();

            if(pontuacao >= 16000 && n1 == false)
            {
                Fortalecer(1);
                
            }
            else if (pontuacao >= 32000 && n2 == false)
            {
                Fortalecer(2);
                
            }
            else if (pontuacao >= 64000 && n3 == false)
            {
                Fortalecer(3);
                
            }
            
        }
    }

    void Fortalecer(int nivel)
    {
        Upar.Play();

        switch (nivel)
        {
            case 1: //BOTAR NO NÍVEL 2
                n1 = true; break;

            case 2: //BOTAR NO NÍVEL 3
                n2 = true; break;

            case 3: SceneManager.LoadScene("Vitoria"); break;



        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Escada"))
        {
            escada = false;
            escalando = false; // Para de escalar
            rb.velocity = new Vector2(rb.velocity.x, 0);
            buttonDown.interactable = false; // Desabilita o botão de descer ao sair da escada
            buttonUp.interactable = false;   // Desabilita o botão de subir ao sair da escada
        }
    }
}
