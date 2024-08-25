using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PegarEmOvos : MonoBehaviour
{
    
    private bool dragging = false;
    private Vector3 offset;

    private void Update()
    {
        if (dragging)
        {
            //Move o objeto, levando em considera��o o deslocamento original
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
        }
    }

    private void OnMouseDown()
    {
        //Registre a diferen�a entre o centro do objeto e o ponto clicado no plano da camera
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        dragging = true;
    }

    private void OnMouseUp()
    {
        dragging = false;
    }
}
