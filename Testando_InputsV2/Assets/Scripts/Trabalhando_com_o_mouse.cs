using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trabalhando_com_o_mouse : MonoBehaviour
{
    public Renderer rend;
    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    /*
    private void OnMouseDrag()
    {
        rend.material.color -= Color.blue * Time.deltaTime;
    }
    */

    /*
    private void OnMouseDown()
    {
        Destroy(gameObject);
    }
    */

    /*
    private void OnMouseEnter()
    {
        rend.material.color = Color.red;
        Debug.Log("Vc está na área de colisão.");
    }
    */

    
    private void OnMouseExit()
    {
        rend.material.color = Color.green;
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
