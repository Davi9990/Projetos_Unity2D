using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CutSceneImagem : MonoBehaviour
{
    [System.Serializable]
    public class Cutscene
    {
        [TextArea(3, 5)]
        public string texto;             // Texto narrado
        public Sprite imagem;            // Imagem de fundo
        public AudioClip audioNarracao;  // Narração/efeito sonoro
        public float tempoExibicao = 6f; // Tempo de exibição
    }

    [Header("Configuração das Cutscenes")]
    public Cutscene[] cutscenes;  
    private int index = 0;

    [Header("Configuração de Cenas")]
    [Tooltip("Cena que será carregada depois da cutscene (pode ser Menu, Fase1, outra cutscene etc).")]
    public string proximaCena = "Menu";

    [Header("Referências UI")]
    public Image fundo;
    public Text textoUI;
    public AudioSource audioSource;
    public CanvasGroup fadePanel;

    void Start()
    {
        StartCoroutine(ExecutarCutscenes());
    }

    IEnumerator ExecutarCutscenes()
    {
        while (index < cutscenes.Length)
        {
            Cutscene atual = cutscenes[index];

            // Troca a imagem
            fundo.sprite = atual.imagem;

            // Troca o texto
            textoUI.text = atual.texto;

            // Toca áudio (se existir)
            if (atual.audioNarracao != null)
            {
                audioSource.Stop();
                audioSource.clip = atual.audioNarracao;
                audioSource.Play();
            }

            // Mostra com fade
            yield return StartCoroutine(FadeIn());

            // Espera o tempo de exibição
            yield return new WaitForSeconds(atual.tempoExibicao);

            // Fade out
            yield return StartCoroutine(FadeOut());

            index++;
        }

        // Depois das cutscenes, carregar a cena configurada no Inspector
        if (!string.IsNullOrEmpty(proximaCena))
            SceneManager.LoadScene(proximaCena);
    }

    IEnumerator FadeIn()
    {
        fadePanel.alpha = 1;
        while (fadePanel.alpha > 0)
        {
            fadePanel.alpha -= Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator FadeOut()
    {
        fadePanel.alpha = 0;
        while (fadePanel.alpha < 1)
        {
            fadePanel.alpha += Time.deltaTime;
            yield return null;
        }
    }
}
