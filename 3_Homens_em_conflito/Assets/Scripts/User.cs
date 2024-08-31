using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class User : MonoBehaviour
{
    public string Email;
    public string Senha;

    public User(string email, string senha)
    {
        Email = email;
        Senha = senha;
    }
}
