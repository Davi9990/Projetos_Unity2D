using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Input_GetKeyUp : MonoBehaviour
{
   
    void Start()
    {
        
    }

    
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.UpArrow))
        {
            print("O corno manso est� quicando na vara");
        }

        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            print("O corno mamnso est� descescendo no cacete");
        }
    }
}
