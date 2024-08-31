using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Movimenta��o
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

        // Verifica a dire��o do movimento e inverte o sprite se necess�rio
        if (xAxis > 0 && !isFacingRight)
        {
            // Inverte a dire��o do sprite
            isFacingRight = !isFacingRight;
            Vector3 scaler = transform.localScale;
            scaler.x *= -1;
            transform.localScale = scaler;
        }
        else if (xAxis < 0 && isFacingRight)
        {
            // Inverte a dire��o do sprite
            isFacingRight = !isFacingRight;
            Vector3 scaler = transform.localScale;
            scaler.x *= -1;
            transform.localScale = scaler; ;
        }
    }
     private void Jump()
    {
        EstaNoChao = Physics2D.OverlapCircle(VerificarChao.position, 0.1f, chao);

        //Se estiver no ch�o, resetar o n�mero de pulos dispon�veis
        if (EstaNoChao)
        {
            PulosDisponiveis = Pulos;
        }

        //Se o bot�o de pular for pressionado e houver pulos dispon�veos
        if(Input.GetButtonDown("Jump") && PulosDisponiveis > 0)
        {
            //Aplicar a for�a do pulo
            rb.velocity = Vector2.up * JumpForce;

            //Reduzir o n�mero de pulos dispon�veis
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
            //Acessa uma for�a ao projetil na dire��o para a qual a m�o est� apontando
            fireRb.velocity = Hand.right * speed;
            Destroy(newFire, 6f);
            nextFireTime = Time.time + fireRate;
        }
        else if(Input.GetKeyDown(KeyCode.P) && Time.time >= nextFireTime)
        {
            GameObject newIce = Instantiate(Ice, Hand.position, Quaternion.identity);
            //Acessa o RigidBody2d do projetil instanciado
            Rigidbody2D iceRb = newIce.GetComponent<Rigidbody2D>();
            //Acessa uma for�a ao projetil na dire��o para a qual a m�o est� apontando
            iceRb.velocity = Hand.right * speed;
            Destroy(newIce, 6f);
            nextFireTime = Time.time + fireRate;
        }
    }
    
}
