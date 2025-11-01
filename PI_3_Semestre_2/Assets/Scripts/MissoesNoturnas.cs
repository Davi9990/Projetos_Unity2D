using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MissoesNoturnas : MonoBehaviour
{
    [Header("Referências UI")]
    public Text textoMissao, textoInteracao, textoVidas, textoAlerta;
    public Button botaoInteragir;
    public Slider barraProgresso;
    public GameObject painelAlerta;
    public float duracaoAlerta = 2f;
    public int vidas = 3;

    [Header("Áudio e Animação")]
    public AudioSource audioSource;
    public AudioClip somInteragir, somConcluirMissao, somErro;
    public Animator animator;

    [Header("Seta Missão")]
    public SetaMissao setaMissoes;
    public Transform[] locaisMissoes;

    private int etapa = 0;
    private int progressoAtual = 0;
    private int progressoNecessario = 3;
    private string objetoProximo = "";

    // Controle dos pontos das missoes
    private HashSet<Transform> pontosVisitados = new HashSet<Transform>();
    private int indicePontoAtual = 0;

    void Start()
    {
        AtualizarMissao("1ª Missão\nMapeie o engenho: observe 3 pontos estratégicos a rota, o esconderijo e a patrulha).");
        textoInteracao.text = "";
        botaoInteragir.gameObject.SetActive(false);
        barraProgresso.maxValue = progressoNecessario;
        barraProgresso.value = 0;

        if (textoVidas != null)
            textoVidas.text = "Vidas: " + vidas;

        botaoInteragir.onClick.AddListener(Interagir);
        AtualizarSeta();
    }
    private void OnTriggerEnter(Collider other)
    {
        objetoProximo = other.tag;

        switch (objetoProximo)
        {
            case "PontoMapa": MostrarInteracao("Toque para observar a área"); break;
            case "Ferramenta": MostrarInteracao("Toque para coletar a ferramenta"); break;
            case "Companheiro": MostrarInteracao("Toque para entregar o objeto"); break;
            case "PontoVigilancia": MostrarInteracao("Toque para observar a vigilância"); break;
            case "SabotagemTocha": MostrarInteracao("Toque para apagar a tocha"); break;
            case "SabotagemCarroca": MostrarInteracao("Toque para danificar a carroça"); break;
            case "SabotagemSuprimentos": MostrarInteracao("Toque para esconder os suprimentos"); break;
            case "Aliados": MostrarInteracao("Toque para se reunir com os aliados"); break;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        objetoProximo = collision.collider.tag;

        switch (objetoProximo)
        {
            case "PontoMapa": MostrarInteracao("Toque para observar a área"); break;
            case "Ferramenta": MostrarInteracao("Toque para coletar a ferramenta"); break;
            case "Companheiro": MostrarInteracao("Toque para entregar o objeto"); break;
            case "PontoVigilancia": MostrarInteracao("Toque para observar a vigilância"); break;
            case "SabotagemTocha": MostrarInteracao("Toque para apagar a tocha"); break;
            case "SabotagemCarroca": MostrarInteracao("Toque para danificar a carroça"); break;
            case "SabotagemSuprimentos": MostrarInteracao("Toque para esconder os suprimentos"); break;
            case "Aliados": MostrarInteracao("Toque para se reunir com os aliados"); break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == objetoProximo)
        {
            objetoProximo = "";
            textoInteracao.text = "";
            botaoInteragir.gameObject.SetActive(false);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.collider.tag == objetoProximo)
        {
            objetoProximo = "";
            textoInteracao.text = "";
            botaoInteragir.gameObject.SetActive(false);
        }
    }

    void MostrarInteracao(string msg)
    {
        textoInteracao.text = msg;
        botaoInteragir.gameObject.SetActive(true);
    }
    void Interagir()
    {
        if (objetoProximo == "" || vidas <= 0) return;
        bool acaoCorreta = false;

        audioSource?.PlayOneShot(somInteragir);

        switch (objetoProximo)
        {
            // Etapa 1: Mapeamento
            case "PontoMapa":
                if (etapa == 0)
                {
                    Collider pontoAtual = null;
                    foreach (var col in Physics.OverlapSphere(transform.position, 2f))
                        if (col.CompareTag("PontoMapa")) { pontoAtual = col; break; }

                    if (pontoAtual != null && !pontosVisitados.Contains(pontoAtual.transform))
                    {
                        pontosVisitados.Add(pontoAtual.transform);
                        progressoAtual++;
                        barraProgresso.value = progressoAtual;
                        acaoCorreta = true;

                        indicePontoAtual++;
                        if (indicePontoAtual < locaisMissoes.Length)
                            setaMissoes.DefinirAlvo(locaisMissoes[indicePontoAtual]);
                        else
                            setaMissoes.DefinirAlvo(null);

                        if (progressoAtual >= 3)
                        {
                            IncrementarProgresso("2ª Missão\nColete 4 ferramentas no armazém.", 1, 3, 4);
                            pontosVisitados.Clear();
                            indicePontoAtual = 0;
                        }
                    }
                }
                break;

            // Etapa 2: Coleta de ferramentas
            case "Ferramenta":
                if (etapa == 1)
                {
                    Collider ferramentaAtual = null;
                    foreach (var col in Physics.OverlapSphere(transform.position, 2f))
                        if (col.CompareTag("Ferramenta")) { ferramentaAtual = col; break; }

                    if (ferramentaAtual != null && !pontosVisitados.Contains(ferramentaAtual.transform))
                    {
                        pontosVisitados.Add(ferramentaAtual.transform);
                        progressoAtual++;
                        barraProgresso.value = progressoAtual;
                        acaoCorreta = true;

                        Destroy(ferramentaAtual.gameObject);
                        Debug.Log($"Ferramenta destruída: {ferramentaAtual.name}");

                        AtualizarSeta();

                        if (progressoAtual >= 4)
                        {
                            IncrementarProgresso("3ª Missão\nEntregue os objetos escondidos aos companheiros.", 2, 4, 2);
                            pontosVisitados.Clear();
                        }
                    }
                }
                break;
            // Etapa 3: Entrega de objetos
            case "Companheiro":
                if (etapa == 2)
                {
                    Collider comp = null;
                    foreach (var col in Physics.OverlapSphere(transform.position, 2f))
                        if (col.CompareTag("Companheiro")) { comp = col; break; }

                    if (comp != null && !pontosVisitados.Contains(comp.transform))
                    {
                        pontosVisitados.Add(comp.transform);
                        progressoAtual++;
                        barraProgresso.value = progressoAtual;
                        acaoCorreta = true;

                        AtualizarSeta();

                        if (progressoAtual >= 4)
                        {
                            IncrementarProgresso("4ª Missão\nObserve 3 áreas vigiadas para identificar pontos fracos.", 3, 4, 3);
                            pontosVisitados.Clear();
                            progressoAtual = 0;
                            barraProgresso.value = 0;
                            indicePontoAtual = 0;
                        }
                    }
                }
                break;
            // Etapa 4: Vigilância
            case "PontoVigilancia":
                if (etapa == 3)
                {
                    Collider vig = null;
                    foreach (var col in Physics.OverlapSphere(transform.position, 2f))
                        if (col.CompareTag("PontoVigilancia")) { vig = col; break; }

                    if (vig != null && !pontosVisitados.Contains(vig.transform))
                    {
                        pontosVisitados.Add(vig.transform);
                        progressoAtual++;
                        barraProgresso.value = progressoAtual;
                        acaoCorreta = true;

                        AtualizarSeta();

                        if (progressoAtual >= progressoNecessario)
                        {
                            IncrementarProgresso("5ª Missão\nRealize sabotagens: apague tochas, danifique carroças e esconda suprimentos.", 4, 3, 3);
                            pontosVisitados.Clear();
                            progressoAtual = 0;
                            barraProgresso.value = 0;
                        }
                    }
                }
                break;
            // Etapa 5: Sabotagens
            case "SabotagemTocha":
            case "SabotagemCarroca":
            case "SabotagemSuprimentos":
                if (etapa == 4)
                {
                    Collider sab = null;
                    foreach (var col in Physics.OverlapSphere(transform.position, 2f))
                        if (col.CompareTag(objetoProximo)) { sab = col; break; }

                    if (sab != null && !pontosVisitados.Contains(sab.transform))
                    {
                        pontosVisitados.Add(sab.transform);
                        progressoAtual++;
                        barraProgresso.value = progressoAtual;
                        acaoCorreta = true;
                        AtualizarSeta();

                        if (progressoAtual >= progressoNecessario)
                        {
                            IncrementarProgresso("6ª Missão\nReúna-se com seus aliados no ponto de encontro.", 5, 3, 1);
                            pontosVisitados.Clear();
                            progressoAtual = 0;
                            barraProgresso.value = 0;
                        }
                    }
                }
                break;
            // Etapa 6: Reunião final
            case "Aliados":
                if (etapa == 5)
                {
                    acaoCorreta = true;
                    ConcluirFase();
                }
                break;
        }
        if (!acaoCorreta)
            PerderVida();
    }
    void IncrementarProgresso(string proximaMissao, int proximaEtapa, int progressoEtapaAnterior, int novoProgressoNecessario)
    {
        progressoAtual = 0;
        barraProgresso.value = 0;
        progressoNecessario = novoProgressoNecessario;
        ProximaEtapa(proximaMissao, proximaEtapa);
        audioSource?.PlayOneShot(somConcluirMissao);
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
    //  NOVA VERSAO COMPLETA DA ATUALIZACAO DA SETA PARA QUE FUNCIONE O QUE ESTAVA DANDO ERRO, MAS NAO MODIFIQUEI NADA LA QUE VOCE FEZ
    void AtualizarSeta()
    {
        if (setaMissoes == null) return;

        switch (etapa)
        {
            case 0:
                if (indicePontoAtual < locaisMissoes.Length)
                    setaMissoes.DefinirAlvo(locaisMissoes[indicePontoAtual]);
                else
                    setaMissoes.DefinirAlvo(null);
                break;

            case 1: AtualizarSetaPorTag("Ferramenta"); break;
            case 2: AtualizarSetaPorTag("Companheiro"); break;
            case 3: AtualizarSetaPorTag("PontoVigilancia"); break;
            case 4: AtualizarSetaPorTagMultipla(new string[] { "SabotagemTocha", "SabotagemCarroca", "SabotagemSuprimentos" }); break;
            case 5: AtualizarSetaPorTag("Aliados"); break;
            default: setaMissoes.DefinirAlvo(null); break;
        }
    }
    void AtualizarSetaPorTag(string tag)
    {
        GameObject[] objetos = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject obj in objetos)
        {
            if (!pontosVisitados.Contains(obj.transform))
            {
                setaMissoes.DefinirAlvo(obj.transform);
                return;
            }
        }
        setaMissoes.DefinirAlvo(null);
    }
    void AtualizarSetaPorTagMultipla(string[] tags)
    {
        foreach (string tag in tags)
        {
            GameObject[] objetos = GameObject.FindGameObjectsWithTag(tag);
            foreach (GameObject obj in objetos)
            {
                if (!pontosVisitados.Contains(obj.transform))
                {
                    setaMissoes.DefinirAlvo(obj.transform);
                    return;
                }
            }
        }
        setaMissoes.DefinirAlvo(null);
    }
    void ConcluirFase()
    {
        AtualizarMissao("Você se reuniu com seus aliados!\nTodas as tarefas foram concluídas!");
        audioSource?.PlayOneShot(somConcluirMissao);
        Invoke(nameof(CarregarProximaFase), 3f);
    }
    void CarregarProximaFase()
    {
        SceneManager.LoadScene("Fase3");
    }
    void PerderVida()
    {
        vidas--;
        audioSource?.PlayOneShot(somErro);
        textoVidas.text = "Vidas: " + vidas;
        MostrarAlerta("Ação incorreta! Você perdeu uma vida!");

        if (vidas <= 0)
        {
            textoMissao.text = "Fim de jogo! Você perdeu todas as vidas!";
            botaoInteragir.gameObject.SetActive(false);
            textoInteracao.text = "";
            Invoke(nameof(ReiniciarFase), 3f);
        }
    }
    void ReiniciarFase()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
        {
            textoAlerta.gameObject.SetActive(false);
        }            
    }
    void EsconderPainelAlerta()
    {
        if (painelAlerta != null)
        {
            painelAlerta.SetActive(false);
        }            
    }
}
