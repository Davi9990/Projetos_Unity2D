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
    //public static bool n1 = false, n2 = false, n3 = false;;

    // Movimentação
    public float velocidade;
    private Rigidbody2D rb;
    private bool isFacingRight = true;
    private bool moveLeft, moveRight, moveDown, moveUp;

    // Pulos
    public Transform VerificarChao;
    public bool EstaNoChao;
    public LayerMask chao;
    public float JumpForce;

    // Escadas
    private float vertical;
    private bool escada;
    private bool escalando;
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

    // Animator
    private Animator anim;

    // Troca de Sprites
    public GameObject Osvaldo_Normal;
    public GameObject Osvaldo_Grande;
    public GameObject Osvaldo_Giga;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Osvaldo_Normal.gameObject.SetActive(true);
        Osvaldo_Grande.gameObject.SetActive(false);
        Osvaldo_Giga.gameObject.SetActive(false);

        AddButtonListeners();
    }

    void Update()
    {
        if (escada && (moveUp || moveDown)) 
        {
            escalando = true;
        }

        Fortalecer();
    }

    private void FixedUpdate()
    {
        EstaNoChao = Physics2D.OverlapCircle(VerificarChao.position, 0.1f, chao);
        anim.SetBool("Pulando", !EstaNoChao);
        Move();

        if (escalando)
        {
            tocar += Time.deltaTime;
            if (tocar >= 0.2)
            {
                Escada.Play();
                tocar = 0;
            }
            rb.gravityScale = 0;
            rb.velocity = new Vector2(rb.velocity.x, vertical * velocidade); 
        }
        else
        {
            Escada.Stop();
            tocar = 0;
            rb.gravityScale = 2f;
        }
    }

    private void AddButtonListeners()
    {
        EventTrigger triggerLeft = buttonLeft.gameObject.AddComponent<EventTrigger>();
        AddEventTrigger(triggerLeft, EventTriggerType.PointerDown, () => MoveLeft(true));
        AddEventTrigger(triggerLeft, EventTriggerType.PointerUp, () => MoveLeft(false));

        EventTrigger triggerRight = buttonRight.gameObject.AddComponent<EventTrigger>();
        AddEventTrigger(triggerRight, EventTriggerType.PointerDown, () => MoveRight(true));
        AddEventTrigger(triggerRight, EventTriggerType.PointerUp, () => MoveRight(false));

        buttonJump.onClick.AddListener(() => Jump());

        EventTrigger triggerDown = buttonDown.gameObject.AddComponent<EventTrigger>();
        AddEventTrigger(triggerDown, EventTriggerType.PointerDown, () => MoveDown(true));
        AddEventTrigger(triggerDown, EventTriggerType.PointerUp, () => MoveDown(false));

        EventTrigger triggerUp = buttonUp.gameObject.AddComponent<EventTrigger>();
        AddEventTrigger(triggerUp, EventTriggerType.PointerDown, () => MoveUp(true));
        AddEventTrigger(triggerUp, EventTriggerType.PointerUp, () => MoveUp(false));
    }

    private void AddEventTrigger(EventTrigger trigger, EventTriggerType eventType, System.Action action)
    {
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = eventType;
        entry.callback.AddListener((eventData) => { action(); });
        trigger.triggers.Add(entry);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "chaoteorico")
        {
            Escada.Stop();
            tocar = 0;
        }
    }

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
            vertical = -1;
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
            vertical = 1;
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
            anim.SetBool("Pulando", true);
        }
    }

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

        rb.velocity = new Vector2(xAxis * velocidade, rb.velocity.y);

        if (moveLeft || moveRight)
        {
            anim.SetBool("Andando", true);
        }
        else
        {
            anim.SetBool("Andando", false);
        }

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Escada"))
        {
            escada = true;
            buttonDown.interactable = true;
            buttonUp.interactable = true;
            anim.SetBool("Escalando", true);
        }

        if(collision.gameObject.tag == "item")
        {
            Coletar.Play();

        }
    }

    void Fortalecer()
    {

        if (ScoreManeger.Instance == null)
        {
            Debug.LogError("ScoreManeger.Instance está nulo!");
            return;
        }

        int scoreAtual = ScoreManeger.Instance.score;
        GameObject prefabAtual = null;
        GameObject proximoPrefab = null;

        if (scoreAtual >= 13000 && scoreAtual < 30000)
        {

            prefabAtual = Osvaldo_Normal;
            proximoPrefab = Osvaldo_Grande;
        }
        else if (scoreAtual >= 30000 && scoreAtual < 40000)
        {
            prefabAtual = Osvaldo_Grande;
            proximoPrefab = Osvaldo_Giga;
        }
        else if (scoreAtual >= 40000)
        {
            SceneManager.LoadScene("Vitoria");
        }

        if (prefabAtual != null && proximoPrefab != null)
        {
            Vector3 posicaoAtual = prefabAtual.transform.position;
            Quaternion rotacaoAtual = prefabAtual.transform.rotation;

            prefabAtual.SetActive(false);

            proximoPrefab.transform.position = posicaoAtual;
            proximoPrefab.transform.rotation = rotacaoAtual;
            proximoPrefab.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Escada"))
        {
            escada = false;
            escalando = false;
            rb.velocity = new Vector2(rb.velocity.x, 0);
            buttonDown.interactable = false;
            buttonUp.interactable = false;
            anim.SetBool("Escalando", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Chao")
        {
            EstaNoChao = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Chao")
        {
            EstaNoChao = false;
        }
    }
}
