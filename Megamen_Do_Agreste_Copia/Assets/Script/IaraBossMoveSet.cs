using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class IaraBossMoveSet : MonoBehaviour
{
    //Primeiro Ataque
    public Transform pontoA;
    public Transform pontoB;
    public float velocidade = 15f;
    private Vector3 pontoAtual;
    private Vector2 direction;
    private bool Flip = true;
    private Rigidbody2D rb;
    public int Moves = 0;
    public GameObject Jato;
    private bool jaContouPonto = false;
    public float amplitude = 2f;
    public float frequencia = 2f;
    private float originalY;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        transform.position = pontoB.position;
        direction = (pontoA.position - pontoB.position).normalized;
        originalY =  transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        NadandoeAtirando();
    }

    public void NadandoeAtirando()
    {
        MoveObjeto();

        if(Vector2.Distance(rb.position, pontoAtual) < 0.1f)
        {
            // Troca o ponto atual
            if (pontoAtual == pontoA.position)
            {
                pontoAtual = pontoB.position;
            }
            else
            {
                pontoAtual = pontoA.position;
            }
            Virar();
        }
    }

    void MoveObjeto()
    {
        float horizontalVelocity = direction.x *  velocidade;

        float verticapOffset = Mathf.Sin(Time.time * frequencia) * amplitude;

        rb.velocity = new Vector2(horizontalVelocity, verticapOffset);
    }

    void Virar()
    {
        // Verifica a direção com base no ponto atual
        if ((pontoAtual.x < transform.position.x && Flip) || (pontoAtual.x > transform.position.x && !Flip))
        {
            Flip = !Flip; // Inverte o estado de Flip
            Vector3 escala = transform.localScale;
            escala.x *= -1; // Inverte a escala no eixo X
            transform.localScale = escala;
        }
    }
}
