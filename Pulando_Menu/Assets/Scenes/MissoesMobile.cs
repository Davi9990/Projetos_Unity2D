using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MissoesMobile : MonoBehaviour
{
    [Header("Referencias UI para nao ficar perdidos")]
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

    void Start()
    {
        AtualizarMissao("Plante 3 canas-de-açúcar.");
        textoInteracao.text = "";
        botaoInteragir.gameObject.SetActive(false);
        barraProgresso.maxValue = progressoNecessario;
        barraProgresso.value = 0;

        if (textoVidas != null)
        {
            textoVidas.text = "Vidas: " + vidas;
        }
        botaoInteragir.onClick.AddListener(Interagir);
    }
    void OnTriggerEnter(Collider other)
    {
        objetoProximo = other.tag;

        //  Morte imediata ao cair no rio, verifiquem a respito de animação do rio blz?
        if (other.CompareTag("MorteRio"))
        {
            MorteRio();
            return;
        }
        // aqui é o switch case dos objetivos da missao
        switch (objetoProximo)
        {
            case "Planta":
                MostrarInteracao("Toque para plantar cana");
                break;
            case "Cana":
                MostrarInteracao("Toque para coletar cana");
                break;
            case "Deposito":
                MostrarInteracao("Toque para entregar as canas");
                break;
            case "Capataz":
                MostrarInteracao("Toque para falar com o capataz");
                break;
            case "NPCDialogoJanio":
                npcProximo = other;
                MostrarInteracao("Toque para conversar com Jânio");
                break;
            case "NPCDialogoNegoDan":
                npcProximo = other;
                MostrarInteracao("Toque para conversar com NegoDan");
                break;
            case "NPCDialogoQuintiliano":
                npcProximo = other;
                MostrarInteracao("Toque para conversar com Quintiliano");
                break;    
            case "Arvore":
                MostrarInteracao("Toque para coletar troncos");
                break;
            case "Fornalha":
                MostrarInteracao("Toque para colocar troncos");
                break;
            case "NPCMapa":
                MostrarInteracao("Toque para pegar o mapa");
                break;
            case "Tronco":
                MostrarInteracao("Toque para destruir o tronco de tortura");
                break;
        }
    }    void OnTriggerExit(Collider other)
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
        textoInteracao.text = msg;
        botaoInteragir.gameObject.SetActive(true);
    }
    void Interagir()
    {
        if (objetoProximo == "" || vidas <= 0)
        {
            return;
        }
        bool acaoCorreta = false;

        if (somInteragir != null && audioSource != null)
        {
            audioSource.PlayOneShot(somInteragir);
        }          

        switch (objetoProximo)
        {
            case "Planta":
                if (etapa == 0)
                {
                    acaoCorreta = true;
                    animator?.SetTrigger("Plantar");
                    IncrementarProgresso("Agora colete 3 canas.", 1);
                }
                break;
            case "Cana":
                if (etapa == 1)
                {
                    acaoCorreta = true;
                    animator?.SetTrigger("Coletar");
                    IncrementarProgresso("Leve as canas até o depósito.", 2);
                }
                break;
            case "Deposito":
                if (etapa == 2)
                {
                    acaoCorreta = true;
                    animator?.SetTrigger("Entregar");
                    ProximaEtapa("Cana entregue, converse com o capataz para ter mais informações.", 3);
                }
                break;
            case "Capataz":
                if (etapa == 3)
                {
                    acaoCorreta = true;
                    ProximaEtapa("Por que parou de fazer suas tarefas? \nVá trabalhar ou lhe coloco no tronco!\nConverse com Jânio.", 4);
                }
                break;
            case "NPCDialogoJanio":
                if (etapa == 4)
                {
                    acaoCorreta = true;
                    botaoInteragir.gameObject.SetActive(false);

                    SistemadeDialogo npcDialogo = npcProximo.GetComponent<SistemadeDialogo>();
                    if (npcDialogo != null)
                    {
                        npcDialogo.aoFinalizarDialogo = () =>
                        {
                            npcsConversados++;
                            if (npcsConversados >= npcsNecessarios)
                            {
                                ProximaEtapa("Você conversou com Jânio, agora vá até o NegoDan.", 5);                               
                            }
                        };
                        npcDialogo.IniciarDialogo();
                    }
                }
                break;
            case "NPCDialogoNegoDan":
                if (etapa == 5)
                {
                    acaoCorreta = true;
                    botaoInteragir.gameObject.SetActive(false);

                    SistemadeDialogo npcDialogo = npcProximo.GetComponent<SistemadeDialogo>();
                    if (npcDialogo != null)
                    {
                        npcDialogo.aoFinalizarDialogo = () =>
                        {
                            npcsConversados++;
                            if (npcsConversados >= npcsNecessarios)
                            {
                                ProximaEtapa("Você conversou com NegoDan, agora vá até o Quintiliano.", 6);
                            }
                        };
                        npcDialogo.IniciarDialogo();
                    }
                }
                break;
            case "NPCDialogoQuintiliano":
                if (etapa == 6)
                {
                    acaoCorreta = true;
                    botaoInteragir.gameObject.SetActive(false);

                    SistemadeDialogo npcDialogo = npcProximo.GetComponent<SistemadeDialogo>();
                    if (npcDialogo != null)
                    {
                        npcDialogo.aoFinalizarDialogo = () =>
                        {
                            npcsConversados++;
                            if (npcsConversados >= npcsNecessarios)
                            {                                
                                ProximaEtapa("Você conversou com todos os trabalhadores, agora colete 3 troncos de madeira.", 7);
                            }
                        };

                        npcDialogo.IniciarDialogo();
                    }
                }
                break;
            case "Arvore":
                if (etapa == 7)
                {
                    acaoCorreta = true;
                    animator?.SetTrigger("Coletar");
                    IncrementarProgresso("Coloque os troncos na fornalha.", 8);
                }
                break;
            case "Fornalha":
                if (etapa == 8)
                {
                    acaoCorreta = true;
                    animator?.SetTrigger("Entregar");
                    ProximaEtapa("Pegue o mapa com o vovô em frente à senzala.", 9);
                }
                break;
            case "NPCMapa":
                if (etapa == 9)
                {
                    acaoCorreta = true;
                    mapaLiberdade++;
                    animator?.SetTrigger("Pegar");
                    ProximaEtapa("Você pegou o mapa da liberdade!\nAgora vá até o tronco da opressão.", 10);

                    if (somConcluirMissao != null && audioSource != null)
                        audioSource.PlayOneShot(somConcluirMissao);
                }
                break;
            case "Tronco":
                if (etapa == 10)
                {
                    acaoCorreta = true;
                    animator?.SetTrigger("Tronco");
                    ProximaEtapa("Missão concluída!\nVocê destruiu o tronco da opressão!\nAgora vá até a Berenice, a ama de leite do senhorzinho.", 11);

                    if (tronco != null)
                        Destroy(tronco);

                    if (somConcluirMissao != null && audioSource != null)
                        audioSource.PlayOneShot(somConcluirMissao);
                }
                break;        }

        if (!acaoCorreta)
        {
            Debug.LogWarning("Ação incorreta: " + objetoProximo + " na etapa " + etapa);
            PerderVida();
        }
    }
    void IncrementarProgresso(string proximaMissao, int proximaEtapa)
    {
        progressoAtual++;
        barraProgresso.value = progressoAtual;

        if (progressoAtual >= progressoNecessario)
        {
            progressoAtual = 0;
            barraProgresso.value = 0;
            etapa = proximaEtapa - 1;
            ProximaEtapa(proximaMissao, proximaEtapa);
        }
    }

    void ProximaEtapa(string msg, int novaEtapa)
    {
        etapa = novaEtapa;
        AtualizarMissao(msg);
        botaoInteragir.gameObject.SetActive(false);
    }

    void AtualizarMissao(string msg)
    {
        textoMissao.text = msg;
        Debug.Log(msg);
    }

    void MorteRio()
    {
        vidas = 0;

        if (textoVidas != null)
            textoVidas.text = "Vidas: " + vidas;

        MostrarAlerta("Você caiu no rio! Game Over!");
        textoMissao.text = "Você morreu! Fim de jogo!";

        if (somErro != null && audioSource != null)
            audioSource.PlayOneShot(somErro);

        botaoInteragir.gameObject.SetActive(false);
        textoInteracao.text = "";

        Invoke(nameof(CarregarGameOver), 3f);
    }

    void PerderVida()
    {
        vidas--;

        if (somErro != null && audioSource != null)
            audioSource.PlayOneShot(somErro);

        if (textoVidas != null)
            textoVidas.text = "Vidas: " + vidas;

        MostrarAlerta("ATENÇÃO! \nMissão incorreta! Você perdeu uma vida!");

        Debug.Log("Errou a missão! -1 vida (Restam " + vidas + ")");

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
