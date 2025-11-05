using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MissoesMobile : MonoBehaviour
{
    [Header("Referências UI")]
    public Text textoMissao, textoInteracao, textoVidas, textoAlerta;
    public Button botaoInteragir;
    public Slider barraProgresso;
    public int mapaLiberdade, morteRio;
    public GameObject tronco, painelAlerta;
    public float duracaoAlerta = 2f;

    [Header("Áudio e Animações")]
    public AudioSource audioSource;
    public AudioClip somInteragir, somConcluirMissao, somErro;
    public Animator animator;

    [Header("Minimapa")]
    public Camera miniMapaCamera;
    public int vidas = 3;
    private int etapa = 0, progressoAtual = 0, progressoNecessario = 3;
    private string objetoProximo = "";

    [Header("Sistema de Diálogo")]
    public SistemadeDialogo sistemaDialogo;
    private int npcsConversados = 0;
    private int npcsNecessarios = 1;
    private Collider npcProximo;

    [Header("Seta de Missão")]
    public SetaMissao setaMissoes;
    public Transform[] locaisMissoes;

    // Controla quais missões já foram concluídas
    private HashSet<string> missoesConcluidas = new HashSet<string>();

    void Start()
    {
        AtualizarMissao("1ª Missão\nPlante 3 canas-de-açúcar que fica localizado às margens do rio.");
        textoInteracao.text = "";
        botaoInteragir.gameObject.SetActive(false);
        barraProgresso.maxValue = progressoNecessario;
        barraProgresso.value = 0;

        if (textoVidas != null)
            textoVidas.text = "Vidas: " + vidas;

        botaoInteragir.onClick.AddListener(Interagir);
        AtualizarSeta();
    }
    void OnTriggerEnter(Collider other)
    {
        objetoProximo = other.tag;

        // Se essa missão já foi concluída, não mostra o botão
        if (missoesConcluidas.Contains(objetoProximo))
            return;

        if (other.CompareTag("MorteRio"))
        {
            MorteRio();
            return;
        }
        switch (objetoProximo)
        {
            case "Planta": MostrarInteracao("Toque para plantar cana"); break;
            case "Cana": MostrarInteracao("Toque para coletar cana"); break;
            case "Deposito": MostrarInteracao("Toque para entregar as canas"); break;
            case "Capataz": MostrarInteracao("Toque para falar com o capataz"); break;
            case "NPCDialogoJanio": npcProximo = other; MostrarInteracao("Toque para conversar com Jânio"); break;
            case "NPCDialogoNegoDan": npcProximo = other; MostrarInteracao("Toque para conversar com NegoDan"); break;
            case "NPCDialogoQuintiliano": npcProximo = other; MostrarInteracao("Toque para conversar com Quintiliano"); break;
            case "Arvore": MostrarInteracao("Toque para coletar troncos"); break;
            case "Fornalha": MostrarInteracao("Toque para colocar troncos"); break;
            case "NPCMapa": MostrarInteracao("Toque para pegar o mapa"); break;
            case "Tronco": MostrarInteracao("Toque para destruir o tronco da opressão"); break;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == objetoProximo)
        {
            objetoProximo = "";
            textoInteracao.text = "";
            botaoInteragir.gameObject.SetActive(false);
        }
    }
    void MostrarInteracao(string msg)
    {
        if (!botaoInteragir.gameObject.activeSelf)
        {
            textoInteracao.text = msg;
            botaoInteragir.gameObject.SetActive(true);
        }
    }
    void Interagir()
    {
        if (objetoProximo == "" || vidas <= 0) return;

        bool acaoCorreta = false;

        if (somInteragir != null && audioSource != null)
            audioSource.PlayOneShot(somInteragir);

        switch (objetoProximo)
        {
            // Missão com 3 interações — não marca como concluída ainda
            case "Planta":
                if (etapa == 0)
                {
                    acaoCorreta = true;
                    IncrementarProgresso("2ª Missão\nColete 3 canas de açúcar nas margens do rio.", 1);
                }
                break;

            case "Cana":
                if (etapa == 1)
                {
                    acaoCorreta = true;
                    IncrementarProgresso("3ª Missão\nLeve as canas ao depósito perto do moinho.", 2);
                }
                break;

            case "Deposito":
                if (etapa == 2)
                {
                    acaoCorreta = true;
                    ProximaEtapa("4ª Missão\nProcure o Capataz que anda pelo mapa.", 3);
                    missoesConcluidas.Add("Deposito");
                }
                break;

            case "Capataz":
                if (etapa == 3)
                {
                    acaoCorreta = true;
                    ProximaEtapa("5ª Missão\nConverse com Jânio perto das árvores antigas.", 4);
                    missoesConcluidas.Add("Capataz");
                }
                break;

            case "NPCDialogoJanio":
                if (etapa == 4)
                {
                    acaoCorreta = true;
                    IniciarDialogoNPC("6ª Missão\nVá até NegoDan, próximo ao Jânio.", 5);
                    missoesConcluidas.Add("NPCDialogoJanio");
                }
                break;

            case "NPCDialogoNegoDan":
                if (etapa == 5)
                {
                    acaoCorreta = true;
                    IniciarDialogoNPC("7ª Missão\nConverse com Quintiliano, próximo a NegoDan.", 6);
                    missoesConcluidas.Add("NPCDialogoNegoDan");
                }
                break;

            case "NPCDialogoQuintiliano":
                if (etapa == 6)
                {
                    acaoCorreta = true;
                    IniciarDialogoNPC("8ª Missão\nColete 3 troncos nas margens do rio.", 7);
                    missoesConcluidas.Add("NPCDialogoQuintiliano");
                }
                break;

            case "Arvore":
                if (etapa == 7)
                {
                    acaoCorreta = true;
                    animator?.SetTrigger("Coletar");
                    IncrementarProgresso("9ª Missão\nLeve os troncos para a fornalha do moinho.", 8);
                }
                break;

            case "Fornalha":
                if (etapa == 8)
                {
                    acaoCorreta = true;
                    animator?.SetTrigger("Entregar");
                    ProximaEtapa("10ª Missão\nFale com o Vovô e pegue o mapa.", 9);
                    missoesConcluidas.Add("Fornalha");
                }
                break;

            case "NPCMapa":
                if (etapa == 9)
                {
                    acaoCorreta = true;
                    mapaLiberdade++;
                    animator?.SetTrigger("Pegar");
                    ProximaEtapa("11ª Missão\nVá até o tronco da opressão em frente à casa grande.", 10);
                    audioSource?.PlayOneShot(somConcluirMissao);
                    missoesConcluidas.Add("NPCMapa");
                }
                break;

            case "Tronco":
                if (etapa == 10)
                {
                    acaoCorreta = true;
                    animator?.SetTrigger("Tronco");
                    ProximaEtapa("Você destruiu o tronco da opressão!\nTodas as missões foram concluídas!", 11);
                    if (tronco != null) Destroy(tronco);
                    audioSource?.PlayOneShot(somConcluirMissao);
                    missoesConcluidas.Add("Tronco");
                }
                break;
        }
        if (!acaoCorreta)
        {
            PerderVida();
        }
        textoInteracao.text = "";
        botaoInteragir.gameObject.SetActive(false);
    }
    void IncrementarProgresso(string proximaMissao, int proximaEtapa)
    {
        progressoAtual++;
        barraProgresso.value = progressoAtual;

        if (progressoAtual >= progressoNecessario)
        {
            //Missão completa, agora sim marcamos como concluída
            if (!missoesConcluidas.Contains(objetoProximo))
                missoesConcluidas.Add(objetoProximo);

            progressoAtual = 0;
            barraProgresso.value = 0;
            etapa = proximaEtapa - 1;
            ProximaEtapa(proximaMissao, proximaEtapa);
        }
    }
    void IniciarDialogoNPC(string proximaMissao, int proximaEtapa)
    {
        botaoInteragir.gameObject.SetActive(false);
        SistemadeDialogo npcDialogo = npcProximo.GetComponent<SistemadeDialogo>();
        if (npcDialogo != null)
        {
            npcDialogo.aoFinalizarDialogo = () =>
            {
                npcsConversados++;
                if (npcsConversados >= npcsNecessarios)
                    ProximaEtapa(proximaMissao, proximaEtapa);
            };
            npcDialogo.IniciarDialogo();
        }
    }
    void ProximaEtapa(string msg, int novaEtapa)
    {
        etapa = novaEtapa;
        AtualizarMissao(msg);
        botaoInteragir.gameObject.SetActive(false);
        AtualizarSeta();
    }
    void AtualizarMissao(string msg)
    {
        textoMissao.text = msg;
        Debug.Log(msg);
    }
    void AtualizarSeta()
    {
        if (setaMissoes == null || locaisMissoes == null || locaisMissoes.Length == 0)
            return;

        if (etapa < locaisMissoes.Length)
            setaMissoes.DefinirAlvo(locaisMissoes[etapa]);
        else
            setaMissoes.DefinirAlvo(null);
    }
    void MorteRio()
    {
        vidas = 0;
        textoVidas.text = "Vidas: " + vidas;
        MostrarAlerta("Você caiu no rio! Game Over!");
        textoMissao.text = "Você morreu! Fim de jogo!";
        audioSource?.PlayOneShot(somErro);
        botaoInteragir.gameObject.SetActive(false);
        textoInteracao.text = "";
        Invoke(nameof(CarregarGameOver), 3f);
    }
    public void PerderVida()
    {
        vidas--;
        audioSource?.PlayOneShot(somErro);
        textoVidas.text = "Vidas: " + vidas;
        MostrarAlerta("ATENÇÃO! \nMissão incorreta! Você perdeu uma vida!");
        if (vidas <= 0)
        {
            textoMissao.text = "Fim de jogo! Você perdeu todas as vidas!";
            botaoInteragir.gameObject.SetActive(false);
            textoInteracao.text = "";
            Invoke(nameof(CarregarGameOver), 3f);
        }
    }
    void CarregarGameOver()
    {
        SceneManager.LoadScene("Fase1");
    }
    void MostrarAlerta(string mensagem)
    {
        if (textoAlerta != null)
        {
            textoAlerta.text = mensagem;
            textoAlerta.gameObject.SetActive(true);
            CancelInvoke(nameof(EsconderAlerta));
            Invoke(nameof(EsconderAlerta), duracaoAlerta);
        }
        if (painelAlerta != null)
        {
            painelAlerta.SetActive(true);
            CancelInvoke(nameof(EsconderPainelAlerta));
            Invoke(nameof(EsconderPainelAlerta), duracaoAlerta);
        }
    }
    void EsconderAlerta()
    {
        if (textoAlerta != null)
            textoAlerta.gameObject.SetActive(false);
    }
    void EsconderPainelAlerta()
    {
        if (painelAlerta != null)
            painelAlerta.SetActive(false);
    }
}
