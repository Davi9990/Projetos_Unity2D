using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DadosPersonagem
{
    public string nome;        // Nome do personagem
    public string sexo;        // Masculino ou Feminino
    public string spriteNome;  // Nome do sprite ou ícone (opcional)
}

[System.Serializable]
public class ListaDePersonagens
{
    public List<DadosPersonagem> personagens = new List<DadosPersonagem>();
}
