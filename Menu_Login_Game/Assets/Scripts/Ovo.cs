using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ovo : MonoBehaviour
{
    private EspacoCerto gameManeger;
    private DinossaurosCertos gameManeger2;

    public string espacoCorretoTag;


    // Start is called before the first frame update
    void Start()
    {
        gameManeger =  GameObject.Find("GameManeger").GetComponent<EspacoCerto>();
        gameManeger2 =  GameObject.Find("GameManeger").GetComponent<DinossaurosCertos>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(espacoCorretoTag))
        {
            Debug.Log($"{gameObject.tag} foi colocada no espa�o correto");
            gameManeger.AtualizarPosicaoOvo(gameObject.tag, true);
            gameManeger2.AtualizarPosicaoOvo(gameObject.tag, true);
        }
        else 
        {
            Debug.Log($"{gameObject.tag} foi colocada no espa�o errado!");
            gameManeger.AtualizarPosicaoOvo(gameObject.tag, false);
            gameManeger2.AtualizarPosicaoOvo(gameObject.tag, false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(espacoCorretoTag))
        {
            Debug.Log($"{gameObject.tag} foi removida do espa�o correto");
            gameManeger.AtualizarPosicaoOvo(gameObject.tag, false);
        }
    }
}
