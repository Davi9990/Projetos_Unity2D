using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ovo : MonoBehaviour
{
    private EspacoCerto gameManeger;

    public string espacoCorretoTag;


    // Start is called before the first frame update
    void Start()
    {
        gameManeger =  GameObject.Find("GameManeger").GetComponent<EspacoCerto>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(espacoCorretoTag))
        {
            Debug.Log($"{gameObject.tag} foi colocada no espaço correto");
            gameManeger.AtualizarPosicaoOvo(gameObject.tag, true);
        }
        else 
        {
            Debug.Log($"{gameObject.tag} foi colocada no espaço errado!");
            gameManeger.AtualizarPosicaoOvo(gameObject.tag, false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(espacoCorretoTag))
        {
            Debug.Log($"{gameObject.tag} foi removida do espaço correto");
            gameManeger.AtualizarPosicaoOvo(gameObject.tag, false);
        }
    }
}
