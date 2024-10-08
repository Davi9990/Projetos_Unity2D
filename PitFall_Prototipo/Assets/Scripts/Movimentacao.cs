using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Movimentacao : MonoBehaviour
{
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

    // Sons
    public AudioSource pulo;
    public AudioSource Escada;

    // UI Buttons 
    public Button buttonLeft;
    public Button buttonRight;
    public Button buttonJump;
    public Button buttonDown;
    public Button buttonUp; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Associa as fun��es de movimenta��o aos eventos de pressionar e soltar os bot�es
        AddButtonListeners();
    }

    void Update()
    {
        if (escada && (moveUp || moveDown)) // Verifica se est� escalando
        {
            escalando = true;
        }
    }

    private void FixedUpdate()
    {
        Move(); // Movimenta o jogador baseado nos inputs

        if (escalando)
        {
            Escada.Play();
            rb.gravityScale = 0;
            rb.velocity = new Vector2(rb.velocity.x, vertical * velocidade); // Move para cima ou para baixo na escada
        }
        else
        {
            Escada.Stop();
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

    // Fun��es chamadas pelos bot�es
    public void MoveLeft(bool isPressed)
    {
        moveLeft = isPressed;
    }

    public void MoveRight(bool isPressed)
    {
        moveRight = isPressed;
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
        }
    }
}
