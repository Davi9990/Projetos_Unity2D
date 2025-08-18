using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vector_Lerp : MonoBehaviour
{
    public Vector2 destination;


    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.Lerp(transform.position, destination, Time.deltaTime); 
    }
}
