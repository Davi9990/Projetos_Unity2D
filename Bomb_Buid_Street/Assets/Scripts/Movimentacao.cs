using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class Movimentacao : MonoBehaviour
{
    public static int pontuacao = 0;
    public static bool n1 = false, n2 = false, n3 = false;
    //Do jeito que est� agora, isso nunca ir� parar de somar. Ou seja, a pontua��o nunca ir� resetar


    // Movimenta��o
    public float velocidade;
    private Rigidbody2D rb;
    private bool isFacingRight = true;
    private bool moveLeft, moveRight, moveDown, moveUp; // Vari�veis para controlar os bot�es de movimento

    // Pulos
    public Transform VerificarChao;
    public bool EstaNoChao;
    public LayerMask chao;
    public float JumpForce;

    // Escadas
    private float vertical;
    private bool escada;
    private bool escalando;
    //bool comecouEscalar = true;
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

    //Score Maneger
    //public ScoreManeger valor;

    //Troca de Sprites
    //public Sprite Osvaldo_Forte;
    //public Sprite Osvaldo_Giga;
    //private SpriteRenderer SpriteRenderer;

    //Trocando Prefabs
    public GameObject Osvaldo_Normal;
    public GameObject Osvaldo_Grande;
    public GameObject Osvaldo_Giga;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        //valor = GetComponent<ScoreManeger>();
        //SpriteRenderer = GetComponent<SpriteRenderer>();
        //Osvaldo_Forte = GetComponent<SpriteRenderer>();
        Osvaldo_Normal.gameObject.SetActive(true);
        Osvaldo_Grande.gameObject.SetActive(false);
        Osvaldo_Giga.gameObject.SetActive(false);

        // Associa as fun��es de movimenta��o aos eventos de pressionar e soltar os bot�es
        AddButtonListeners();
    }

    void Update()
    {
        if (escada && (moveUp || moveDown)) // Verifica se est� escalando
        {
            escalando = true;
        }

        Fortalecer();
    }

    private void FixedUpdate()
    {
        EstaNoChao = Physics2D.OverlapCircle(VerificarChao.position, 0.1f,chao);
        anim.SetBool("Pulando",!EstaNoChao);
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

    // Adiciona listeners para pressionar e soltar os bot�es
    private void AddButtonListeners()
    {
        // Bot�o de movimenta��o para a esquerda
        EventTrigger triggerLeft = buttonLeft.gameObject.AddComponent<EventTrigger>();
        AddEventTrigger(triggerLeft, EventTriggerType.PointerDown, () => MoveLeft(true));
        AddEventTrigger(triggerLeft, EventTriggerType.PointerUp, () => MoveLeft(false));

        // Bot�o de movimenta��o para a direita
        EventTrigger triggerRight = buttonRight.gameObject.AddComponent<EventTrigger>();
        AddEventTrigger(triggerRight, EventTriggerType.PointerDown, () => MoveRight(true));
        AddEventTrigger(triggerRight, EventTriggerType.PointerUp, () => MoveRight(false));

        // Bot�o de pulo (um clique)
        buttonJump.onClick.AddListener(() => Jump());

        // Bot�o de descida
        EventTrigger triggerDown = buttonDown.gameObject.AddComponent<EventTrigger>();
        AddEventTrigger(triggerDown, EventTriggerType.PointerDown, () => MoveDown(true));
        AddEventTrigger(triggerDown, EventTriggerType.PointerUp, () => MoveDown(false));

        // Bot�o de subida
        EventTrigger triggerUp = buttonUp.gameObject.AddComponent<EventTrigger>();
        AddEventTrigger(triggerUp, EventTriggerType.PointerDown, () => MoveUp(true));
        AddEventTrigger(triggerUp, EventTriggerType.PointerUp, () => MoveUp(false));
    }

    // Fun��o utilit�ria para adicionar eventos de clique
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

    // Fun��es chamadas pelos bot�es
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
            anim.SetBool("Pulando",true);
        }
    }

    // Movimenta o jogador de acordo com os bot�es pressionados
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
        
        if(moveLeft || moveRight)
        {
            anim.SetBool("Andando", true);
        }
        else
        {
            anim.SetBool("Andando",false);
        }

        // Logando os valores
        //Debug.Log("xAxis: " + xAxis + ", rb.velocity: " + rb.velocity);

        // Atualiza a dire��o do movimento e inverte o sprite se necess�rio
        if (xAxis > 0 && !isFacingRight)
        {
            Flip();
           
        }
        else if (xAxis < 0 && isFacingRight)
        {
            Flip();
           
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    // Verifica se o jogador est� numa escada
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Escada"))
        {
            escada = true;
            buttonDown.interactable = true; // Habilita o bot�o de descer ao estar na escada
            buttonUp.interactable = true;   // Habilita o bot�o de subir ao estar na escada
            anim.SetBool("Escalando",true);
        }

        //if (collision.gameObject.tag == "item")
        //{
        //    Coletar.Play();

        //    if(pontuacao >= 16000 && n1 == false)
        //    {
        //        //Fortalecer(1);
                
        //    }
        //    else if (pontuacao >= 32000 && n2 == false)
        //    {
        //        //Fortalecer(2);
                
        //    }
        //    else if (pontuacao >= 64000 && n3 == false)
        //    {
        //        //Fortalecer(3);
                
        //    }
            
        //}
    }

    void Fortalecer()
    {
        //Upar.Play();

        int scoreAtual = ScoreManeger.Instance.score;

        switch (scoreAtual)
        {
            case 4000: //BOTAR NO N�VEL 2
                Osvaldo_Normal.gameObject.SetActive(false);
                Osvaldo_Grande.gameObject.SetActive(true);
                break;

            case 16000: //BOTAR NO N�VEL 3
                Osvaldo_Grande.gameObject.SetActive(false);
                Osvaldo_Giga.gameObject.SetActive(true);
                break;

            case 40000: SceneManager.LoadScene("Vitoria"); break;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Escada"))
        {
            escada = false;
            escalando = false; // Para de escalar
            rb.velocity = new Vector2(rb.velocity.x, 0);
            buttonDown.interactable = false; // Desabilita o bot�o de descer ao sair da escada
            buttonUp.interactable = false;   // Desabilita o bot�o de subir ao sair da escada
            anim.SetBool("Escalando",false);
        }
    }
}
