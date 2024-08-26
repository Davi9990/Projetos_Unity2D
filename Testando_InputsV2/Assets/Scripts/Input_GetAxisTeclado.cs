using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Input_GetAxisTeclado : MonoBehaviour
{
  
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float T = Input.GetAxis("AulaTeste");
        float V = Input.GetAxis("AulaTeste2");

        transform.Translate(new Vector3(T * Time.deltaTime, V * Time.deltaTime, 0));
        //transform.Translate(new Vector3(T * Time.deltaTime,0,0));
    }
}
