using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Input_GetKeyDown : MonoBehaviour
{
   
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            print("O amigo foi pela direita");
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            print("O amigo for para a esquerda");
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            print("O amigo está descendo para o inferno");
        }
    }
}
