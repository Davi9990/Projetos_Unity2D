using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Movimentação
    public float velocidade;
    private Rigidbody2D rb;
    private bool isFacingRight = true;

    //Pulos
    public int Pulos = 2;
    public int PulosDisponiveis;
    public Transform VerificarChao;
    public bool EstaNoChao;
    public LayerMask chao;
    public float JumpForce;

    //Atirar
    public Transform Hand;
    public GameObject fire;
    public GameObject Ice;
    public float speed = 5;
    public float fireRate = 1f;
    public float nextFireTime = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
        Jump();
        Atirar();
    }

    private void Move()
    {
        // Captura o eixo horizontal
        float xAxis = Input.GetAxisRaw("Horizontal");

        // Atualiza a velocidade no eixo X, mantendo a velocidade Y existente
        rb.velocity = new Vector2(xAxis * velocidade, rb.velocity.y);

        // Verifica a direção do movimento e inverte o sprite se necessário
        if (xAxis > 0 && !isFacingRight)
        {
            // Inverte a direção do sprite
            isFacingRight = !isFacingRight;
            Vector3 scaler = transform.localScale;
            scaler.x *= -1;
            transform.localScale = scaler;
        }
        else if (xAxis < 0 && isFacingRight)
        {
            // Inverte a direção do sprite
            isFacingRight = !isFacingRight;
            Vector3 scaler = transform.localScale;
            scaler.x *= -1;
            transform.localScale = scaler; ;
        }
    }
     private void Jump()
    {
        EstaNoChao = Physics2D.OverlapCircle(VerificarChao.position, 0.1f, chao);

        //Se estiver no chão, resetar o número de pulos disponíveis
        if (EstaNoChao)
        {
            PulosDisponiveis = Pulos;
        }

        //Se o botão de pular for pressionado e houver pulos disponíveos
        if(Input.GetButtonDown("Jump") && PulosDisponiveis > 0)
        {
            //Aplicar a força do pulo
            rb.velocity = Vector2.up * JumpForce;

            //Reduzir o número de pulos disponíveis
            PulosDisponiveis--;

            //Debug.Log("Jump!");
        }
        
    }

    private void Atirar()
    {
        if(Input.GetKeyDown(KeyCode.F) && Time.time >= nextFireTime)
        {
            GameObject newFire = Instantiate(fire,Hand.position, Quaternion.identity);
            //Acessa o RigidBody2d do projetil instanciado
            Rigidbody2D fireRb = newFire.GetComponent<Rigidbody2D>();
            //Acessa uma força ao projetil na direção para a qual a mão está apontando
            fireRb.velocity = Hand.right * speed;
            Destroy(newFire, 6f);
            nextFireTime = Time.time + fireRate;
        }
        else if(Input.GetKeyDown(KeyCode.P) && Time.time >= nextFireTime)
        {
            GameObject newIce = Instantiate(Ice, Hand.position, Quaternion.identity);
            //Acessa o RigidBody2d do projetil instanciado
            Rigidbody2D iceRb = newIce.GetComponent<Rigidbody2D>();
            //Acessa uma força ao projetil na direção para a qual a mão está apontando
            iceRb.velocity = Hand.right * speed;
            Destroy(newIce, 6f);
            nextFireTime = Time.time + fireRate;
        }
    }
    
}
