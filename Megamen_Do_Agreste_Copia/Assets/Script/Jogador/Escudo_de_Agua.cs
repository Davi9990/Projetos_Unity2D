using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escudo_de_Agua : MonoBehaviour
{
    public int dano;
    public float AreaEscudo;//Raio de espandido do escudo
    private CircleCollider2D circle2D;
    private float lastAttackTime;
    public float TempoDeExpansao;
    public float TempoDeRecarga;

    void Start()
    {
        circle2D = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        ComandandoOLevantamentoDoEscudo();
    }

    public void ComandandoOLevantamentoDoEscudo()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(LevantandoEscudo());
        }
    }

    private IEnumerator LevantandoEscudo()
    {

        circle2D.radius = AreaEscudo;

        yield return new WaitForSeconds(TempoDeExpansao);

        circle2D.radius = 0.5f;

        yield return new WaitForSeconds(TempoDeRecarga); // Espera o tempo de recarga
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemySniper"))
        {
            VidaInimigo Inimigo = collision.GetComponent<VidaInimigo>();
            VidaEnemyBoss Inimigo2 = collision.GetComponent<VidaEnemyBoss>();
            Vida_Enemy_Boss_Iara Inimigo3 = collision.GetComponent<Vida_Enemy_Boss_Iara>();
            Vida_Enemy_Boss_Curupira Inimigo4 = collision.GetComponent<Vida_Enemy_Boss_Curupira>();

            if(Inimigo != null && Time.time >= lastAttackTime + 1f)
            {
                Inimigo.TakeDamege(dano);
                lastAttackTime = Time.time;
            }
            if(Inimigo2 != null && Time.time >= lastAttackTime + 1f)
            {
                Inimigo2.TakeDamege(dano);
                lastAttackTime = Time.time;
            }
            if(Inimigo3 != null && Time.time >= lastAttackTime + 1f)
            {
                Inimigo3.TakeDamege(dano);
                lastAttackTime = Time.time;
            }
             if(Inimigo4 != null && Time.time >= lastAttackTime + 1f)
            {
                Inimigo4.TakeDamege(dano);
                lastAttackTime = Time.time;
            }
        }
    }
}
