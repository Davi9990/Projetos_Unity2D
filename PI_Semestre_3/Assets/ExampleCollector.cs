using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Exemplo simples: colecione objetos ao tocar (trigger). 
/// - Coloque esse script num objeto com Collider (isTrigger = true) ou no jogador com Collider/rigidbody.
/// - Configure o campo stackManager no inspector.
/// </summary>
public class ExampleCollector : MonoBehaviour
{
    public StackManager stackManager;

    private void Reset()
    {
        // tenta achar automaticamente
        if (stackManager == null)
            stackManager = FindObjectOfType<StackManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // exemplo: empilha apenas objetos com tag "Collectible"
        if (other.gameObject.CompareTag("Collectible"))
        {
            bool ok = stackManager.Push(other.gameObject);
            if (!ok)
            {
                Debug.Log("Não foi possível empilhar (pilha cheia ou objeto inválido).");
            }
        }
    }
}
