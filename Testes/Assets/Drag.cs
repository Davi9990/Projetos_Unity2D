using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour
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
        //registre a diferen�a entre o centro do objeto e o ponto clicado no plano da c�mera
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        dragging = true;
    }

    private void OnMouseUp() 
    {
        //Para de desenhar
        dragging = false;
    }
}
