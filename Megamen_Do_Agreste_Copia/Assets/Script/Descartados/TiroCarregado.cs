using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiroCarregado : MonoBehaviour
{
    public Transform Hand;
    public GameObject Tiros;
    public float speedBullets;
    public int maxDamage = 10;
    public float tempoMaxCarregamento = 1.5f;
    public bool estaCarregando = false;
    private Coroutine CoroutineCarregamento;
    private int danoAtual;
    private float tempoCarregando;
    private Move virando;
    private PlayerLogica virando2;
    private GameObject projetilCarregado; // Referência ao projétil carregado
    public Rigidbody2D Player;
    private PlayerLogica Pause;

    public float cooldown = 1f; // Tempo de cooldown entre ataques
    private bool podeAtirar = true; // Controla se o jogador pode atirar

    void Start()
    {
        Player = GetComponent<Rigidbody2D>();
        virando = GetComponent<Move>();
        virando2 = GetComponent<PlayerLogica>();
        Pause = GetComponent<PlayerLogica>();   

        if (virando2 == null)
        {
            Debug.LogError("Move não encontrado no GameObject do Player.");
        }
    }

    void Update()
    {
        if (Pause.isPaused == false)
        {
            CarregandoTiro();

            if (estaCarregando && projetilCarregado != null)
            {
                projetilCarregado.transform.position = Hand.position;
            }
        }
    }

    public void CarregandoTiro()
    {
        if (Input.GetKeyDown(KeyCode.F) && podeAtirar)
        {
            estaCarregando = true;
            tempoCarregando = 0f;
            danoAtual = 0; // Reinicia o dano a cada novo tiro carregado

            // Instancia o projétil e o mantém no ponto de disparo
            projetilCarregado = Instantiate(Tiros, Hand.position, Quaternion.identity);
            projetilCarregado.transform.SetParent(Hand); // Anexa ao ponto de disparo

            // Ativa a animação de carregamento do projétil
            Animator projAnimator = projetilCarregado.GetComponent<Animator>();
            if (projAnimator != null)
            {
                projAnimator.SetBool("Carregando", true);
            }

            // Inicia o Coroutine se não estiver rodando
            if (CoroutineCarregamento == null)
            {
                CoroutineCarregamento = StartCoroutine(CarregandoCarga());
            }
        }

        if (Input.GetKeyUp(KeyCode.F))
        {
            estaCarregando = false;

            // Para o Coroutine se ele tiver sido iniciado
            if (CoroutineCarregamento != null)
            {
                StopCoroutine(CoroutineCarregamento);
                CoroutineCarregamento = null;
            }

            LançarProjetil();
        }
    }

    IEnumerator CarregandoCarga()
    {
        while (estaCarregando)
        {
            tempoCarregando += Time.deltaTime;

            // Incrementa o dano enquanto o tempoCarregando está abaixo do limite e o dano não atingiu o máximo
            if (tempoCarregando <= tempoMaxCarregamento && danoAtual < maxDamage)
            {
                danoAtual++; // Incrementa o dano em 1
            }
            else
            {
                // Carregamento máximo atingido
                estaCarregando = false;
                LançarProjetil();
                yield break; // Interrompe a Coroutine após lançar o projétil
            }

            yield return new WaitForSeconds(0.1f); // Define a frequência de incremento do dano
        }
        CoroutineCarregamento = null; // Redefine após término
    }

    public void LançarProjetil()
    {
        if (projetilCarregado == null || !podeAtirar) return;

        // Define que o jogador não pode atirar novamente até o cooldown terminar
        podeAtirar = false;
        StartCoroutine(CooldownRotina());

        // Desanexa o projétil do ponto de disparo
        projetilCarregado.transform.SetParent(null);

        // Troca a animação para o estado de disparo
        Animator projAnimator = projetilCarregado.GetComponent<Animator>();
        if (projAnimator != null)
        {
            projAnimator.SetBool("Carregando", false);
            projAnimator.SetTrigger("Disparo");
        }

        Rigidbody2D firerb = projetilCarregado.GetComponent<Rigidbody2D>();

        float direction = virando2 != null && virando2.isFacingRight ? 1 : -1;
        float bulletSpeed = speedBullets + Mathf.Abs(Player.velocity.x);
        firerb.velocity = new Vector2(direction * bulletSpeed, 0);

        // Define o valor de dano no script do projétil
        BulletDamagePlayerCarregado projScript = projetilCarregado.GetComponent<BulletDamagePlayerCarregado>();
        if (projScript != null)
        {
            projScript.damage = danoAtual;
        }
        else
        {
            Debug.LogError("BulletDamagePlayerCarregado não encontrado no projétil.");
        }

        Destroy(projetilCarregado, 6f);

        // Reseta o tempo de carregamento para o próximo tiro
        tempoCarregando = 0f;
        CoroutineCarregamento = null; // Garante que o Coroutine seja redefinido
        projetilCarregado = null; // Reseta a referência do projétil carregado
    }

    private IEnumerator CooldownRotina()
    {
        yield return new WaitForSeconds(cooldown);
        podeAtirar = true;
    }
}
