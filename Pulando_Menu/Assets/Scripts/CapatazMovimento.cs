using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapatazMovimento : MonoBehaviour
{
    [Header("Movimentação")]
    public float velocidade = 2f;
    public float raioDeMovimento = 10f;
    public float gravidade = -9.81f;     // força da gravidade

    private Vector3 destinoAtual;
    private bool jogadorPerto = false;
    private float velocidadeVertical = 0f; // AGORA é float!

    [Header("Animação")]
    public Animator anim;
    private bool andando = false;

    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        if (controller == null)
        {
            controller = gameObject.AddComponent<CharacterController>();
        }
        EscolherNovoDestino();

        if (anim == null)
            anim = GetComponent<Animator>();
    }
    void Update()
    {
        if (!jogadorPerto)
        {
            Mover();
        }
        else
        {
            AtualizarAnimacao(false);
        }
        AplicarGravidade();
    }
    void Mover()
    {
        Vector3 direcao = destinoAtual - transform.position;
        direcao.y = 0f; // ignora altura

        if (direcao.magnitude > 0.1f)
        {
            controller.Move(direcao.normalized * velocidade * Time.deltaTime);
            transform.forward = Vector3.Lerp(transform.forward, direcao.normalized, Time.deltaTime * 5f);
            AtualizarAnimacao(true);
        }

        if (Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(destinoAtual.x, 0, destinoAtual.z)) < 0.5f)
        {
            EscolherNovoDestino();
        }
    }
    void EscolherNovoDestino()
    {
        Vector2 pontoAleatorio = Random.insideUnitCircle * raioDeMovimento;
        destinoAtual = new Vector3(pontoAleatorio.x + transform.position.x, transform.position.y, pontoAleatorio.y + transform.position.z);
    }
    void AplicarGravidade()
    {
        if (controller.isGrounded && velocidadeVertical < 0)
        {
            velocidadeVertical = -2f; // mantém o personagem no chão
        }

        velocidadeVertical += gravidade * Time.deltaTime;
        controller.Move(Vector3.up * velocidadeVertical * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jogadorPerto = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jogadorPerto = false;
            EscolherNovoDestino();
        }
    }
    void AtualizarAnimacao(bool estaAndando)
    {
        if (andando != estaAndando)
        {
            andando = estaAndando;
            anim.SetBool("Andando", andando);
        }
    }
}
