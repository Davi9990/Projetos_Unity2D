using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuizAntirracismo : MonoBehaviour
{
    [Header("UI")]
    public GameObject painelQuiz, Quiz;               // Painel principal do quiz e referência do objeto de quiz
    public Text textoPergunta, textoMensagem, textoInteracaoQuiz; // Textos da interface
    public Button[] botoesResposta;                   // Botões das opções de resposta
    public Button botaoInteragir;                     // Botão para abrir o quiz
    public int pontos = 0;                            // Pontuação do jogador
    private bool quizAtivo = false, podeResponder = true, jogadorPerto = false;
    private int perguntaAtual = 0;                    // Índice da pergunta atual

    [Header("Animações")]
    public float duracaoAnimacaoPainel = 0.3f;        // Duração da animação de abrir/fechar o painel

    [Header("Sons")]
    public AudioClip somAcerto, somErro;              // Sons de feedback (acerto e erro)
    private AudioSource audioSource;                  // Componente responsável por tocar os sons

    // Classe interna para armazenar perguntas e respostas
    [System.Serializable]
    public class Pergunta
    {
        public string pergunta;                       // Texto da pergunta
        public string[] opcoes = new string[4];       // As 4 opções possíveis
        public int indiceCorreto;                     // Índice da resposta correta
    }
    public List<Pergunta> perguntas = new List<Pergunta>(); // Lista de perguntas do quiz

    void Start()
    {
        painelQuiz.SetActive(false);                  // Painel começa invisível
        botaoInteragir.gameObject.SetActive(false);   // Botão de interação escondido até o jogador se aproximar
        textoInteracaoQuiz.gameObject.SetActive(false);
        audioSource = GetComponent<AudioSource>();    // Obtém o componente de áudio
        CarregarPerguntas();                          // Cria as perguntas do quiz
        botaoInteragir.onClick.AddListener(AbrirQuiz);// Define a ação do botão de interação
    }

    // Faz o painel crescer suavemente até o tamanho normal
    IEnumerator AnimarAberturaPainel()
    {
        painelQuiz.transform.localScale = Vector3.zero; // Escala inicial = invisível
        float tempo = 0;

        while (tempo < duracaoAnimacaoPainel)           // Enquanto o tempo não acabar...
        {
            tempo += Time.deltaTime;
            float t = Mathf.Clamp01(tempo / duracaoAnimacaoPainel);
            painelQuiz.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, t); // Interpola de 0 → 1
            yield return null; // Espera o próximo frame
        }

        painelQuiz.transform.localScale = Vector3.one;  // Garante que o painel termine no tamanho normal
        MostrarPergunta();                              // Mostra a primeira pergunta
    }

    // Faz o painel encolher suavemente até sumir
    public IEnumerator AnimarFechamentoPainel()
    {
        float tempo = 0;
        Vector3 escalaInicial = painelQuiz.transform.localScale;

        while (tempo < duracaoAnimacaoPainel)
        {
            tempo += Time.deltaTime;
            float t = Mathf.Clamp01(tempo / duracaoAnimacaoPainel);
            painelQuiz.transform.localScale = Vector3.Lerp(escalaInicial, Vector3.zero, t); // De 1 → 0
            yield return null;
        }

        painelQuiz.transform.localScale = Vector3.zero; // Garante que o painel chegue a 0
        ResetarQuiz();                                  // Reinicia o quiz
        painelQuiz.SetActive(false);                    // Desativa o painel
    }
    // Cria as perguntas e respostas
    void CarregarPerguntas()
    {
        perguntas.Add(new Pergunta
        {
            pergunta = "O que levou os escravizados do Engenho de Santana a se revoltarem em 1789?",
            opcoes = new string[]
            {
                "A) Castigos e jornadas desumanas nas plantações.",
                "B) A chegada de novos senhores bondosos.",
                "C) A liberdade já ter sido concedida por lei.",
                "D) A falta de trabalho no engenho."                
            },
            indiceCorreto = 0
        });
        perguntas.Add(new Pergunta
        {
            pergunta = "Como o racismo se manifestava na sociedade baiana do século XVIII?",
            opcoes = new string[]
            {       
                "A) Por meio da igualdade entre todos.",                 
                "B) Por meio da exclusão dos negros da educação, poder e liberdade.",                
                "C) Através do apoio dos senhores aos escravizados.",
                "D) Não havia racismo no período colonial."
            },
            indiceCorreto = 1
        });
        perguntas.Add(new Pergunta
        {
            pergunta = "Qual foi uma das principais formas de resistência dos escravizados?",
            opcoes = new string[]
            {
                "A) Trabalhar mais para agradar os senhores.",
                "B) Permanecer calados diante das injustiças.",
                "C) Fugir e formar quilombos.",
                "D) Pedir ajuda às autoridades coloniais."                                            
            },
            indiceCorreto = 2
        });
        perguntas.Add(new Pergunta
        {
            pergunta = "Quem eram os líderes da revolta no Engenho de Santana?",
            opcoes = new string[]
            {
                "A) Comerciantes do porto.",
                "B) Capitães portugueses.",
                "C) Padres europeus.",
                "D) Escravizados como Gregório Luís, Ventura, Cosme e Zeferina que representa o papel fundamental das mulheres negras na resistência."
                      
            },
            indiceCorreto = 3
        });
    }
    // Exibe a pergunta atual na tela
    void MostrarPergunta()
    {
        if (perguntaAtual >= perguntas.Count)           // Se acabou as perguntas
        {
            FimDoQuiz();
            return;
        }

        Pergunta p = perguntas[perguntaAtual];
        textoPergunta.text = p.pergunta;                // Mostra a pergunta

        // Atualiza as opções nos botões
        for (int i = 0; i < botoesResposta.Length; i++)
        {
            botoesResposta[i].GetComponentInChildren<Text>().text = p.opcoes[i];
            int index = i;
            botoesResposta[i].onClick.RemoveAllListeners();
            botoesResposta[i].onClick.AddListener(() => Responder(index)); // Define ação do botão
        }

        textoMensagem.text = "";
        podeResponder = true;
    }
    // Verifica se o jogador respondeu certo ou errado
    void Responder(int indice)
    {
        if (!podeResponder) return; // Impede múltiplos cliques
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

        StartCoroutine(ProximaPergunta()); // Avança após 3 segundos
    }
    // Espera alguns segundos e vai para a próxima pergunta
    IEnumerator ProximaPergunta()
    {
        yield return new WaitForSeconds(3f);
        perguntaAtual++;
        MostrarPergunta();
    }
    // Quando o quiz termina
    void FimDoQuiz()
    {
        textoPergunta.text = "Fim do quiz!";

        // Esconde os botões de resposta
        foreach (Button b in botoesResposta)
            b.gameObject.SetActive(false);

        // Calcula o percentual de acertos
        float percentualAcertos = ((float)pontos / perguntas.Count) * 100f;
        int percentualArredondado = Mathf.RoundToInt(percentualAcertos);

        // Mostra a mensagem de resultado
        textoMensagem.text = "Você acertou " + percentualArredondado + "% das perguntas.\nA Revolta no Engenho de Santana foi uma das primeiras expressões da luta por liberdade no Brasil.\nMesmo enfrentando racismo, violência e opressão, os escravizados da Bahia deixaram um legado de coragem e resistência.";

        // Verifica se o jogador passou
        if (percentualAcertos >= 70f)
        {
            textoMensagem.text += "\nParabéns! Você avançará para a próxima fase!";
            StartCoroutine(IrParaFase2());
        }
        else
        {
            textoMensagem.text += "\nTente novamente para alcançar 70% ou mais!";
            StartCoroutine(FecharQuiz());
        }
    }
    // Avança para a próxima cena
    IEnumerator IrParaFase2()
    {
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene("Cutscene 2");
    }
    // Fecha o quiz após 4 segundos
    IEnumerator FecharQuiz()
    {
        yield return new WaitForSeconds(4f);
        StartCoroutine(AnimarFechamentoPainel()); // Encolhe o painel antes de fechar
    }
    // Reseta todas as variáveis do quiz
    public void ResetarQuiz()
    {
        painelQuiz.SetActive(false);
        quizAtivo = false;
        perguntaAtual = 0;
        pontos = 0;

        foreach (Button b in botoesResposta)
            b.gameObject.SetActive(true);

        // Se o jogador ainda estiver perto, mostra o botão de interação novamente
        if (jogadorPerto)
        {
            botaoInteragir.gameObject.SetActive(true);
            textoInteracaoQuiz.gameObject.SetActive(true);
            textoInteracaoQuiz.text = "Pressione para jogar o quiz novamente";
        }
    }
    // Retorna uma mensagem positiva aleatória
    string MensagemAleatoriaPositiva()
    {
        string[] mensagens =
        {
            "A revolta foi uma resposta à violência e exploração dos senhores de engenho. Os escravizados lutavam por liberdade e dignidade.",
            "O racismo estruturava toda a sociedade colonial. Negros eram vistos como propriedade, não como pessoas, uma das maiores injustiças da história.",
            "A fuga e a formação de quilombos eram atos de coragem e resistência contra um sistema brutal e racista.",
            "Os líderes eram homens e mulheres escravizados que arriscaram a vida por liberdade, os verdadeiros símbolos de resistência.",
            "Mesmo com a repressão, a revolta inspirou outras lutas e mostrou que a liberdade era um direito inegociável."
        };
        return mensagens[UnityEngine.Random.Range(0, mensagens.Length)];
    }
    // Detecta quando o jogador se aproxima
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Berenice"))
        {
            jogadorPerto = true;
            Quiz = other.gameObject;

            if (!quizAtivo)
            {
                botaoInteragir.gameObject.SetActive(true);
                textoInteracaoQuiz.gameObject.SetActive(true);
                textoInteracaoQuiz.text = "Pressione para interagir com o quiz";
            }
        }
    }
    // Detecta quando o jogador sai da área
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Berenice"))
        {
            jogadorPerto = false;
            botaoInteragir.gameObject.SetActive(false);
            textoInteracaoQuiz.gameObject.SetActive(false);
        }
    }
    // Abre o quiz com animação de expansão
    void AbrirQuiz()
    {
        if (jogadorPerto && !quizAtivo)
        {
            quizAtivo = true;
            painelQuiz.SetActive(true);
            StartCoroutine(AnimarAberturaPainel()); // Faz o painel crescer
            botaoInteragir.gameObject.SetActive(false);
            textoInteracaoQuiz.gameObject.SetActive(false);
        }
    }
}