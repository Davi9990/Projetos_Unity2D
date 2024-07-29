using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaquePlayer : MonoBehaviour
{
    private bool atacando;
    public Animator animator;

    public Transform PontodeAtaque;
    public float ataqueRanger = 0.5f;
    public LayerMask EnemyLayers;
    

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        atacando = Input.GetButtonDown("Fire2");


        if ( atacando ==  true ) 
        {
            Ataque();
        }

        void Ataque()
        {
            animator.SetTrigger("Ataque");

            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(PontodeAtaque.position, ataqueRanger,EnemyLayers);

            foreach(Collider2D enemy in  hitEnemies)
            {
                enemy.GetComponent<Enemy2>().TakeDamage(5);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(PontodeAtaque.position, ataqueRanger);
    }
}
