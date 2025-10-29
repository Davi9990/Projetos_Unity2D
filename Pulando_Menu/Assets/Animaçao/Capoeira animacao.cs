using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Capoeiraanimacao : MonoBehaviour
{
    private Animator animator;
    private float tempoParaTrocar = 3f, tempo;   

    void Start()
    {
        animator = GetComponent<Animator>();
        tempo = 0f;
    }
    void Update()
    {
        tempo += Time.deltaTime;

        if (tempo >= tempoParaTrocar)
        {
            animator.SetBool("MeiaLua",true); 
            tempo = 0f;
        }
    }
}
