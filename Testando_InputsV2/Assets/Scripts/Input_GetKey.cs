using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Input_GetKey : MonoBehaviour
{
   
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("up"))
        {
            print("O companheiro est� atingindo a ilumina��o");
        }
        if (Input.GetKey("down"))
        {
            print("O companheiro est� atingindo o fundo do po�o");
        }
    }
}
