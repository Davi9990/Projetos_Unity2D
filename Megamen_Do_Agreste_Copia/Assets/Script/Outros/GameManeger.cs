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
            DontDestroyOnLoad(gameObject); // Garante que persista entre cenas
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
