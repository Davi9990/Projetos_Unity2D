using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Troca_de_Imagens : MonoBehaviour
{
    public Image targetImage; // Referência ao componente Image que será trocado
    public TextMeshProUGUI targetText; // Referência ao componente TextMeshProUGUI que será trocado

    // Este método deve ser chamado pelo botão clicado
    public void SwapImageAndText(Button clickedButton)
    {
        // Obtém a imagem e o texto do botão clicado
        Image buttonImage = clickedButton.GetComponent<Image>();
        TextMeshProUGUI buttonText = clickedButton.GetComponentInChildren<TextMeshProUGUI>();

        // Troca a imagem e o texto
        if (buttonImage != null && buttonText != null)
        {
            targetImage.sprite = buttonImage.sprite;
            targetText.text = buttonText.text;
        }
    }
}
