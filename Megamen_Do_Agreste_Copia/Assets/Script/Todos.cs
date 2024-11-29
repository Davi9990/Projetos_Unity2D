using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Todos : MonoBehaviour
{
    public int vida;
    public float speed;
    protected bool morrendo = false;
    protected SpriteRenderer sp;
    protected Rigidbody2D rb;
    protected Animator anim;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(vida <= 0)
        {
            morrendo = true;
        }
    }
}
