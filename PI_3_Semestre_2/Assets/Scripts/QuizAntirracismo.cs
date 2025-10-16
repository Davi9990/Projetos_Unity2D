using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizAntirracismo : MonoBehaviour
{
    [Header("Referências da UI")]
    public GameObject painelQuiz;
    public Text textoPergunta, textoMensagem;
    public Button[] botoesResposta;       

    [Header("Configuração do jogo")]
    public GameObject Quiz; 
    public int pontos = 0;
    private bool quizAtivo = false;
    private int perguntaAtual = 0;
    private bool podeResponder = true;
    
    [Header("Sons")]
    public AudioClip somAcerto, somErro;
    private AudioSource audioSource;

    [System.Serializable]
    public class Pergunta
    {
        public string pergunta;
        public string[] opcoes = new string[4];
        public int indiceCorreto;
    }
    public List<Pergunta> perguntas = new List<Pergunta>();

    void Start()
    {
        painelQuiz.SetActive(false);
        audioSource = GetComponent<AudioSource>();
        CarregarPerguntas();
    }
    void CarregarPerguntas()
    {
        perguntas.Add(new Pergunta {
            pergunta = "O que o racismo tenta fazer com os povos negros?",
            opcoes = new string[] {
                "Unir e fortalecer", 
                "Apagar sua história e cultura", 
                "Ensinar igualdade", 
                "Promover justiça"
            },
            indiceCorreto = 1
        });
        perguntas.Add(new Pergunta {
            pergunta = "Qual dessas frases expressa resistência negra?",
            opcoes = new string[] {
                "Enquanto houver quem julgue pela cor, haverá quem lute por igualdade.",
                "A cor da pele define o valor de uma pessoa.",
                "O racismo é natural e sempre existirá.",
                "Devemos nos calar diante da injustiça."
            },
            indiceCorreto = 0
        });
        perguntas.Add(new Pergunta {
            pergunta = "O que representa a luta contra o racismo?",
            opcoes = new string[] {
                "A busca por privilégios.",
                "A negação da história.",
                "A defesa da igualdade e da dignidade.",
                "A divisão entre povos."
            },
            indiceCorreto = 2
        });
    }
    void MostrarPergunta()
    {
        if (perguntaAtual >= perguntas.Count)
        {
            FimDoQuiz();
            return;
        }
        Pergunta p = perguntas[perguntaAtual];
        textoPergunta.text = p.pergunta;

        for (int i = 0; i < botoesResposta.Length; i++)
        {
            botoesResposta[i].GetComponentInChildren<Text>().text = p.opcoes[i];
            int index = i;
            botoesResposta[i].onClick.RemoveAllListeners();
            botoesResposta[i].onClick.AddListener(() => Responder(index));
        }
        textoMensagem.text = "";
        podeResponder = true;
    }
    void Responder(int indice)
    {
        if (!podeResponder) return;
        podeResponder = false;

        Pergunta p = perguntas[perguntaAtual];

        if (indice == p.indiceCorreto)
        {
            pontos++;
            textoMensagem.text = MensagemAleatoriaPositiva();
            if (somAcerto) audioSource.PlayOneShot(somAcerto);
        }
        else
        {
            textoMensagem.text = "Resposta incorreta. Reflita e tente novamente!";
            if (somErro) audioSource.PlayOneShot(somErro);
        }
        StartCoroutine(ProximaPergunta());
    }
    IEnumerator ProximaPergunta()
    {
        yield return new WaitForSeconds(3f);
        perguntaAtual++;
        MostrarPergunta();
    }
    void FimDoQuiz()
    {
        textoPergunta.text = "Fim do quiz!";
        textoMensagem.text = "Um dia, a liberdade não terá cor, e esse dia nascerá da nossa luta.";
        foreach (Button b in botoesResposta) b.gameObject.SetActive(false);

        // Destruir Quiz como símbolo da luta vencida
        if (Quiz != null)
            Destroy(Quiz);

        StartCoroutine(FecharQuiz());
    }
    IEnumerator FecharQuiz()
    {
        yield return new WaitForSeconds(5f);
        painelQuiz.SetActive(false);
        quizAtivo = false;
        perguntaAtual = 0;
        foreach (Button b in botoesResposta) b.gameObject.SetActive(true);
    }
    string MensagemAleatoriaPositiva()
    {
        string[] mensagens = new string[] {
            "O racismo é a corrente invisível e nós a rompemos com dignidade.",
            "Nossa pele é herança de reis e rainhas, não de correntes.",
            "Enquanto houver quem julgue pela cor, haverá quem lute por igualdade.",
            "O racismo não nos fez menores; fez de nós gigantes na resistência.",
            "Eles tentam nos esconder na sombra, mas somos o próprio sol."
        };
        return mensagens[Random.Range(0, mensagens.Length)];
    }

    // ATIVAÇÃO DO QUIZ QUANDO O JOGADOR TOCAR NO Quiz
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Quiz") && !quizAtivo)
        {
            Quiz = other.gameObject;
            quizAtivo = true;
            painelQuiz.SetActive(true);
            MostrarPergunta();
        }
    }
}
