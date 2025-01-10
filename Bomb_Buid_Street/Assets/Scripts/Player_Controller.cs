using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Controller : MonoBehaviour
{
    private static Player_Controller instance;

    private GameObject pontoDeSpawnAtual;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnSceneLoaded(Scene cena, LoadSceneMode loadSceneMode)
    {
        StartCoroutine(PosicionarNoPontoDeSpawn());
    }

    private IEnumerator PosicionarNoPontoDeSpawn()
    {
        yield return null;

        // Se o ponto de spawn não foi setado ainda, tentamos encontrar o primeiro ponto de spawn na cena
        if (pontoDeSpawnAtual == null)
        {
            pontoDeSpawnAtual = GameObject.FindGameObjectWithTag("EndPoint8"); // Ou qualquer outro ponto inicial
        }

        if (pontoDeSpawnAtual != null)
        {
            // Move o próprio GameObject
            transform.position = pontoDeSpawnAtual.transform.position;

            // Agora, percorre todos os filhos e os move também
            foreach (Transform child in transform)
            {
                child.position = pontoDeSpawnAtual.transform.position;
            }

            Debug.Log($"Player posicionado no ponto de spawn com a tag {pontoDeSpawnAtual.tag}");
        }
        else
        {
            Debug.LogWarning("Nenhum ponto de spawn encontrado na cena atual.");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Se o player colidir com um ponto de spawn, muda o ponto de spawn atual
        if (collision.gameObject.CompareTag("EndPoint2"))
        {
            MudarPontoDeSpawn("EndPoint3");
        }
        else if (collision.gameObject.CompareTag("EndPoint6"))
        {
            MudarPontoDeSpawn("EndPoint4");
        }
        else if (collision.gameObject.CompareTag("EndPoint3"))
        {
            MudarPontoDeSpawn("EndPoint2");
        }
    }

    private void MudarPontoDeSpawn(string tagPonto)
    {
        pontoDeSpawnAtual = GameObject.FindGameObjectWithTag(tagPonto);

        if (pontoDeSpawnAtual != null)
        {
            // Move o próprio GameObject
            transform.position = pontoDeSpawnAtual.transform.position;

            // Agora, percorre todos os filhos e os move também
            foreach (Transform child in transform)
            {
                child.position = pontoDeSpawnAtual.transform.position;
            }

            Debug.Log($"Player movido para {pontoDeSpawnAtual.transform.position} usando a tag {tagPonto}");
        }
        else
        {
            Debug.LogWarning($"Ponto de spawn {tagPonto} não encontrado ou está inativo");
        }
    }
}
