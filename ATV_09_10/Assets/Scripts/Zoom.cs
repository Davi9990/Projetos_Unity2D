using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Zoom : MonoBehaviour
{
    Camera mainCamera;
    float touchesPrevPosDifference, touchesCurPosDifference, zoomModifier;
    Vector2 firstTouchPrevPos, secondTouchPrevPos;
    [SerializeField]
    float zoomModifierSpeed = 0.1f;
    [SerializeField]
    TextMeshProUGUI text;
    void Start()
    {
        mainCamera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        //Verificaremos a Quantidade de Toques

        if(Input.touchCount == 2){
            //Pegando a posição do toque

            Touch firstTouch = Input.GetTouch(0);

            Touch secondTouch = Input.GetTouch(1);

            firstTouchPrevPos = firstTouch.position - firstTouch.deltaPosition;

            secondTouchPrevPos = secondTouch.position - secondTouch.deltaPosition;

            touchesPrevPosDifference = (firstTouchPrevPos -secondTouchPrevPos).magnitude;

            touchesCurPosDifference = (firstTouch.position -secondTouch.position).magnitude;

            zoomModifier = (firstTouch.deltaPosition - secondTouch.deltaPosition).magnitude * zoomModifierSpeed;

            if(touchesPrevPosDifference > touchesCurPosDifference)
                mainCamera.orthographicSize += zoomModifier;
            if(touchesPrevPosDifference < touchesCurPosDifference)
                mainCamera.orthographicSize -= zoomModifier;
        }

        mainCamera.orthographicSize = Mathf.Clamp(mainCamera.orthographicSize, 2f, 10f);

        text.text = "Camera Size " + mainCamera.orthographicSize;
    }
}
