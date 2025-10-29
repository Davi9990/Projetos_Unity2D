using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapaztPersegueOPlayer : MonoBehaviour
{
    public float velocidade = 2f, raioDeMovimento = 10f, gravidade = -9.81f, distanciaAtaque = 1.5f, intervaloPerseguicao = 60f;   
    private bool perseguindo = false,andando = false;              
    private Transform jogador;     
    public Animator animador;                
    private CharacterController controlador;        
    private Vector3 destinoAtual;                   
    private float velocidadeVertical = 0f;          

    void Start()
    {
        controlador = GetComponent<CharacterController>();
        if (controlador == null)
            controlador = gameObject.AddComponent<CharacterController>();
        // Acha o jogador pela tag "Player"
        GameObject objJogador = GameObject.FindWithTag("Player");
        if (objJogador != null)
        {
            jogador = objJogador.transform;
            // Começa a rotina de perseguição a cada 1 minuto
            StartCoroutine(PerseguirACadaMinuto());
        }
        else
        {
            Debug.Log("Nenhum objeto com a tag 'Player' foi encontrado!");
        }
        EscolherNovoDestino();

        // Pega o animador, se existir
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
            // Espera 1 minuto
            yield return new WaitForSeconds(intervaloPerseguicao);

            // Começa a perseguir o jogador
            perseguindo = true;
            Debug.Log("Capataz começou a perseguir o jogador!");

            // Faz a perseguição até atacar uma vez
            yield return StartCoroutine(PerseguirEAtaqueUmaVez());

            // Volta ao modo normal (andar aleatoriamente)
            perseguindo = false;
            Debug.Log("Capataz voltou ao normal!");
            EscolherNovoDestino();
        }
    }

    IEnumerator PerseguirEAtaqueUmaVez()
    {
        bool atacou = false;

        while (!atacou)
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
                atacou = true;
                AtualizarAnimacao(false);
                // Aqui você pode colocar um comando de dano, exemplo:
                // jogador.GetComponent<VidaDoJogador>().LevarDano(1);
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
