using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Difference : MonoBehaviour
{
   public static event Action DifferenceClicked = delegate { };

    [SerializeField]
    private GameObject pair;

    private SpriteRenderer spRend;
    void Start()
    {
        spRend = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (spRend.enabled)
    {
        pair.GetComponent<SpriteRenderer>().enabled = true;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        pair.GetComponent<BoxCollider2D>().enabled = false;
        DifferenceClicked();
    }
    else
    {
        spRend.enabled = true;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        pair.GetComponent<BoxCollider2D>().enabled = false;
        DifferenceClicked();
    }
    }
}
