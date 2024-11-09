using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndoEmbora : MonoBehaviour
{
    public SpriteRenderer Vitoria; // Sprite que queremos tornar transparente
    public Buttom vitoriaButton;    // Bot�o que ativa o sprite

    void Start()
    {
        // Obt�m o bot�o (caso n�o esteja atribu�do no inspetor)
        if (vitoriaButton == null)
        {
            vitoriaButton = GetComponent<Buttom>();
        }
        vitoriaButton.Vitoria.enabled = false;
        // Deixa o sprite invis�vel inicialmente
        SetTransparency(0f);
    }

    void Update()
    {
        Fugindo();
        
    }

    void Fugindo()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                // Ativa o sprite e define a transpar�ncia
                vitoriaButton.Vitoria.enabled = true;
                SetTransparency(1f); // Define como opaco (ou o valor de transpar�ncia desejado)
            }
        }
    }

    // Fun��o para definir a transpar�ncia do sprite
    void SetTransparency(float alpha)
    {
        if (Vitoria != null)
        {
            Color color = Vitoria.color;
            color.a = Mathf.Clamp01(alpha); // Garante que o alfa esteja entre 0 e 1
            Vitoria.color = color;
        }
    }
}
