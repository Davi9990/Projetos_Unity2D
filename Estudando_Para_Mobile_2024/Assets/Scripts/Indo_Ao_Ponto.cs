using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Indo_Ao_Ponto : MonoBehaviour
{
    public float speed = 5f;
    private Vector3 position;
    private int direcao = 1;
    public Text TextoIndicativo;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Began)
            {
                Vector3 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
                touchPos.z = 0;
                transform.position = touchPos;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Inimigo"))
        {
            Destroy(collision.gameObject);
            TextoIndicativo.text = "Objeto_Destruido";
        }
    }
}
