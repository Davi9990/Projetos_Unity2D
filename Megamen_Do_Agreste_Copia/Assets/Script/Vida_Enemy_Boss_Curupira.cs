using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vida_Enemy_Boss_Curupira : MonoBehaviour
{
   public int maxHealth = 5;
   public int currentHealth;
   public bool CurupiraON;

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

        if(currentHealth <= 0)
        {
            Destroy(gameObject);
            CurupiraON = true;
            if(Player != null)
            {
                Player.curupira = true;
            }
        }
    }
}
