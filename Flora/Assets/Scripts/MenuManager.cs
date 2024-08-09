using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public Button botaoJogar;
    public Button botaoFases;
    public Button botaoCreditos;
    public Button botaoSair;
    public Button botaoFase1;
    public Button botaoFase2;
    public Button botaoFase3;
    public Button botaoFaseBoss;


    public GameObject panelFases;
    public AudioSource musicaDeFundo;
    public Slider sliderVolume;

    void Start()
    {
        botaoJogar.onClick.AddListener(() => CarregarCena("Fase1"));
        botaoFaseBoss.onClick.AddListener(() => CarregarCena("FaseBoss"));

        //botaoFases.onClick.AddListener(TogglePanelFases);
        //botaoCreditos.onClick.AddListener(() => CarregarCena("Creditos"));
        botaoSair.onClick.AddListener(SairDoJogo);
        //botaoFase1.onClick.AddListener(() => CarregarCena("Fase1"));
        //botaoFase2.onClick.AddListener(() => CarregarCena("Fase2"));
        //botaoFase3.onClick.AddListener(() => CarregarCena("Fase3"));

        // Inicializar o painel
        //panelFases.SetActive(false);

        // Configurar o slider de volume
        if (sliderVolume != null && musicaDeFundo != null)
        {
            sliderVolume.value = musicaDeFundo.volume;
            sliderVolume.onValueChanged.AddListener(AjustarVolume);
        }

        // Tocar música de fundo
        if (musicaDeFundo != null && !musicaDeFundo.isPlaying)
        {
            musicaDeFundo.Play();
        }
    }

    //void TogglePanelFases()
    //{
    //    // Alternar a visibilidade do painel de fases
    //    panelFases.SetActive(!panelFases.activeSelf);
    //}

    void CarregarCena(string nomeDaCena)
    {
        // Carregar a cena diretamente
        SceneManager.LoadScene(nomeDaCena);
    }

    void SairDoJogo()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    void AjustarVolume(float volume)
    {
        if (musicaDeFundo != null)
        {
            musicaDeFundo.volume = volume;
        }
    }
}
