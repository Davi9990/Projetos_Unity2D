using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapaztPersegueOPlayer : MonoBehaviour
{
    public float velocidade = 2f;
    public float raioDeMovimento = 10f;
    public float gravidade = -9.81f;
    public float distanciaAtaque = 1.5f;
    public float intervaloPerseguicao = 60f;
    public float intervaloAtaque = 2f; // tempo entre ataques
    private bool perseguindo = false, andando = false;

    private Transform jogador;
    public Animator animador;
    private CharacterController controlador;
    private Vector3 destinoAtual;
    private float velocidadeVertical = 0f;

    public MissoesMobile missoesMobile; // Referência ao script de vidas

    void Start()
    {
        controlador = GetComponent<CharacterController>();
        if (controlador == null)
            controlador = gameObject.AddComponent<CharacterController>();

        GameObject objJogador = GameObject.FindWithTag("Player");
        if (objJogador != null)
        {
            jogador = objJogador.transform;
            StartCoroutine(PerseguirACadaMinuto());
        }
        else
        {
            Debug.Log("Nenhum objeto com a tag 'Player' foi encontrado!");
        }
        EscolherNovoDestino();

        if (animador == null)
            animador = GetComponent<Animator>();
    }
    void Update()
    {
        if (!perseguindo)
        {
            AndarAleatoriamente();
        }
        else
        {
            PerseguirJogador();
        }
        AplicarGravidade();
    }
    void AndarAleatoriamente()
    {
        Vector3 direcao = destinoAtual - transform.position;
        direcao.y = 0f;

        if (direcao.magnitude > 0.1f)
        {
            controlador.Move(direcao.normalized * velocidade * Time.deltaTime);
            transform.forward = Vector3.Lerp(transform.forward, direcao.normalized, Time.deltaTime * 5f);
            AtualizarAnimacao(true);
        }
        if (Vector3.Distance(transform.position, destinoAtual) < 0.5f)
        {
            EscolherNovoDestino();
        }
    }
    void EscolherNovoDestino()
    {
        Vector2 pontoAleatorio = Random.insideUnitCircle * raioDeMovimento;
        destinoAtual = new Vector3(transform.position.x + pontoAleatorio.x, transform.position.y, transform.position.z + pontoAleatorio.y);
    }
    void AplicarGravidade()
    {
        if (controlador.isGrounded && velocidadeVertical < 0)
            velocidadeVertical = -2f;

        velocidadeVertical += gravidade * Time.deltaTime;
        controlador.Move(Vector3.up * velocidadeVertical * Time.deltaTime);
    }
    IEnumerator PerseguirACadaMinuto()
    {
        while (true)
        {
            yield return new WaitForSeconds(intervaloPerseguicao);

            perseguindo = true;
            Debug.Log("Capataz começou a perseguir o jogador!");

            // Ataca repetidamente enquanto estiver próximo
            yield return StartCoroutine(PerseguirEAtaque());

            perseguindo = false;
            Debug.Log("Capataz voltou ao normal!");
            EscolherNovoDestino();
        }
    }
    IEnumerator PerseguirEAtaque()
    {
        while (perseguindo)
        {
            if (jogador == null) yield break;

            Vector3 direcao = (jogador.position - transform.position).normalized;
            direcao.y = 0f;
            controlador.Move(direcao * velocidade * Time.deltaTime);
            transform.forward = Vector3.Lerp(transform.forward, direcao, Time.deltaTime * 5f);
            AtualizarAnimacao(true);
            float distancia = Vector3.Distance(transform.position, jogador.position);
            if (distancia <= distanciaAtaque)
            {
                Debug.Log("Capataz atacou o jogador!");
                AtualizarAnimacao(false);

                if (missoesMobile != null)
                    missoesMobile.PerderVida();

                yield return new WaitForSeconds(intervaloAtaque); // Espera antes de atacar novamente
            }
            yield return null;
        }
    }
    void PerseguirJogador()
    {
        if (jogador == null) return;

        Vector3 direcao = (jogador.position - transform.position).normalized;
        direcao.y = 0f;

        controlador.Move(direcao * velocidade * Time.deltaTime);
        transform.forward = Vector3.Lerp(transform.forward, direcao, Time.deltaTime * 5f);
        AtualizarAnimacao(true);
    }
    void AtualizarAnimacao(bool estaAndando)
    {
        if (andando != estaAndando)
        {
            andando = estaAndando;
            animador.SetBool("Andando", andando);
        }
    }
}
