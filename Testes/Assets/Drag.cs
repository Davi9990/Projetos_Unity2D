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
            //Move o objeto, levando em consideração o deslocamento original
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
        }
    }


    private void OnMouseDown()
    {
        //registre a diferença entre o centro do objeto e o ponto clicado no plano da câmera
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        dragging = true;
    }

    private void OnMouseUp() 
    {
        //Para de desenhar
        dragging = false;
    }
}
