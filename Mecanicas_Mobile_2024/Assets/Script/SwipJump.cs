using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipJump : MonoBehaviour
{
    private Rigidbody rb;
    private Vector2 startTouchPosition,endTouchPosition;
    private float jumpForce;
    private bool jumpAllowed;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        jumpForce = 300f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) 
        {
            startTouchPosition = Input.GetTouch(0).position;
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended) 
        {
            endTouchPosition = Input.GetTouch(0).position;
        }

        if (endTouchPosition.y > startTouchPosition.y && rb.velocity.y == 0) 
        {
            jumpAllowed = true;
            startTouchPosition = Vector2.zero;
            endTouchPosition = Vector2.zero;
        }
    }

    private void FixedUpdate()
    {
        if (jumpAllowed) 
        {
            rb.AddForce(Vector3.up * jumpForce);
            jumpAllowed = false;
        }
    }
}
