using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gamemanager : MonoBehaviour
{
    public GameObject caixaPrefab;
    public Transform spawnPoint;
    public Transform player;
    private GameObject caixaAtual;
    private int nivel = 0;

    public void Start()
    {
        SpawnCaixa();
    }

    public void OnPlayerJump()
    {
        // Para o movimento da caixa atual
        if (caixaAtual != null)
            caixaAtual.GetComponent<caixamove>().enabled = false;
    }

    public void OnPlayerLanded(Vector3 playerPos)
    {
        float playerY = playerPos.y;

        if (Mathf.Abs(playerY - (nivel + 1)) < 0.5f)
        {
            nivel++;
            SpawnCaixa();
        }
    }

    public void SpawnCaixa()
    {
        Vector3 pos = new Vector3(2, nivel + 1, 0); // nível do topo
        caixaAtual = Instantiate(caixaPrefab, pos, Quaternion.identity);
    }

    public void GameOver()
    {
        Debug.Log("Game Over!");
        // Aqui você pode reiniciar o jogo, mostrar tela de fim, etc.
    }
}
