using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class Draghandler : MonoBehaviour
{
    private Transform parentToReturnTo; // Pai original do bot�o

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Armazena o pai original
        parentToReturnTo = transform.parent;

        // Move o bot�o para o topo da hierarquia para evitar problemas visuais
        transform.SetParent(transform.parent.parent);

        // Permite que outros objetos detectem o bot�o enquanto � arrastado
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Atualiza a posi��o do bot�o para seguir o cursor do mouse
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Retorna o bot�o ao pai original
        transform.SetParent(parentToReturnTo);

        // Restaura a detec��o de raycast para o bot�o
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}
