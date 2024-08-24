using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peca : MonoBehaviour
{
    private PecaCerta gameManager;

    public string espacoCorretoTag; // A tag do espaço correto para essa peça

    void Start()
    {
        // Encontra o GameManager na cena
        gameManager = GameObject.Find("PontosCertos").GetComponent<PecaCerta>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(espacoCorretoTag))
        {
            Debug.Log($"{gameObject.tag} foi colocada no espaço correto.");
            gameManager.AtualizarPosicaoPeca(gameObject.tag, true);
        }
        else
        {
            Debug.LogError($"{gameObject.tag} foi colocada no espaço errado!");
            gameManager.AtualizarPosicaoPeca(gameObject.tag, false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(espacoCorretoTag))
        {
            Debug.Log($"{gameObject.tag} foi removida do espaço correto.");
            gameManager.AtualizarPosicaoPeca(gameObject.tag, false);
        }
    }
}
