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
        public string texto;             
        public Sprite imagem;            
        public AudioClip audioNarracao;  
        public float tempoExibicao = 6f; 
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

            // Vai trocando a imagem 
            fundo.sprite = atual.imagem;
            // aqui vai troca o texto 
            textoUI.text = atual.texto;
            // Toca áudio, mas estou pensando em tirar por que nao quero falar no audio não
            if (atual.audioNarracao != null)
            {
                audioSource.Stop();
                audioSource.clip = atual.audioNarracao;
                audioSource.Play();
            }
            // Mostra com fade a tela escura que serve para transição da cena
            yield return StartCoroutine(FadeIn());
            // Aqui é para esperar o tempo de exibição para nao passar rapido 
            yield return new WaitForSeconds(atual.tempoExibicao);
            // Fade out
            yield return StartCoroutine(FadeOut());
            index++;
        }
        // Depois das cutscenes, carregar a cena configurada no Inspector que vai ir para outra cutscene , porem essa primeira vai para o menu
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
