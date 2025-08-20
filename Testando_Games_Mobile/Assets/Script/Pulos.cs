using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pulos : MonoBehaviour
{
    [Header("Vectors Directions")]
    private Vector2 startPos;
    private Vector2 direction;
    public bool directionChosen;

    [Header("Pulos")]
    private Rigidbody2D rb;
    public float JumpForce;
    public float velocidade;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.right * velocidade * Time.deltaTime;

        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    rb.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
                    break;
            }
        }
    }
}
