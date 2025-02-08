using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Configurador : MonoBehaviour
{
    public Text wordText;
    public Transform buttonsParent;
    public GameObject buttonPrefab;
    public Sprite[] librasImages;
    public string[] words;
    public GameObject gameOverPanel;
    public GameObject[] successIcons; // Ícones de sucesso
    public GameObject[] errorIcons;

    private string currentWord;
    private List<int> correctOrder = new List<int>();
    private List<Button> activeButtons = new List<Button>();
    private int currentErrors = 0;
    private int nextIndex = 0;
    private int successCount = 0; // Conta quantas palavras foram acertadas

    private Dictionary<string, List<int>> wordSequences = new Dictionary<string, List<int>>()
    {
        { "Lilas", new List<int> { 11, 8, 11, 0, 18 } },
        { "Rosa", new List<int> { 17, 14, 18, 0 } },
        { "Amarelo", new List<int> { 0, 12, 0, 17, 4, 11, 14 } },
        { "Verde", new List<int> { 21, 4, 17, 3, 4 } },
        { "Azul", new List<int> { 0, 25, 20, 11 } },
        { "Roxo", new List<int> { 17, 14, 23, 14 } },
        { "Laranja", new List<int> { 11, 0, 17, 0, 13, 9, 0 } },
        { "Branco", new List<int> { 1, 17, 0, 13, 2, 14 } },
        { "Marrom", new List<int> { 12, 0, 17, 17, 14, 12 } },
        { "Preto", new List<int> { 15, 17, 4, 19, 14 } },
    };

    void Start()
    {
        StartNewRound();

        foreach (GameObject icon in successIcons)
            icon.SetActive(false);
    }

    void StartNewRound()
    {
        if (words.Length == 0)
        {
            Debug.LogError("Nenhuma palavra definida!");
            return;
        }

        nextIndex = 0;
        currentErrors = 0;

        // Não desativamos os ícones de sucesso para manter o progresso
        foreach (GameObject icon in errorIcons)
            icon.SetActive(false);

       

        currentWord = words[Random.Range(0, words.Length)];
        wordText.text = currentWord.ToUpper();

        if (!wordSequences.ContainsKey(currentWord))
        {
            Debug.LogError($"Palavra '{currentWord}' não encontrada no dicionário!");
            return;
        }

        correctOrder = new List<int>(wordSequences[currentWord]);

        SetupButtons();

        StartCoroutine(ShowWordAndShuffle());
    }

    void SetupButtons()
    {
        foreach (Button btn in activeButtons)
            Destroy(btn.gameObject);

        activeButtons.Clear();

        for (int i = 0; i < correctOrder.Count; i++)
        {
            int letterIndex = correctOrder[i];

            if (letterIndex < 0 || letterIndex >= librasImages.Length)
            {
                Debug.LogError($"Índice '{letterIndex}' não tem imagem correspondente!");
                continue;
            }

            GameObject buttonObj = Instantiate(buttonPrefab, buttonsParent);
            Button button = buttonObj.GetComponent<Button>();
            button.image.sprite = librasImages[letterIndex];
            buttonObj.SetActive(false);
            activeButtons.Add(button);

            int index = i;
            button.onClick.AddListener(() => ValidateButtonPress(index));
        }
    }

    IEnumerator ShowWordAndShuffle()
    {
        wordText.gameObject.SetActive(true);
        yield return new WaitForSeconds(5);
        wordText.gameObject.SetActive(false);

        foreach (Button btn in activeButtons)
            btn.gameObject.SetActive(true);

        yield return new WaitForSeconds(5);
        ShuffleButtons();
    }

    void ShuffleButtons()
    {
        List<Transform> buttonTransforms = new List<Transform>();
        foreach (Button btn in activeButtons)
            buttonTransforms.Add(btn.transform);

        for (int i = 0; i < buttonTransforms.Count; i++)
        {
            Transform temp = buttonTransforms[i];
            int randomIndex = Random.Range(0, buttonTransforms.Count);
            buttonTransforms[i] = buttonTransforms[randomIndex];
            buttonTransforms[randomIndex] = temp;
        }

        foreach (Transform t in buttonTransforms)
            t.SetSiblingIndex(Random.Range(0, buttonTransforms.Count));
    }

    public void ValidateButtonPress(int buttonIndex)
    {
        if (nextIndex < correctOrder.Count && buttonIndex == nextIndex)
        {
            nextIndex++;

            // Somente ativa um ícone de sucesso quando TODA a sequência for concluída
            if (nextIndex == correctOrder.Count)
            {
                Debug.Log("Palavra completada!");

                if (successCount < successIcons.Length)
                {
                    successIcons[successCount].SetActive(true);
                    successCount++;

                    if(successCount == 3)
                    {
                        SceneManager.LoadScene("Tela_Selecao");
                    }
                }

                StartCoroutine(NextRoundAfterDelay());
            }
        }
        else
        {
            currentErrors++;

            if (currentErrors <= errorIcons.Length)
                errorIcons[currentErrors - 1].SetActive(true);

            if (currentErrors == 3)
            {
                Debug.Log("Game Over!");
                GameOver();
            }
        }
    }

    IEnumerator NextRoundAfterDelay()
    {
        yield return new WaitForSeconds(2); // Tempo para mostrar o sucesso antes da próxima palavra
        StartNewRound();
    }

    void GameOver()
    {
        gameOverPanel.SetActive(true);
    }
}




