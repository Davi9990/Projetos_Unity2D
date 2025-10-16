using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuIniciar : MonoBehaviour
{
    [Header("Painéis do Menu")] //Aqui vai mostar os paineis no menu iniciar sme precisar ir 
    //                            para outra cena, voces veirifam ai que ainda falta eu 
    //                            implementar o retorno para o menu normal. Ass. Bruno Barreiros
    public GameObject painelSair, painelCreditos, painelConfiguracao;
    public Slider volumeSlider;

    void Start()
    {
        if (volumeSlider != null)
        {
            volumeSlider.value = AudioListener.volume;
            volumeSlider.onValueChanged.AddListener(SetVolume);
        }
    }
    public void Menu()
    {
        SceneManager.LoadScene("Cutscene 1");
        print("Menu");
    }
    public void IniciarJogo()
    {
        SceneManager.LoadScene("Cutscene 1");
        print("Iniciar");
    }
    public void Creditos()
    {
        FecharTodosPaineis();
        painelCreditos.SetActive(true);
        print("Creditos");
    }
    public void Configuracao()
    {
        FecharTodosPaineis();
        painelConfiguracao.SetActive(true);
        print("Configuração");
    }
    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
    }
    // Aqui vai abrir o painel de confirmacao de saida
    public void Sair()
    {
        FecharTodosPaineis();
        painelSair.SetActive(true);
        print("Deseja sair?");
    }
    // AQui tem o Botao "Sim" do painel
    public void ConfirmarSair()
    {
        Application.Quit();
        print("Saiu do jogo");
    }
    // Aqui tem o Botao "Nao" do painel pra cancelar a saida
    public void CancelarSair()
    {
        painelSair.SetActive(false);
        print("Cancelou saída");
    }
    public void FecharTodosPaineis()
    {
        painelSair.SetActive(false);
        painelCreditos.SetActive(false);
        painelConfiguracao.SetActive(false);
    }
}
