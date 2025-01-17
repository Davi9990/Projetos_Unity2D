using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Configurador : MonoBehaviour
{
    public Text wordText; // Texto que exibe a palavra em português
    public Transform buttonsParent; // Pai dos botões
    public GameObject buttonPrefab; // Prefab do botão
    public Sprite[] librasImages; // Imagens em Libras (ordem A-Z)
    public string[] words; // Lista de palavras em português
    public GameObject gameOverPanel; // Painel de Game Over
    public GameObject[] successIcons; // Ícones de acertos
    public GameObject[] errorIcons; // Ícones de erros

    private string currentWord;
    private List<int> correctOrder = new List<int>(); // Ordem correta das letras
    private List<Button> activeButtons = new List<Button>(); // Botões ativos
    private int currentSuccesses = 0; // Contador de acertos
    private int currentErrors = 0; // Contador de erros
    private int nextIndex = 0; // Índice atual da sequência correta

    void Start()
    {
        StartNewRound();
    }

    void StartNewRound()
    {
        if (words.Length == 0)
        {
            Debug.LogError("Nenhuma palavra definida!");
            return;
        }

        // Reseta os contadores para a nova rodada
        nextIndex = 0;

        // Desativa os ícones de sucesso e erro
        foreach (GameObject icon in successIcons)
        {
            icon.SetActive(false);
        }

        foreach (GameObject icon in errorIcons)
        {
            icon.SetActive(false);
        }

        // Seleciona uma palavra aleatória
        currentWord = words[Random.Range(0, words.Length)];
        wordText.text = currentWord.ToUpper();

        // Configurar botões para a palavra
        SetupButtons();

        // Mostrar palavra por 5 segundos
        StartCoroutine(ShowWordAndShuffle());
    }

    void SetupButtons()
    {
        // Limpar botões antigos
        foreach (Button btn in activeButtons)
        {
            Destroy(btn.gameObject);
        }
        activeButtons.Clear();
        correctOrder.Clear();

        // Criar botões para cada letra da palavra
        for (int i = 0; i < currentWord.Length; i++)
        {
            char letter = currentWord[i];
            int letterIndex = char.ToUpper(letter) - 'A';

            if (letterIndex < 0 || letterIndex >= librasImages.Length)
            {
                Debug.LogError($"Letra '{letter}' não tem imagem correspondente!");
                continue;
            }

            // Criar botão e configurar
            GameObject buttonObj = Instantiate(buttonPrefab, buttonsParent);
            Button button = buttonObj.GetComponent<Button>();
            button.image.sprite = librasImages[letterIndex];
            buttonObj.SetActive(false); // Botões começam desativados
            activeButtons.Add(button);
            correctOrder.Add(letterIndex);

            // Adiciona o evento ao botão
            int index = i; // Para evitar problemas de referência no loop
            button.onClick.AddListener(() => ValidateButtonPress(index));
        }
    }

    IEnumerator ShowWordAndShuffle()
    {
        // Mostrar palavra em português
        wordText.gameObject.SetActive(true);
        yield return new WaitForSeconds(5);

        // Esconder palavra em português
        wordText.gameObject.SetActive(false);

        // Ativar botões com letras em Libras
        foreach (Button btn in activeButtons)
        {
            btn.gameObject.SetActive(true);
        }

        // Mostrar botões na ordem correta por 5 segundos
        yield return new WaitForSeconds(5);

        // Embaralhar botões
        ShuffleButtons();
    }

    void ShuffleButtons()
    {
        List<Transform> buttonTransforms = new List<Transform>();
        foreach (Button btn in activeButtons)
        {
            buttonTransforms.Add(btn.transform);
        }

        // Embaralhar posições
        for (int i = 0; i < buttonTransforms.Count; i++)
        {
            Transform temp = buttonTransforms[i];
            int randomIndex = Random.Range(0, buttonTransforms.Count);
            buttonTransforms[i] = buttonTransforms[randomIndex];
            buttonTransforms[randomIndex] = temp;
        }

        // Atualizar posições na hierarquia
        foreach (Transform t in buttonTransforms)
        {
            t.SetSiblingIndex(Random.Range(0, buttonTransforms.Count));
        }
    }

    public void ValidateButtonPress(int buttonIndex)
    {
        if (nextIndex < correctOrder.Count && correctOrder[nextIndex] == buttonIndex)
        {
            // Jogador acertou o botão correto na sequência
            nextIndex++;

            // Mostra um ícone de sucesso
            if (nextIndex <= successIcons.Length)
            {
                successIcons[nextIndex - 1].SetActive(true);
            }

            // Verifica se completou a palavra
            if (nextIndex == correctOrder.Count)
            {
                currentSuccesses++;
                if (currentSuccesses == 3)
                {
                    Debug.Log("Você venceu!");
                    // Aqui você pode adicionar a lógica para finalizar o jogo
                    return;
                }

                Debug.Log("Palavra completada!");
                StartNewRound();
            }
        }
        else
        {
            // Jogador errou
            currentErrors++;

            // Mostra um ícone de erro
            if (currentErrors <= errorIcons.Length)
            {
                errorIcons[currentErrors - 1].SetActive(true);
            }

            // Verifica se o jogador perdeu
            if (currentErrors == 3)
            {
                Debug.Log("Game Over!");
                GameOver();
            }
        }
    }

    void GameOver()
    {
        gameOverPanel.SetActive(true);
        Debug.Log("Game Over!");
    }
}




