using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrastando_Geladeira : MonoBehaviour
{
    public SpriteRenderer Porta;
    public SpriteRenderer Vitoria;
    private Vector3 offset;
    private bool isDragging = false;

    void Start()
    {
        Porta.enabled = true;
        Vitoria.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0)
        {
           Touch touch = Input.GetTouch(0);

            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

            if (touch.phase == TouchPhase.Began)
            {
                RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.zero);
                if (hit.collider != null && hit.collider.gameObject == gameObject)
                {
                    offset = transform.position - touchPosition;
                    isDragging = true;
                }
            }
            else if (touch.phase == TouchPhase.Moved && isDragging) 
            {
                transform.position = new Vector3(touchPosition.x + offset.x, transform.position.y, transform.position.z);
            }
            else if(touch.phase == TouchPhase.Ended)
            {
                isDragging = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Buttom"))
        {
            Porta.enabled = false;
            Vitoria.enabled = true;
        }
    }
}
