using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Aleatorizador_De_Palavras_2 : MonoBehaviour
{
    public TextMeshProUGUI Text; // Refer�ncia ao TextMeshPro
    public float TempoEmbaralhando = 4f; // Tempo de "embaralhamento"
    public float TempoEmTela = 5f; // Tempo que a palavra fica vis�vel
    public Button[] Alfabeto; // Bot�es correspondentes �s letras do alfabeto
    public Transform[] Slots; // Objetos vazios para posicionar os bot�es

    public List<string> Palavras = new List<string>
    {
        "Verde",
        "Amarelo",
        "Lil�s",
        "Azul",
        "Rosa",
        "Roxo",
        "Laranja",
        "Branco",
        "Marrom",
        "Preto"
    };

    public GameObject[] slotsErro; // Slots visuais para os erros
    public GameObject[] slotsAcerto;
    private int erros = 0; // Contador de erros
    private int indiceAtual = 0; // �ndice da pr�xima letra da palavra
    private string palavraAtual; // Palavra que o jogador deve formar
    private int pontos = 0; // Pontos do jogador

    private Dictionary<string, List<int>> PalavrasParaBotoes = new Dictionary<string, List<int>>()
    {
        {"Verde", new List<int>() {18, 3, 23, 1, 26}},
        {"Amarelo", new List<int>() {4, 13, 27, 23, 3, 8, 17}},
        {"Lilais", new List<int>() {8, 11, 28, 4, 29, 16}},
        {"Azul", new List<int>() {4, 21, 15, 8}},
        {"Rosa", new List<int>() {23, 17, 16, 4}},
        {"Laranja", new List<int>() {8, 4, 23, 27, 9, 12, 30}},
        {"Branco", new List<int>() {2, 23, 4, 9, 0, 17}},
        {"Marrom", new List<int>() {13, 4, 23, 31, 17, 32}},
        {"Preto", new List<int> {25, 23, 3, 14, 17} }
    };

    void Start()
    {
        // Inicia o efeito de embaralhamento
        StartCoroutine(EmbaralharEExibirPalavra());

        // Loop para desabilitar a visibilidade de todos os bot�es
        foreach (Button bt in Alfabeto)
        {
            bt.gameObject.SetActive(false);
        }

        // Esconde os slots de erro no in�cio
        foreach (GameObject slot in slotsErro)
        {
            slot.SetActive(false);
        }

        foreach(GameObject slot2 in slotsAcerto)
        {
            slot2.SetActive(false);
        }
    }

    // M�todo para pegar uma palavra aleat�ria da lista
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

        // Enquanto o tempo de embaralhamento n�o acabar
        while (tempoRestante > 0)
        {
            // Exibe uma palavra aleat�ria da lista durante o efeito de embaralhamento
            Text.text = RandomPalavra();

            // Espera o pr�ximo frame
            yield return null;

            // Reduz o tempo restante
            tempoRestante -= Time.deltaTime;
        }

        // Ap�s o embaralhamento, exibe a palavra sorteada
        Text.text = palavraAtual;

        // Espera o tempo de exibi��o da palavra sorteada
        yield return new WaitForSeconds(TempoEmTela);

        // Agora que o tempo de exibi��o acabou, ativa os bot�es correspondentes e posiciona nos slots
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

            // Ativa os bot�es e posiciona nos slots
            List<int> indicesBotoes = PalavrasParaBotoes[palavraAtual];
            for (int i = 0; i < indicesBotoes.Count && i < slotsEmbaralhados.Count; i++)
            {
                int indiceBotao = indicesBotoes[i];
                AtivarButton(indiceBotao, slotsEmbaralhados[i]);
            }
        }

        // Torna o Text invis�vel, j� que agora apenas os bot�es devem permanecer
        Text.gameObject.SetActive(false);
    }

    void AtivarButton(int indice, Transform slot)
    {
        if (indice >= 0 && indice < Alfabeto.Length)
        {
            Button botao = Alfabeto[indice];
            botao.gameObject.SetActive(true); // Ativa o bot�o
            botao.transform.position = slot.position; // Posiciona no slot correspondente

            // Adiciona a funcionalidade ao bot�o
            botao.onClick.RemoveAllListeners();
            botao.onClick.AddListener(() => ValidarLetra(botao.GetComponentInChildren<TextMeshProUGUI>().text));
        }
        else
        {
            Debug.LogWarning("�ndice fora do intervalo do array");
        }
    }

    public void ValidarLetra(string letra)
    {
        // Verifica se a letra clicada est� correta
        if (palavraAtual[indiceAtual].ToString().ToUpper() == letra.ToUpper())
        {
            indiceAtual++; // Incrementa o �ndice da palavra

            // Se o jogador completou a palavra
            if (indiceAtual >= palavraAtual.Length)
            {
                pontos++; // Incrementa os pontos
                Debug.Log("Acertou a sequ�ncia! Pontos: " + pontos);

                // Reinicia a sequ�ncia
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

            // Verifica se h� slots de erro dispon�veis
            if (erros <= slotsErro.Length)
            {
                // Mostra o pr�ximo slot de erro
                slotsErro[erros - 1].SetActive(true);
            }

            // Se o jogador cometer 3 erros, finaliza o jogo
            if (erros >= 3)
            {
                Debug.Log("Fim de jogo! Voc� errou 3 vezes.");
                // Aqui voc� pode implementar a l�gica de fim de jogo (ex.: tela de game over)
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
