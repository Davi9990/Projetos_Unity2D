using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragBall : MonoBehaviour
{
    float deltaX, deltaY;

    Rigidbody2D rb;

    bool moveAllowed = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        PhysicsMaterial2D mat = new PhysicsMaterial2D();
        mat.bounciness = 0.75f;
        mat.friction = 0.4f;
        GetComponent<CircleCollider2D>().sharedMaterial = mat;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            Vector2 touchpos = Camera.main.ScreenToWorldPoint(touch.position);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    if(GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchpos))
                    {
                        deltaX = touchpos.x - transform.position.x;
                        deltaY = touchpos.y - transform.position.y;

                        moveAllowed = true;

                        rb.freezeRotation = true;
                        rb.velocity = new Vector2(0, 0);
                        rb.gravityScale = 0;
                        GetComponent<CircleCollider2D>().sharedMaterial = null;
                    }
                break;

                case TouchPhase.Moved:
                    if(GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchpos) && moveAllowed)
                    {
                        rb.MovePosition(new Vector2(touchpos.x - deltaX, touchpos.y - deltaY));
                    }
                break;

                case TouchPhase.Ended:
                    moveAllowed = false;
                    rb.freezeRotation = false;
                    rb.gravityScale = 2;
                    PhysicsMaterial2D mat = new PhysicsMaterial2D();
                    mat.bounciness = 0.75f;
                    mat.friction = 0.4f;
                    GetComponent<CircleCollider2D>().sharedMaterial = mat;
                    break;
            }

        }
    }
}
