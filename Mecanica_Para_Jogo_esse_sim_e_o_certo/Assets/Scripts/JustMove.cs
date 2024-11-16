using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JustMove : MonoBehaviour
{
   private float move, movespeed, rotation, rotateSpeed;


    void Start()
    {
        movespeed = 200f;
        rotateSpeed = 400f;
    }

    // Update is called once per frame
    void Update()
    {
        move = Input.GetAxis("Vertical") * movespeed * Time.deltaTime;

        rotation = Input.GetAxis("Horizontal") * rotateSpeed * Time.deltaTime;
    }

    private void FixedUpdate()
    {
        transform.Translate(0f,0f,move);
        transform.Rotate(0f,rotation,0f);
    }
}
