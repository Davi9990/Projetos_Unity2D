using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Virarando_o_Rosto : MonoBehaviour
{
    public float velocidade = 3.0f;
    public float jumpforce = 600f;
    public bool facingRight = true;
    public float moveX;
    private Rigidbody2D objeto;
    private Transform transform;

    void Start()
    {
        objeto = GetComponent<Rigidbody2D>();
        transform = GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        Mover();
        //PularPersonagem();
    }

    void Mover()
    {
     moveX = Input.GetAxis("Horizontal");
     if (moveX > 0 && !facingRight)
     {
       virarPersonagem();
     }
    else if (moveX < 0 && facingRight)
     {
       virarPersonagem();
     }
        objeto.velocity = new Vector2(moveX * velocidade, objeto.velocity.y);
    }

    void virarPersonagem()
    {
        facingRight = !facingRight;
        Vector3 scale =
        GetComponent<Transform>().localScale;
        scale.x *= -1;
         GetComponent<Transform>().localScale = scale;
    }

    /*
    void PularPersonagem()
    {
        var absY = Mathf.Abs(objeto.velocity.y);
        if (Input.GetKeyDown("space") && absY <= 0.05)
        {
            objeto.AddForce(new Vector2(objeto.velocity.x, jumpforce));
        }
    }
    */
}
