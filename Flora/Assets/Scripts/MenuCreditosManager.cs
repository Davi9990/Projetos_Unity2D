using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuCreditosManager : MonoBehaviour
{
    public Button botaoVoltar;

    void Start()
    {
        botaoVoltar.onClick.AddListener(VoltarParaMenu);
    }

    void VoltarParaMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
