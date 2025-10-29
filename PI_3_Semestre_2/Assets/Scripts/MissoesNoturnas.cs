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

    void Start()
    {
        AtualizarMissao("1° Missão\nMapeie o engenho: observe 3 ponto estratégicos(rotas, esconderijos e patrulhas)");
        textoInteracao.text = "";
        botaoInteragir.gameObject.SetActive(false);
        barraProgresso.maxValue = progressoNecessario;
        barraProgresso.value = 0;

        if(textoVidas != null)
        {
            textoVidas.text = "Vidas: " + vidas;
        }

        botaoInteragir.onClick.AddListener(Interagir);
        AtualizarSeta();
    }

    private void OnTriggerEnter(Collider other)
    {
        objetoProximo = other.tag;

        switch (objetoProximo) 
        {
            case "PontoMapa": MostrarInteracao("Toque para observar a área");  break;
            case "Ferramenta": MostrarInteracao("Toque para coletar a ferramenta"); break;
            case "Companheiro": MostrarInteracao("Toque para entregar objeto"); break;
            case "PontoVigilancia": MostrarInteracao("Toque para observar a vigilância"); break;
            case "SabotagemTocha": MostrarInteracao("Toque para apagar a tocha"); break;
            case "SabotagemCarroca": MostrarInteracao("Toque para danificar carroça"); break;
            case "SabotagemSuprimentos": MostrarInteracao("Toque para esconder suprimentos"); break;
            case "Aliados": MostrarInteracao("Toque para se reunir com os aliados"); break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == objetoProximo)
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
        if(objetoProximo == "" || vidas <= 0) return;
        bool acaoCorreta = false;

        audioSource?.PlayOneShot(somInteragir);

        switch (objetoProximo)
        {
            //Etapa 1: Mapeamento do engenho
            case "PontoMapa":
                if(etapa == 0)
                {
                    acaoCorreta = true;
                    IncrementarProgresso("2° Missão\nColete 4 ferramentas no armazem.", 1, 3, 4);
                }
                break;

            //Etapa 2: Coleta de Ferramentas
            case "Ferramenta":
                if (etapa == 1) 
                {
                    acaoCorreta = true;
                    IncrementarProgresso("3° Missão\nEntregue os objetos escondidos aos companheiros.", 2, 4, 2);                    
                }
                break;

            //Etapa 3: Entrega de objetos
            case "Companheiro":
                if(etapa == 2)
                {
                    acaoCorreta = true;
                    IncrementarProgresso("4° Missão\nObserve 3 áreas vigiadas para identificas pontos fracos", 3, 2, 3);
                }
                break;

            //Etapa 4: Observar áreas de vigilância
            case "PontoVigilancia":
                if(etapa == 3)
                {
                    acaoCorreta = true;
                    IncrementarProgresso("5° Missão\nRealize sabotagens: apague tochas, danifique carroça e esconda suprimentos.", 4, 3, 3);
                }
                break;

            //Etapa 5: Sabotagens
            case "SabotagemTocha":
            case "SabotagemCarroca":
            case "SabotagemSuprimentos":
                if(etapa == 4)
                {
                    acaoCorreta = true;
                    IncrementarProgresso("6° Missão\nReúna-se com seus aliados no ponto de encontro", 5, 3, 1);
                }
                break;

            //Etapa 6: Reunião com aliados
            case "Aliados":
                if(etapa == 5)
                {
                    acaoCorreta = true;
                    ConcluirFase();
                }
                break;
        }

        if (!acaoCorreta)
        {
            PerderVida();
        }
    }

    void IncrementarProgresso(string proximaMissao, int proximaEtapa, int progressoEtapaAnterior, int novoProgressoNecessario)
    {
        progressoAtual++;

        barraProgresso.value = progressoAtual;

        if(progressoAtual >= progressoNecessario)
        {
            progressoAtual = 0;
            barraProgresso.value = 0;
            progressoNecessario = novoProgressoNecessario;
            ProximaEtapa(proximaMissao, proximaEtapa);
            audioSource?.PlayOneShot(somConcluirMissao);
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
        {
            setaMissoes.DefinirAlvo(locaisMissoes[etapa]);
        }
        else
        {
            setaMissoes.DefinirAlvo(null);
        }
    }

    void ConcluirFase()
    {
        AtualizarMissao("Você se reuniu com seus aliados!\nTodas as tarefas foram concluidas");
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
        if(textoAlerta != null)
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
