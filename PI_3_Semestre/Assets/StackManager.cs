using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Gerencia uma pilha simples de objetos 3D.
/// Anexe este script a um GameObject vazio que será o "parent" da pilha.
/// </summary>
public class StackManager : MonoBehaviour
{
    [Header("Configuração da Pilha")]
    public Transform stackParent;         // onde os objetos empilhados serão parented (pode ser o próprio objeto com este script)
    public float offsetY = 0.5f;          // distância em Y entre cada item empilhado
    public int maxStack = 50;             // limite opcional de itens na pilha

    private List<GameObject> stack = new List<GameObject>();

    private void Reset()
    {
        // se não for definido, usa o próprio transform
        if (stackParent == null) stackParent = this.transform;
    }

    /// <summary>
    /// Empilha o objeto (posiciona e ajusta física).
    /// </summary>
    public bool Push(GameObject obj)
    {
        if (obj == null) return false;
        if (stack.Count >= maxStack) return false;

        // parent e posicionamento relativo
        obj.transform.SetParent(stackParent);

        Vector3 localPos = Vector3.zero;
        localPos.y = stack.Count * offsetY;
        obj.transform.localPosition = localPos;
        obj.transform.localRotation = Quaternion.identity;

       
        // ajustar física: primeiro zera velocidades, depois torna kinematic
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true;
        }


        // opcional: desativar collider para evitar colisões entre stacked/ambiente
        Collider col = obj.GetComponent<Collider>();
        if (col != null) col.enabled = false;

        stack.Add(obj);
        return true;
    }

    /// <summary>
    /// Remove e retorna o objeto do topo da pilha.
    /// </summary>
    public GameObject Pop()
    {
        if (stack.Count == 0) return null;
        GameObject top = stack[stack.Count - 1];
        stack.RemoveAt(stack.Count - 1);

        // restaurar física se existir (deixar fora da pilha)
        Rigidbody rb = top.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
        }

        Collider col = top.GetComponent<Collider>();
        if (col != null) col.enabled = true;

        top.transform.SetParent(null); // desvincula do parent
        return top;
    }

    /// <summary>
    /// Limpa a pilha (desfaz alterações mínimas).
    /// </summary>
    public void Clear()
    {
        while (stack.Count > 0)
        {
            GameObject g = Pop();
            // opcional: destruir ou reposicionar
            // Destroy(g);
        }
    }

    /// <summary>
    /// Consulta quantos itens há na pilha.
    /// </summary>
    public int Count()
    {
        return stack.Count;
    }
}
