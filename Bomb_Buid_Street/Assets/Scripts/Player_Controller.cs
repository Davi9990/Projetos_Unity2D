using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Controller : MonoBehaviour
{
    private static Player_Controller instance;

    // Dicionário para associar cenas a tags de spawn
    private Dictionary<string, string> spawnPorCena = new Dictionary<string, string>
    {
        { "Tela1", "EndPoint4" },  // Associa "Tela1" ao ponto "EndPoint4"
        { "Tela2", "EndPoint5" },  // Exemplo: Associa "Tela2" ao ponto "EndPoint5"
        { "Tela3", "EndPoint6" }   // Adicione mais cenas aqui conforme necessário
    };

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Mantém o GameObject entre cenas
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject); // Evita múltiplas instâncias
        }
    }

    private void OnSceneLoaded(Scene cena, LoadSceneMode loadSceneMode)
    {
        // Tenta localizar o ponto de spawn específico para a cena
        StartCoroutine(PosicionarNoPontoDeSpawn(cena.name));
    }

    private IEnumerator PosicionarNoPontoDeSpawn(string nomeCenaAtual)
    {
        yield return null; // Espera 1 frame para garantir que os objetos sejam carregados

        // Verifica se a cena atual tem um ponto de spawn definido no dicionário
        if (spawnPorCena.ContainsKey(nomeCenaAtual))
        {
            string tagPontoDeSpawn = spawnPorCena[nomeCenaAtual];

            // Procura o ponto de spawn pela tag definida
            GameObject pontoDeSpawn = GameObject.FindGameObjectWithTag(tagPontoDeSpawn);

            if (pontoDeSpawn != null)
            {
                // Move o GameObject vazio (pai) para o ponto de spawn
                this.transform.position = pontoDeSpawn.transform.position;

                // Garantir que o player (filho) siga a posição do pai
                Transform player = transform.GetChild(0); // Pega o primeiro filho (Player)))
                if (player != null)
                {
                    player.position = pontoDeSpawn.transform.position; // Move o player para o ponto
                }

                // Debug para confirmar o posicionamento
                Debug.Log($"Player posicionado no ponto {tagPontoDeSpawn} na cena {nomeCenaAtual}");
            }
            else
            {
                Debug.LogWarning($"Ponto de spawn com a tag {tagPontoDeSpawn} não encontrado na cena {nomeCenaAtual}");
            }
        }
        else
        {
            Debug.LogWarning($"Nenhum ponto de spawn definido para a cena {nomeCenaAtual}");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verifica se a colisão foi com um objeto de transição (ex: EndPoint2, EndPoint3)
        if (collision.gameObject.CompareTag("EndPoint2"))
        {
            // Muda para o ponto de spawn relacionado a "EndPoint3"
            MudarPontoDeSpawn("EndPoint3");
        }
        else if (collision.gameObject.CompareTag("EndPoint6"))
        {
            // Se colidir com o "EndPoint3", muda para outro ponto (ex: EndPoint4)
            MudarPontoDeSpawn("EndPoint4");
        }
        // Adicione outros pontos de colisão conforme necessário
    }

    private void MudarPontoDeSpawn(string tagPonto)
    {
        // Procura pelo ponto de spawn na próxima cena
        GameObject NovoPonto = GameObject.FindGameObjectWithTag(tagPonto);
        if (NovoPonto != null && NovoPonto.activeInHierarchy)
        {
            // Muda a posição do Player para o novo ponto de spawn
            this.transform.position = NovoPonto.transform.position;

            // Debug para confirmar o reposicionamento
            Debug.Log($"Player movido para {NovoPonto.transform.position} usando a tag {tagPonto}");
        }
        else
        {
            Debug.LogWarning($"Ponto de spawn {tagPonto} não encontrado ou está inativo");
        }
    }
}
