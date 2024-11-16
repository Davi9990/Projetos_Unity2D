using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exercitando_GetTouch : MonoBehaviour
{
    private Vector3 position;
    private float width;
    private float height;

    void Start()
    {
        
    }

    void Awake()
    {
        width = (float)Screen.width / 2.0f;
        height = (float)Screen.height / 2.0f;
        // Position used for the cube.
        position = new Vector3(0.0f, 0.0f, 0.0f);
    }

    private void OnGUI()
    {
        GUI.skin.label.fontSize = (int)(Screen.width / 25.0f);
        GUI.Label(new Rect(20, 20, width, height * 0.25f),
        "x = " + position.x.ToString("f2") + ", y = " + position.y.ToString("f2"));
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                Vector2 pos = touch.position;
                pos.x = (pos.x - width) / width;
                pos.y = (pos.y - height) / height;
                position = new Vector3(-pos.x, pos.y, 0.0f);
                // Position the cube.
                transform.position = position;
            }

            if(Input.touchCount == 2)
            {
                touch = Input.GetTouch(1);


                if(touch.phase == TouchPhase.Began)
                {
                    transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
                }
                if(touch.phase == TouchPhase.Ended)
                {
                    transform.localScale = new Vector3(1.0f,1.0f, 1.0f);
                }
            }
        }

        //if (Input.touchCount > 0)
        //{
        //    Touch touch = Input.GetTouch(0);
        //    Vector3 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
        //    touchPos.z = 0f;
        //    transform.position = touchPos;
        //}



        //for(int i = 0; i < Input.touchCount; i++)
        //{
        //    Vector3 touchPos = Camera.main.ScreenToViewportPoint(Input.touches[i].position);

        //    Debug.DrawLine(Vector3.zero, touchPos,  Color.white);
        //}

        //if (Input.touchCount > 0)
        //{
        //    Touch touch = Input.GetTouch(0);
        //    Vector3 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
        //    touchPos.z = 0f;
        //    transform.position = touchPos;
        //}
    }
}
