using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dialogo : MonoBehaviour
{
    public string displayText = "Texto de Exemplo"; // Texto a ser exibido
    public Vector3 textOffset = new Vector3(0, 1, 0); // Offset da posi��o do texto em rela��o ao GameObject
    public GameObject textPrefab; // Prefab do texto a ser instanciado

    private GameObject instantiatedText; // Refer�ncia ao texto instanciado

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (instantiatedText == null)
            {
                // Instancia o texto na posi��o desejada
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
            // Destr�i o texto quando o jogador sai da colis�o (opcional)
            if (instantiatedText != null)
            {
                Destroy(instantiatedText);
                instantiatedText = null;
            }
        }
    }
}
