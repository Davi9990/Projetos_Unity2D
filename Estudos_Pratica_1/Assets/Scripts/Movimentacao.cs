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
        

        //Se estiver no ch�o, resetar o n�mero de pulos dispon�veis
        if (EstaNoChao)
        {
            PulosDisponiveis = Pulos;
        }

        //Se o bot�o de pular for pressionado e houver pulos dispon�veos
        if (Input.GetButtonDown("Jump") && PulosDisponiveis > 0)
        {
            //Aplicar a for�a do pulo
            rb.velocity = Vector2.up * JumpForce;

            //Reduzir o n�mero de pulos dispon�veis
            PulosDisponiveis--;

            //Debug.Log("Jump!");
        }
    }
}
