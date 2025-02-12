using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms;

public class Aleatorizador_De_Palavras_2 : MonoBehaviour
{
    public TextMeshProUGUI Text; // Referência ao TextMeshPro
    public float TempoEmbaralhando = 2f; // Tempo de "embaralhamento"
    public float TempoEmTela = 3f; // Tempo que a palavra fica visível
    public Button[] Alfabeto; // Botões correspondentes às letras do alfabeto
    public Transform[] Slots; // Objetos vazios para posicionar os botões

    public List<string> Palavras = new List<string>
    {

        //Cores

        "Malto",
        "Rosa",
        "Vinho",
        "Lima",
        "Azul",
        "Ciano",
        "Cobre",
        "Branco",
        "Cinza",
        "Preto",

        //Verbos

        "Causei",
        "Dizer",
        "Fugir",
        "Molhar",
        "Puxem",
        "Brindamos",
        "Tocar",
        "Zombar",
        "Jogar",
        "Falou",

        //Cumprimentos

        "BomDia",
        "BoaTarde",
        "BoaNoite",
        "Ola",
        "Oi",
        "Adeus",
        "Desculpe",
        "Obrigado",
        "Licenca",
        "PorFavor"
    };

    public GameObject[] slotsErro; // Slots visuais para os erros
    public GameObject[] slotsAcerto;
    private int erros = 0; // Contador de erros
    private int indiceAtual = 0; // Índice da próxima letra da palavra
    private string palavraAtual; // Palavra que o jogador deve formar
    private int pontos = 0; // Pontos do jogador

    private Dictionary<string, List<int>> PalavrasParaBotoes = new Dictionary<string, List<int>>()
    {
        //Cores

        {"Malto", new List<int>() {13, 4, 8, 14, 17}},
        {"Vinho", new List<int>() {18, 11, 9, 10, 17}},
        {"Lima", new List<int>() {8, 11, 13, 4}},
        {"Azul", new List<int>() {4, 21, 15, 8}},
        {"Rosa", new List<int>() {23, 17, 16, 4}},
        {"Ciano", new List<int>() {0, 11, 4, 9, 17}},
        {"Branco", new List<int>() {2, 23, 4, 9, 0, 17}},
        {"Cobre", new List<int>() {0, 17, 2, 23, 3}},
        {"Cinza", new List<int> { 0, 11, 9, 21, 4} },
        {"Preto", new List<int> {25, 23, 3, 14, 17} },


        // Verbos

        {"Causei", new List<int>() {0, 4, 15, 16, 3, 11}},
        {"Dizer", new List<int>() {1, 11, 21, 3, 23}},
        {"Fugir", new List<int>() {5, 15, 7, 11, 23}}, 
        {"Molhar", new List<int>() {13, 17, 8, 10, 4, 23}},
        {"Puxem", new List<int>() {25, 15, 19, 13}}, 
        {"Brindamos", new List<int>() {2, 23, 11, 9, 1, 4, 13, 17, 16}},
        {"Tocar", new List<int>() {14, 17, 0, 4, 23}},
        {"Zombar", new List<int>() {21, 17, 13, 2, 4, 23}},
        {"Jogar", new List<int> { 12, 17, 7, 4, 23} },
        {"Falou", new List<int> {5, 4, 8, 17, 15} },

        //Cumprimentos

        {"BomDia", new List<int>() {2, 17, 13, 1, 11, 4}},
        {"BoaTarde", new List<int>() {2, 17, 4, 14, 4, 23, 1, 3}},
        {"BoaNoite", new List<int>() {2, 17, 4, 9, 17, 11, 14, 3}},
        {"Oi", new List<int>() {17, 11}},
        {"Ola", new List<int>() {17, 8, 4}},
        {"Obrigado", new List<int>() {17, 2, 23, 11, 7, 4, 1, 17}},
        {"Desculpa", new List<int>() {1, 3, 16, 0, 15, 8, 25, 4}},
        {"Licenca", new List<int>() {8, 11, 0, 3, 9, 0, 4 }},
        {"PorFavor", new List<int> { 25, 17, 23, 5, 4, 18, 17, 23} },
        {"Adeus", new List<int> {4, 1, 3, 15, 16} }
    };

    void Start()
    {
        // Inicia o efeito de embaralhamento
        StartCoroutine(EmbaralharEExibirPalavra());

        // Loop para desabilitar a visibilidade de todos os botões
        foreach (Button bt in Alfabeto)
        {
            bt.gameObject.SetActive(false);
        }

        // Esconde os slots de erro no início
        foreach (GameObject slot in slotsErro)
        {
            slot.SetActive(false);
        }

        foreach(GameObject slot2 in slotsAcerto)
        {
            slot2.SetActive(false);
        }
    }

    // Método para pegar uma palavra aleatória da lista
    public string RandomPalavra()
    {
        int randomIndex = Random.Range(0, Palavras.Count);
        return Palavras[randomIndex];
    }

    // Corrotina para embaralhar palavras antes de exibir a palavra sorteada
    private IEnumerator EmbaralharEExibirPalavra()
    {
        // Sorteia a palavra antes do embaralhamento
        palavraAtual = RandomPalavra();
        float tempoRestante = TempoEmbaralhando;

        // Desativa a interatividade de todos os botões no array
        foreach (Button botao in Alfabeto)
        {
            if (botao != null)
            {
                botao.interactable = false;
            }
        }

        // Enquanto o tempo de embaralhamento não acabar
        while (tempoRestante > 0)
        {


            // Exibe uma palavra aleatória da lista durante o efeito de embaralhamento
            Text.text = RandomPalavra();

            // Espera o próximo frame
            yield return null;

            // Reduz o tempo restante
            tempoRestante -= Time.deltaTime;
        }

        // Após o embaralhamento, exibe a palavra sorteada
        Text.text = palavraAtual;




        // Espera o tempo de exibição da palavra sorteada
        yield return new WaitForSeconds(TempoEmTela);

        // Agora que o tempo de exibição acabou, ativa os botões correspondentes e posiciona nos slots
        if (PalavrasParaBotoes.ContainsKey(palavraAtual))
        {
            // Embaralha os slots
            List<Transform> slotsEmbaralhados = new List<Transform>(Slots);
            for (int i = slotsEmbaralhados.Count - 1; i > 0; i--)
            {
                int randomIndex = Random.Range(0, i + 1);
                Transform temp = slotsEmbaralhados[i];
                slotsEmbaralhados[i] = slotsEmbaralhados[randomIndex];
                slotsEmbaralhados[randomIndex] = temp;
            }

            // Ativa os botões e posiciona nos slots
            List<int> indicesBotoes = PalavrasParaBotoes[palavraAtual];
            for (int i = 0; i < indicesBotoes.Count && i < slotsEmbaralhados.Count; i++)
            {
                int indiceBotao = indicesBotoes[i];
                AtivarButton(indiceBotao, slotsEmbaralhados[i]);
            }
        }

        // Torna o Text invisível, já que agora apenas os botões devem permanecer
        Text.gameObject.SetActive(false);

        // Ativa a interatividade de todos os botões no array
        foreach (Button botao in Alfabeto)
        {
            if (botao != null)
            {
                botao.interactable = true;
            }
        }
    }

    void AtivarButton(int indice, Transform slot)
    {
        if (indice >= 0 && indice < Alfabeto.Length)
        {
            Button botao = Alfabeto[indice];
            botao.gameObject.SetActive(true); // Ativa o botão
            botao.transform.position = slot.position; // Posiciona no slot correspondente

            // Adiciona a funcionalidade ao botão
            botao.onClick.RemoveAllListeners();
            botao.onClick.AddListener(() => ValidarLetra(botao.GetComponentInChildren<TextMeshProUGUI>().text));
        }
        else
        {
            Debug.LogWarning("Índice fora do intervalo do array");
        }
    }

    public void ValidarLetra(string letra)
    {
        // Verifica se a letra clicada está correta
        if (palavraAtual[indiceAtual].ToString().ToUpper() == letra.ToUpper())
        {
            indiceAtual++; // Incrementa o índice da palavra

            // Se o jogador completou a palavra
            if (indiceAtual >= palavraAtual.Length)
            {
                pontos++; // Incrementa os pontos
                Debug.Log("Acertou a sequência! Pontos: " + pontos);

                // Reinicia a sequência
                indiceAtual = 0;

                // Reinicia os erros e esconde os slots de erro
                ResetarErros();

                // Sorteia uma nova palavra
                StartCoroutine(EmbaralharEExibirPalavra());
            }
        }
        else
        {
            // Incrementa o contador de erros
            erros++;

            // Verifica se há slots de erro disponíveis
            if (erros <= slotsErro.Length)
            {
                // Mostra o próximo slot de erro
                slotsErro[erros - 1].SetActive(true);
            }

            // Se o jogador cometer 3 erros, finaliza o jogo
            if (erros >= 3)
            {
                Debug.Log("Fim de jogo! Você errou 3 vezes.");
                // Aqui você pode implementar a lógica de fim de jogo (ex.: tela de game over)
            }
        }
    }

    private void ResetarErros()
    {
        erros = 0; // Reseta o contador de erros
        foreach (GameObject slot in slotsErro)
        {
            slot.SetActive(false); // Esconde todos os slots de erro
        }
    }
}
