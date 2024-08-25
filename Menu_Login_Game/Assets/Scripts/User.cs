using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class User : MonoBehaviour
{
        public string Nome;
        public string Senha;
        public string Email;

        public User(string nome, string senha, string email)
        {
            Nome = nome;
            Senha = senha;
            Email = email;
        }
}
