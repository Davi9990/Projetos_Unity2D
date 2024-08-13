using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnarPlat : MonoBehaviour
{
    public GameObject plat1, plat2;  // Referências aos prefabs das plataformas que serão instanciadas
    public float tempo;              // Tempo de espera entre as instâncias das plataformas

    void Start()
    {
        tempo = 3f;  // Define o tempo inicial de 3 segundos antes que as plataformas sejam instanciadas pela primeira vez
    }

    // Update is called once per frame
    void Update()
    {
        tempo -= Time.deltaTime;  // Reduz o valor de 'tempo' a cada frame, contando o tempo decorrido

        if (tempo < 0)  // Verifica se o tempo chegou a zero ou menos
        {
            Instantiate(plat1);  // Instancia a primeira plataforma na cena
            Instantiate(plat2);  // Instancia a segunda plataforma na cena
            tempo = 3f;  // Reinicia o tempo para 3 segundos para o próximo ciclo de instanciamento
        }
    }
}
