using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemys : MonoBehaviour
{
    Rigidbody2D rbGoomba;
    [SerializeField] float speed = 2f;
    [SerializeField] Transform point1, point2;
    [SerializeField] LayerMask layer;
    [SerializeField] bool isColliding;

    private void Awake()
    {
        rbGoomba = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        rbGoomba.velocity = new Vector2(speed,rbGoomba.velocity.y);

       isColliding = Physics2D.Linecast(point1.position,point2.position,layer);

        if(isColliding )
        {
            transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
            speed *= -1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
