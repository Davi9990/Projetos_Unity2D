using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vida_Enemy_Boss_Iara : MonoBehaviour
{
   public int maxHealth = 5;
   public int currentHealth;
   public bool IaraON;

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
            IaraON = true;
            if(Player != null)
            {
                Player.Iara = true;
            }
        }
    }
}
