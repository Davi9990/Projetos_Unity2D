using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaqueRaioSolar : MonoBehaviour
{
    public GameObject raioSolarParticlesPrefab; // Prefab do sistema de part�culas de carregamento
    public GameObject projetilPrefab; // Prefab do proj�til
    public Transform spawnPoint; // Ponto onde o projetil ser� gerado
    public float tempoCarregamento = 1.5f; // Tempo de carregamento antes do disparo
    public float velocidadeProjetil = 10f; // Velocidade do proj�til
    private bool estaCarregando = false;
    private bool estaProntoParaDisparar = false;
    private GameObject raioSolarParticlesInstance; // Inst�ncia do sistema de part�culas de carregamento
    private Coroutine carregamentoCoroutine; // Armazena a refer�ncia da Coroutine de carregamento

    // Sons
    public AudioClip somDeCarregamento; // Som reproduzido ao carregar
    public AudioClip somDeLancamento; // Som reproduzido ao lan�ar o proj�til
    private AudioSource audioSource; // Fonte de �udio do personagem

    // Vari�veis de dano
    public LayerMask EnemyLayers; // Layers dos inimigos que podem ser atingidos
    public float ataqueRanger = 3.2f; // Alcance do ataque

    void Start()
    {
        // Obt�m ou adiciona o componente AudioSource ao personagem
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        // Inicia o carregamento quando a tecla C � pressionada
        if (Input.GetKeyDown(KeyCode.C) && !estaCarregando)
        {
            StartCarregamento();
        }

        // Atualiza a posi��o das part�culas para seguir o personagem
        if (raioSolarParticlesInstance != null)
        {
            raioSolarParticlesInstance.transform.position = spawnPoint.position; // Mant�m a posi��o das part�culas no ponto de spawn
        }

        // Cancela o carregamento e n�o lan�a o proj�til se a tecla C for solta antes de completar o tempo
        if (Input.GetKeyUp(KeyCode.C))
        {
            if (estaProntoParaDisparar)
            {
                Lan�arProjetil();
            }
            else
            {
                CancelarCarregamento();
            }
        }
    }

    void StartCarregamento()
    {
        estaCarregando = true;

        // Toca o som de carregamento em looping
        if (somDeCarregamento != null)
        {
            audioSource.clip = somDeCarregamento;
            audioSource.loop = true; // Define o som para looping
            audioSource.Play(); // Reproduz o som de carregamento
        }

        // Instancia o prefab do sistema de part�culas no ponto de spawn
        raioSolarParticlesInstance = Instantiate(raioSolarParticlesPrefab, spawnPoint.position, Quaternion.identity);

        // Certifique-se de que o sistema de part�culas est� ativo
        ParticleSystem ps = raioSolarParticlesInstance.GetComponent<ParticleSystem>();
        if (ps != null)
        {
            ps.Play(); // Certifique-se de que as part�culas estejam ativas
        }

        // Inicia a Coroutine de carregamento
        carregamentoCoroutine = StartCoroutine(CarregarSkill());
    }

    IEnumerator CarregarSkill()
    {
        // Aguarda o tempo de carregamento
        yield return new WaitForSeconds(tempoCarregamento);

        estaProntoParaDisparar = true; // Pronto para lan�ar o proj�til
        estaCarregando = false;
    }

    void Lan�arProjetil()
    {
        // Para o som de carregamento e toca o som de lan�amento
        if (somDeLancamento != null)
        {
            audioSource.Stop(); // Para o som de carregamento
            audioSource.loop = false; // Desativa o looping
            audioSource.PlayOneShot(somDeLancamento); // Reproduz o som de lan�amento
        }

        // Instancia o proj�til na dire��o do sprite
        GameObject projetil = Instantiate(projetilPrefab, spawnPoint.position, Quaternion.identity);

        // Obt�m o sistema de part�culas do proj�til e o ativa
        ParticleSystem projetilParticulas = projetil.GetComponentInChildren<ParticleSystem>();

        if (projetilParticulas != null)
        {
            projetilParticulas.Play(); // Ativa as part�culas do proj�til
        }

        // Define a dire��o do proj�til
        Vector2 direcao = transform.right * (GetComponent<SpriteRenderer>().flipX ? -1 : 1); // Ajusta a dire��o com base no flip

        // Aplica a velocidade ao proj�til
        projetil.GetComponent<Rigidbody2D>().velocity = direcao * velocidadeProjetil;

        // Destr�i a inst�ncia do sistema de part�culas de carregamento
        if (raioSolarParticlesInstance != null)
        {
            Destroy(raioSolarParticlesInstance);
        }

        // Adiciona dano ao proj�til
        ProjetilDano projetilDano = projetil.AddComponent<ProjetilDano>(); // Adiciona o script de dano ao proj�til
        projetilDano.Inicializar(direcao, EnemyLayers, ataqueRanger); // Passa as informa��es necess�rias

        estaProntoParaDisparar = false; // Reseta o estado
    }

    void CancelarCarregamento()
    {
        // Cancela o carregamento se o tempo de carregamento n�o for completado
        if (carregamentoCoroutine != null)
        {
            StopCoroutine(carregamentoCoroutine);
        }

        // Para o som de carregamento
        if (audioSource.isPlaying && audioSource.clip == somDeCarregamento)
        {
            audioSource.Stop();
            audioSource.loop = false; // Desativa o looping
        }

        // Destr�i a inst�ncia do sistema de part�culas de carregamento
        if (raioSolarParticlesInstance != null)
        {
            Destroy(raioSolarParticlesInstance);
        }

        estaCarregando = false;
        estaProntoParaDisparar = false; // Reseta o estado
    }
}
