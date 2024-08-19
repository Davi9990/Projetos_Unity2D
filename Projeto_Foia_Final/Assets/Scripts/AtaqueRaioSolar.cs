using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaqueRaioSolar : MonoBehaviour
{
    public GameObject raioSolarParticlesPrefab; // Prefab do sistema de partículas de carregamento
    public GameObject projetilPrefab; // Prefab do projétil
    public Transform spawnPoint; // Ponto onde o projetil será gerado
    public float tempoCarregamento = 1.5f; // Tempo de carregamento antes do disparo
    public float velocidadeProjetil = 10f; // Velocidade do projétil
    private bool estaCarregando = false;
    private bool estaProntoParaDisparar = false;
    private GameObject raioSolarParticlesInstance; // Instância do sistema de partículas de carregamento
    private Coroutine carregamentoCoroutine; // Armazena a referência da Coroutine de carregamento

    // Sons
    public AudioClip somDeCarregamento; // Som reproduzido ao carregar
    public AudioClip somDeLancamento; // Som reproduzido ao lançar o projétil
    private AudioSource audioSource; // Fonte de áudio do personagem

    // Variáveis de dano
    public LayerMask EnemyLayers; // Layers dos inimigos que podem ser atingidos
    public float ataqueRanger = 3.2f; // Alcance do ataque

    void Start()
    {
        // Obtém ou adiciona o componente AudioSource ao personagem
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        // Inicia o carregamento quando a tecla C é pressionada
        if (Input.GetKeyDown(KeyCode.C) && !estaCarregando)
        {
            StartCarregamento();
        }

        // Atualiza a posição das partículas para seguir o personagem
        if (raioSolarParticlesInstance != null)
        {
            raioSolarParticlesInstance.transform.position = spawnPoint.position; // Mantém a posição das partículas no ponto de spawn
        }

        // Cancela o carregamento e não lança o projétil se a tecla C for solta antes de completar o tempo
        if (Input.GetKeyUp(KeyCode.C))
        {
            if (estaProntoParaDisparar)
            {
                LançarProjetil();
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

        // Instancia o prefab do sistema de partículas no ponto de spawn
        raioSolarParticlesInstance = Instantiate(raioSolarParticlesPrefab, spawnPoint.position, Quaternion.identity);

        // Certifique-se de que o sistema de partículas está ativo
        ParticleSystem ps = raioSolarParticlesInstance.GetComponent<ParticleSystem>();
        if (ps != null)
        {
            ps.Play(); // Certifique-se de que as partículas estejam ativas
        }

        // Inicia a Coroutine de carregamento
        carregamentoCoroutine = StartCoroutine(CarregarSkill());
    }

    IEnumerator CarregarSkill()
    {
        // Aguarda o tempo de carregamento
        yield return new WaitForSeconds(tempoCarregamento);

        estaProntoParaDisparar = true; // Pronto para lançar o projétil
        estaCarregando = false;
    }

    void LançarProjetil()
    {
        // Para o som de carregamento e toca o som de lançamento
        if (somDeLancamento != null)
        {
            audioSource.Stop(); // Para o som de carregamento
            audioSource.loop = false; // Desativa o looping
            audioSource.PlayOneShot(somDeLancamento); // Reproduz o som de lançamento
        }

        // Instancia o projétil na direção do sprite
        GameObject projetil = Instantiate(projetilPrefab, spawnPoint.position, Quaternion.identity);

        // Obtém o sistema de partículas do projétil e o ativa
        ParticleSystem projetilParticulas = projetil.GetComponentInChildren<ParticleSystem>();

        if (projetilParticulas != null)
        {
            projetilParticulas.Play(); // Ativa as partículas do projétil
        }

        // Define a direção do projétil
        Vector2 direcao = transform.right * (GetComponent<SpriteRenderer>().flipX ? -1 : 1); // Ajusta a direção com base no flip

        // Aplica a velocidade ao projétil
        projetil.GetComponent<Rigidbody2D>().velocity = direcao * velocidadeProjetil;

        // Destrói a instância do sistema de partículas de carregamento
        if (raioSolarParticlesInstance != null)
        {
            Destroy(raioSolarParticlesInstance);
        }

        // Adiciona dano ao projétil
        ProjetilDano projetilDano = projetil.AddComponent<ProjetilDano>(); // Adiciona o script de dano ao projétil
        projetilDano.Inicializar(direcao, EnemyLayers, ataqueRanger); // Passa as informações necessárias

        estaProntoParaDisparar = false; // Reseta o estado
    }

    void CancelarCarregamento()
    {
        // Cancela o carregamento se o tempo de carregamento não for completado
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

        // Destrói a instância do sistema de partículas de carregamento
        if (raioSolarParticlesInstance != null)
        {
            Destroy(raioSolarParticlesInstance);
        }

        estaCarregando = false;
        estaProntoParaDisparar = false; // Reseta o estado
    }
}
