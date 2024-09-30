using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inputs : MonoBehaviour
{
    private Vector3 position;
    private float width;
    private float height;

    void Awake()
    {

        width = (float)Screen.width / 2.0f;
        height = (float)Screen.height / 2.0f;

        position = new Vector3(0.0f, 0.0f, 0.0f);

    }

    // Update is called once per frame
    void Update()
    {
        //Exemplo1(Movimentando pelo toque)
        /*
       if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
            touchPos.z = 0f;
            transform.position = touchPos;
        }
        */


        //Exemplo2(Criando uma linha pelo toque)

        //for (int i = 0; i < Input.touchCount; i++) 
        //{
        //    Vector3 touchPos = Camera.main.ScreenToWorldPoint(Input.touches[i].position);

        //    Debug.DrawLine(Vector3.zero, touchPos, Color.red);
        //}

        //Exemplo3(Debug tocando)

        //if (Input.touchCount > 0)
        //{
        //    Touch touch = Input.GetTouch(0);
        //    if (touch.phase == TouchPhase.Began)
        //    {
        //        Debug.Log("Dedo Clicando!");
        //    }

        //    if (touch.phase == TouchPhase.Canceled)
        //    {
        //        Debug.Log("Toque Incorreto!");
        //    }
        //}

        //Exemplo4(Arrastando)

        if (Input.touchCount > 0)
        {

            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Ended)
            {
                Debug.Log("Tirou o dedo!");
            }
            //movimentou o dedo
            if (touch.phase == TouchPhase.Moved)
            {
                Vector3 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
                touchPos.z = 0f;
                transform.position = touchPos;
            }
            //dedo parado sem se mover na tela
            if (touch.phase == TouchPhase.Stationary)
            {
                Debug.Log("Dedo parado na tela!");
            }
        }

        //if (Input.touchCount > 0)
        //{
        //    Touch touch = Input.GetTouch(0);

        //    if (touch.phase == TouchPhase.Moved)
        //    {
        //        Vector2 pos = touch.position;
        //        pos.x = (pos.x - width) / width;
        //        pos.y = (pos.y - height) / height;
        //        position = new Vector3(pos.x, pos.y, 0.0f);

        //        transform.position = position;
        //    }
        //}

        //if (Input.touchCount > 2) 
        //{
        //   Touch touch = Input.GetTouch(1);

        //    if(touch.phase == TouchPhase.Began)
        //    {
        //        transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
        //    }

        //    if(touch.phase == TouchPhase.Ended)
        //    {
        //        transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        //    }
        //}

    }

    //void OnGUI()
    //{
    //    GUI.skin.label.fontSize = (int)(Screen.width / 25.0f);

    //    GUI.Label(new Rect(20, 20, width, height * 0.25f),
    //        "x = " + position.x.ToString("f2") + ", y = " + position.y.ToString("f2"));
    //}
}
