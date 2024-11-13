using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse_Click : MonoBehaviour
{
    public Renderer rend;


    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //private void OnMouseEnter()
    //{
    //    rend.material.color = Color.yellow;
    //}

    //private void OnMouseOver()
    //{
    //    rend.material.color -= new Color(0.1f,0,0) * Time.deltaTime;
    //}

    private void OnMouseExit()
    {
        rend.material.color = Color.gray;
    }
}
