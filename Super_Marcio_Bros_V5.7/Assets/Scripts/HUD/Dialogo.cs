using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dialogo : MonoBehaviour
{
    public string displayText = "Texto de Exemplo"; // Texto a ser exibido
    public Vector3 textOffset = new Vector3(0, 1, 0); // Offset da posição do texto em relação ao GameObject
    public GameObject textPrefab; // Prefab do texto a ser instanciado

    private GameObject instantiatedText; // Referência ao texto instanciado

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (instantiatedText == null)
            {
                // Instancia o texto na posição desejada
                instantiatedText = Instantiate(textPrefab, transform.position + textOffset, Quaternion.identity);

                // Ajusta o texto
                TextMeshPro tmp = instantiatedText.GetComponent<TextMeshPro>();
                if (tmp != null)
                {
                    tmp.text = displayText;
                }

                // Faz o texto seguir o GameObject (opcional)
                instantiatedText.transform.SetParent(transform);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Destrói o texto quando o jogador sai da colisão (opcional)
            if (instantiatedText != null)
            {
                Destroy(instantiatedText);
                instantiatedText = null;
            }
        }
    }
}
