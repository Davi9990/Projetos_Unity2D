using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VidaEnemyBoss : MonoBehaviour
{
   public int maxHealth = 5;
   public int currentHealth;
   public bool BoitataON;

    public PlayerLogica Player;

    void Start()
    {
        currentHealth = maxHealth;
        if(Player == null)
        {
            Player = FindObjectOfType<PlayerLogica>();//Busca referancia ao script do Player
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamege(int Damege)
    {
        currentHealth -= Damege;

        if (currentHealth <= 0)
        {
            Destroy(gameObject);

            // Atualiza o progresso global
            GameManager.Instance.Boitata = true;

            // Troca para a tela de seleção de fases
            SceneManager.LoadScene("SelecaoDeFase");
        }
    }
}
