using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vilao : MonoBehaviour
{
    public int vidaMaxima = 5;
    private int vidaAtual;
    private SpriteRenderer Sprite;
    private bool isDead = false;

    void Start()
    {
        vidaAtual = vidaMaxima;
        Sprite = GetComponent<SpriteRenderer>();
    }

    public void ReceberDano(int Dano)
    {
        if(isDead) return;

        vidaAtual -= Dano;

        if(vidaAtual <= 0)
        {
            Debug.Log("Morreu!");
            Destroy(gameObject);
        }
        else
        {
            PiscarDano();
        }
    }

    IEnumerator PiscarDano()
    {
        Sprite.color = Color.black;
        yield return new WaitForSeconds(0.1f);
        Sprite.color = Color.white;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
