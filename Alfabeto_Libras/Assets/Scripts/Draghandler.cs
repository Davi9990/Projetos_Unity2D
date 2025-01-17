using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class Draghandler : MonoBehaviour
{
    private Transform parentToReturnTo; // Pai original do botão

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Armazena o pai original
        parentToReturnTo = transform.parent;

        // Move o botão para o topo da hierarquia para evitar problemas visuais
        transform.SetParent(transform.parent.parent);

        // Permite que outros objetos detectem o botão enquanto é arrastado
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Atualiza a posição do botão para seguir o cursor do mouse
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Retorna o botão ao pai original
        transform.SetParent(parentToReturnTo);

        // Restaura a detecção de raycast para o botão
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}
