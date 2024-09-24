using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimentacao : MonoBehaviour
{
    public float velocidade;
    private Rigidbody2D rb;
   

    //Pulos
    public int Pulos;
    public int PulosDisponiveis;
    public Transform VerificarChao;
    public bool EstaNoChao;
    public LayerMask chao;
    public float JumpForce;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Jump();

        float xAxis = Input.GetAxis("Horizontal");

        //Atualiza a valocidade no eixo X, mantendo a velocidade Y existente
        rb.velocity = new Vector2(xAxis * velocidade, rb.velocity.y);
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
        if (Input.GetButtonDown("Jump") && PulosDisponiveis > 0)
        {
            //Aplicar a força do pulo
            rb.velocity = Vector2.up * JumpForce;

            //Reduzir o número de pulos disponíveis
            PulosDisponiveis--;

            //Debug.Log("Jump!");
        }
    }
}
