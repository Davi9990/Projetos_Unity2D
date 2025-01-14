using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public bool Boitata;
    public bool Curupira;
    public bool Iara;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Garante que o GameManager persista entre as cenas
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Método para atualizar as variáveis do GameManager (pode ser chamado quando o boss for derrotado ou qualquer evento relevante)
    public void AtualizarPoderes(bool boitata, bool curupira, bool iara)
    {
        Boitata = boitata;
    }
}
