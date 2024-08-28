using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaGaiola : MonoBehaviour
{
    public int vidaMaxima = 5;
    private int vidaatual;
    private SpriteRenderer Sprite;
    public Sprite spriteMorto;
    private bool isDead = false;

    void Start()
    {
        vidaatual = vidaMaxima;
        Sprite = GetComponent<SpriteRenderer>();
    }

    public void ReceberDano(int dano)
    {
        if (isDead) return;

        vidaatual -= dano;

        if (vidaatual <= 0)
        {
            Debug.Log("Sprite Modificado");
            TrocarDeSprite();
        }
        else
        {
            StartCoroutine(PiscarVermelho());
        }
    }

    IEnumerator PiscarVermelho()
    {
        Sprite.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        Sprite.color = Color.white;
    }

    void TrocarDeSprite()
    {
       isDead = true;
        Sprite.sprite = spriteMorto;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
