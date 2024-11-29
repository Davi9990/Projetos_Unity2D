using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Example : MonoBehaviour
{
    
    void Start()
    {
        StartCoroutine(WaitAndPrint());
    }

   IEnumerator WaitAndPrint()
    {
        Debug.Log("Come�ando...");
        yield return new WaitForSeconds(2f);
        Debug.Log("Passaram 2 segundos");
    }
}
