using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escudo_de_Agua : MonoBehaviour
{
    public int dano;
    public float AreaEscudo; // Raio de expansão do escudo
    private CircleCollider2D circle2D;
    private float lastAttackTime;
    public float TempoDeExpansao;
    public float TempoDeRecarga;
    private PlayerLogica Pause;

    // Referência à prefab do escudo
    public GameObject escudoPrefab; 

    void Start()
    {
        Pause = GetComponent<PlayerLogica>();
        circle2D = GetComponent<CircleCollider2D>();

        // Desativa a prefab logo no começo
        if (escudoPrefab != null)
        {
            escudoPrefab.SetActive(false);
        }
    }

    void Update()
    {
        if (Pause.isPaused == false)
        {
            ComandandoOLevantamentoDoEscudo();
        }
    }

    public void ComandandoOLevantamentoDoEscudo()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(LevantandoEscudo());
        }
    }

    private IEnumerator LevantandoEscudo()
    {
        // Ativa a prefab do escudo
        if (escudoPrefab != null)
        {
            escudoPrefab.SetActive(true);
        }

        // Expande o escudo
        circle2D.radius = AreaEscudo;

        // Espera o tempo de expansão
        yield return new WaitForSeconds(TempoDeExpansao);

        // Desativa a prefab do escudo
        if (escudoPrefab != null)
        {
            escudoPrefab.SetActive(false);
        }

        // Retorna o raio do escudo para o valor inicial
        circle2D.radius = 0.1f;

        // Espera o tempo de recarga
        yield return new WaitForSeconds(TempoDeRecarga);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemySniper"))
        {
            VidaInimigo Inimigo = collision.GetComponent<VidaInimigo>();
            VidaEnemyBoss Inimigo2 = collision.GetComponent<VidaEnemyBoss>();
            Vida_Enemy_Boss_Iara Inimigo3 = collision.GetComponent<Vida_Enemy_Boss_Iara>();
            Vida_Enemy_Boss_Curupira Inimigo4 = collision.GetComponent<Vida_Enemy_Boss_Curupira>();

            if (Inimigo != null && Time.time >= lastAttackTime + 1f)
            {
                Inimigo.TakeDamege(dano);
                lastAttackTime = Time.time;
            }
            if (Inimigo2 != null && Time.time >= lastAttackTime + 1f)
            {
                Inimigo2.TakeDamage(dano);
                lastAttackTime = Time.time;
            }
            if (Inimigo3 != null && Time.time >= lastAttackTime + 1f)
            {
                Inimigo3.TakeDamege(dano);
                lastAttackTime = Time.time;
            }
            if (Inimigo4 != null && Time.time >= lastAttackTime + 1f)
            {
                Inimigo4.TakeDamege(dano);
                lastAttackTime = Time.time;
            }
        }
    }
}
