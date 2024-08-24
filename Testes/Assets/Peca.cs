using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peca : MonoBehaviour
{
    private PecaCerta gameManager;

    public string espacoCorretoTag; // A tag do espa�o correto para essa pe�a

    void Start()
    {
        // Encontra o GameManager na cena
        gameManager = GameObject.Find("PontosCertos").GetComponent<PecaCerta>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(espacoCorretoTag))
        {
            Debug.Log($"{gameObject.tag} foi colocada no espa�o correto.");
            gameManager.AtualizarPosicaoPeca(gameObject.tag, true);
        }
        else
        {
            Debug.LogError($"{gameObject.tag} foi colocada no espa�o errado!");
            gameManager.AtualizarPosicaoPeca(gameObject.tag, false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(espacoCorretoTag))
        {
            Debug.Log($"{gameObject.tag} foi removida do espa�o correto.");
            gameManager.AtualizarPosicaoPeca(gameObject.tag, false);
        }
    }
}
