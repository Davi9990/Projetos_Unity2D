using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pika : MonoBehaviour
{
    private Vector3 position;
    public Text text;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                Vector3 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
                touchPos.z = 0f;
                transform.position = touchPos;
                text.text = "Agarrando um objeto";
            }
            else
            {
                text.text = "Pegue um novo objeto";
            }
        }
    }
}
