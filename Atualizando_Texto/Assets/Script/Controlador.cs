using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Controlador : MonoBehaviour
{
    public string textoDoSprite; // Texto que será mostrado quando o mouse estiver sobre o sprite
    private TMP_Text textComponent;
    private static string textoPadrao = "NENHUM JOGO ENCONTRADO"; // Texto padrão quando o mouse não estiver sobre nenhum sprite

    void Start()
    {
        // Encontre o componente TextMeshProUGUI no Canvas
        textComponent = GameObject.Find("TextoFoda").GetComponent<TMP_Text>();
    }

    void OnMouseEnter()
    {
        // Atualiza o texto quando o mouse entra no sprite
        textComponent.text = textoDoSprite;
    }

    void OnMouseExit()
    {
        // Volta ao texto padrão quando o mouse sai do sprite
        textComponent.text = textoPadrao;
    }
}
