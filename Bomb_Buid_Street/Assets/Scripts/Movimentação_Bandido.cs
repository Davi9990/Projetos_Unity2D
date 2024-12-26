using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Movimentação_Bandido : MonoBehaviour
{
    public Transform player;
    public Transform player_Grande;
   

    public Rigidbody2D playerRb;
    public Rigidbody2D playerRbGrande; // Referência ao Player Grande
    public Rigidbody2D playerRbGiga; // Referenccia ao Player Giga

    
    public float moveSpeed = 2f;
    private Rigidbody2D rb;

    //Bandido Fase 1
    [SerializeField] Transform starPoint;
    [SerializeField] Transform endPoint;
    [SerializeField] float JumpForce;
    private Vector2 direction;
    private bool movingToEnd = true;

    //Bandido Fase 2
    public float Reaload = 1f;
    public GameObject prefabProjetil;
    public Transform ShootPoint;
    public float ProjetilVelocity = 10f;
    public float LifeProjectTime = 5f;
    public float JumpInterval = 2f;
    private bool isGround = true;
    private float jumpTimer;
    private float fireTimer;
    public float fireRate = 3f;

    //Bandido Fase 3
    public bool Fugindo;
    bool enterMedo = true;
    public Transform player_Giga;
    public float FugaDistance = 10f;
    private SpriteRenderer render;

    //Animator
    private Animator anim;

    public AudioSource a;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        render = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        jumpTimer = JumpInterval;
        fireTimer = fireRate;

        if(playerRb == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if(playerObject != null)
            {
                playerRb = playerObject.GetComponent<Rigidbody2D>();
            }
        }

        //Define a direção inicial
        direction = (endPoint.position - transform.position).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        //float distanceToPlayer = Vector2.Distance(transform.position, player.position);


        if(playerRb != null && playerRb.gameObject.activeInHierarchy)
        {
            Covarde();
        }
        else if(playerRbGrande != null && playerRbGrande.gameObject.activeInHierarchy)
        {
            FrenteTrasPulo();
        }
        else if(playerRbGiga != null && playerRbGiga.gameObject.activeInHierarchy)
        {
            if (enterMedo)
            {
                a.Play();
                enterMedo = false;
            }
            Frango();

        }
    }

    void Covarde()
    {
       //Movimenta o inimigo entre o startPoint e endPoint
       if(movingToEnd)
       {
           direction = (endPoint.position - transform.position).normalized;
       }
       else
       {
           direction = (starPoint.position - transform.position).normalized;
       }

        rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y);

        // Verifica se o inimigo chegou ao destino
        if (Vector2.Distance(transform.position, endPoint.position) < 0.1f && movingToEnd)
        {
            movingToEnd = false; // Começa a voltar para o ponto inicial
        }
        else if (Vector2.Distance(transform.position, starPoint.position) < 0.1f && !movingToEnd)
        {
            movingToEnd = true; // Começa a ir para o endPoint novamente
        }
    }

    void FrenteTrasPulo() 
    {
        float moveDirection = Mathf.PingPong(Time.time * moveSpeed, 2) - 1;
        rb.velocity = new Vector2(moveDirection * moveSpeed, rb.velocity.y);

        //Controle de Pulo
        jumpTimer -= Time.deltaTime;
        if(jumpTimer <= 0 && isGround)
        {
            Jump();
            jumpTimer = JumpInterval;
        }

        //Controle de tiro fogo
        fireTimer -= Time.deltaTime;
        if(fireTimer <= 0)
        {
            ShootFire();
            fireTimer = fireRate;
        }
    }

    void Frango()
    {
        float distanciaParaJogador = Vector2.Distance(transform.position, player_Giga.position);

        Flip();

        //Se o jogador estiver dentro do raio de fuga, o inimigo foge
        if(distanciaParaJogador <= FugaDistance)
        {
            direction = (transform.position - player_Giga.position).normalized;
            transform.position += (Vector3)(direction * moveSpeed * Time.deltaTime);
        }
        else
        {
            FrenteTrasPulo();
        }
    }

   void Flip()
    {
        if (player_Giga != null)
        {
            if(player_Giga.position.x < transform.position.x)
            {
                render.flipX = false;
            }
            else
            {
                render.flipX = true;
            }
        }
    }

    private void Jump()
    {
        anim.SetBool("Pulando", true);
        rb.velocity = new Vector2(rb.velocity.x, JumpForce);
    }

    private void ShootFire()
    {
        GameObject fireball = Instantiate(prefabProjetil, ShootPoint.position, Quaternion.identity);
        //Acessa o rigidbody2D do projétil instanciado
        Rigidbody2D fireRb = fireball.GetComponent<Rigidbody2D>();

        fireRb.velocity = new Vector2(-ProjetilVelocity, fireRb.velocity.y);

        Destroy(fireball, LifeProjectTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PontoDePulo"))
        {
            rb.velocity = new Vector2(rb.velocity.x, JumpForce);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("EndPoint") || collision.gameObject.CompareTag("Player") 
            || collision.gameObject.CompareTag("Player_Grande"))
        {
            transform.position = starPoint.position;
            rb.velocity = Vector2.zero;
            movingToEnd = true;
        }

        if (collision.gameObject.CompareTag("Chao"))
        {
            anim.SetBool("Pulando", false);
            isGround = true;
        }

        if (collision.gameObject.CompareTag("EndPoint2")) 
        {
            Destroy(gameObject);
        }
    }
}
