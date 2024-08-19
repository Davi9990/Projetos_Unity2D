using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPauseManager : MonoBehaviour
{
    public GameObject panelPause; // O painel de pausa
    public Button botaoMenu; // O bot�o que retorna ao menu principal

    private bool jogoPausado = false; // Estado do jogo pausado

    void Start()
    {
        // Configurar o listener para o bot�o de menu
        botaoMenu.onClick.AddListener(() => RetornarAoMenu());

        // Garantir que o painel de pausa esteja inicialmente desativado
        panelPause.SetActive(false);
    }

    void Update()
    {
        // Verifica se a tecla ESC foi pressionada
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (jogoPausado)
            {
                // Se o jogo j� estiver pausado, despausar
                ContinuarJogo();
            }
            else
            {
                // Se o jogo n�o estiver pausado, pausar
                PausarJogo();
            }
        }
    }

    void PausarJogo()
    {
        panelPause.SetActive(true); // Mostrar o painel de pausa
        Time.timeScale = 0f; // Pausar o tempo no jogo
        jogoPausado = true;
    }

    void ContinuarJogo()
    {
        panelPause.SetActive(false); // Esconder o painel de pausa
        Time.timeScale = 1f; // Continuar o tempo no jogo
        jogoPausado = false;
    }

    void RetornarAoMenu()
    {
        Time.timeScale = 1f; // Garantir que o tempo seja retomado antes de carregar o menu
        SceneManager.LoadScene("Menu"); // Carregar a cena do menu principal
    }
}
