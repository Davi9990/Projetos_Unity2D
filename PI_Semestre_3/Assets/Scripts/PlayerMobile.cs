
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMobile : MonoBehaviour
{
    [Header("Configurações de Movimento")]
    public float velocidade = 6f;              
    public float velocidadeRotacao = 10f;     
    public float gravidade = 30f;              
    public Joystick joystick;                 

    [Header("Componentes")]
    private CharacterController controller;   
    private Animator animator;                 

    private Vector3 direcaoMovimento = Vector3.zero;
    private Vector3 forcaGravidade = Vector3.zero;
    private bool andandoAtual = false;         

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        //============================ Captura a direção do joystick====================================
        float moverH = joystick.Horizontal;
        float moverV = joystick.Vertical;

        direcaoMovimento = new Vector3(moverH, 0, moverV);

        // Aqui faz a verificacao do movimento
        bool andando = direcaoMovimento.magnitude > 0.15f;

        // Aqui faz a atualizacao as animações
        if (andando != andandoAtual)
        {
            animator.SetBool("isWalking", andando);
            animator.SetBool("isIdle", !andando);
            andandoAtual = andando;
        }

        // Aqui faz se estiver se movendo, aplica movimento e rotação no player
        if (andando)
        {
            Vector3 movimento = direcaoMovimento.normalized * velocidade;

            // Faz o personagem olhar para a direção do movimento
            Quaternion rotacao = Quaternion.LookRotation(movimento);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotacao, velocidadeRotacao * Time.deltaTime);

            // Move o personagem
            controller.Move(movimento * Time.deltaTime);
        }

        // Aplica gravidade
        if (controller.isGrounded)
        {
            forcaGravidade.y = -2f; // Mantém o personagem no chão
        }
        else
        {
            forcaGravidade.y -= gravidade * Time.deltaTime;
        }

        controller.Move(forcaGravidade * Time.deltaTime);
    }
}



/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMobile : MonoBehaviour
{
    [Header("Configurações de movimento")]
    public float velocidade = 5f;        // Velocidade do personagem
    public Joystick joystick;            // Referência ao joystick móvel
    public float gravity = -9.81f;       // Gravidade para manter no chão
    public float rotationSpeed = 10f;    // Velocidade de rotação suave

    private CharacterController controller;
    private Animator anim;
    private Vector3 velocity;            // Para gravidade
    private bool andandoAtual = false;   // Estado da animação

    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // Movimento horizontal
        float moverH = joystick.Horizontal;
        float moverV = joystick.Vertical;

        Vector3 direcao = new Vector3(moverH, 0, moverV);

        // Verifica se está se movendo
        bool andando = direcao.magnitude > 0.15f;

        // Atualiza animação
        if (andando != andandoAtual)
        {
            anim.SetBool("Andando", andando);
            andandoAtual = andando;
        }

        if (andando)
        {
            // Normaliza e aplica velocidade
            Vector3 movimento = direcao.normalized * velocidade;

            // Rotação suave do personagem
            Quaternion rotacao = Quaternion.LookRotation(direcao);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotacao, rotationSpeed * Time.deltaTime);

            // Move o CharacterController
            controller.Move(movimento * Time.deltaTime);
        }

        // Gravidade
        if (!controller.isGrounded)
        {
            velocity.y += gravity * Time.deltaTime;
        }
        else if (velocity.y < 0)
        {
            velocity.y = -2f; // Mantém no chão
        }

        controller.Move(velocity * Time.deltaTime);
    }
}*/
