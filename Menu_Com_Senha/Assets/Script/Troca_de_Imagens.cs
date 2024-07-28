using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Troca_de_Imagens : MonoBehaviour
{
    public Image targetImage; // Refer�ncia ao componente Image que ser� trocado
    public TextMeshProUGUI targetText; // Refer�ncia ao componente TextMeshProUGUI que ser� trocado

    // Este m�todo deve ser chamado pelo bot�o clicado
    public void SwapImageAndText(Button clickedButton)
    {
        // Obt�m a imagem e o texto do bot�o clicado
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
