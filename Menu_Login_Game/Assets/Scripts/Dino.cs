using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dino : MonoBehaviour
{
    
    private DinossaurosCertos gameManeger2;

    public string espacoCorretoTag;


    // Start is called before the first frame update
    void Start()
    {
        
        gameManeger2 = GameObject.Find("GameManeger").GetComponent<DinossaurosCertos>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(espacoCorretoTag))
        {
            Debug.Log($"{gameObject.tag} foi colocada no espaço correto");
            gameManeger2.AtualizarPosicaoOvo(gameObject.tag, true);
        }
        else
        {
            Debug.Log($"{gameObject.tag} foi colocada no espaço errado!");
            gameManeger2.AtualizarPosicaoOvo(gameObject.tag, false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(espacoCorretoTag))
        {
            Debug.Log($"{gameObject.tag} foi removida do espaço correto");
            gameManeger2.AtualizarPosicaoOvo(gameObject.tag, false);
        }
    }
}
