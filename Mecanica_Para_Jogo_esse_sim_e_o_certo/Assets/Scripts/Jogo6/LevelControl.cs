using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelControl : MonoBehaviour
{
    GameObject[] toEnable, toDisable;

    public GameObject correctSing, incorrectSign, cup, trophySing;

    int currentSceneIndex;

    public string whichCupGut = "CupGut";


    void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        toEnable = GameObject.FindGameObjectsWithTag("ToEnable");
        toDisable = GameObject.FindGameObjectsWithTag("ToDisable");

        foreach(GameObject element in toEnable)
        {
            element.gameObject.SetActive(false);
        }
    }

    public void RightAnswer()
    {
        foreach(GameObject element in toDisable)
        {
            element.gameObject.SetActive(false);
        }

        correctSing.gameObject.SetActive(true);

        int Cupgot = PlayerPrefs.GetInt(whichCupGut);

        if (Cupgot == 1)
        {
            Invoke("LoadNextLevel", 1f);
        }
        else
        {
            Invoke("GetTrophy", 1f);
        }
    }

    public void WrongAnswer()
    {
        foreach(GameObject element in toDisable)
        {
            element.gameObject.SetActive(false);
        }

        incorrectSign.SetActive(true);

        Invoke("GotoMainMenu", 1f);
    }

    void GetTrophy()
    {
        correctSing.SetActive(false);

        cup.SetActive(true);

        trophySing.SetActive(true);

        PlayerPrefs.SetInt(whichCupGut, 1);

        Invoke("LoadNextLevel", 1f);
    }

    void LoadNextLevel()
    {
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    void GotoMainMenu()
    {
        SceneManager.LoadScene("Jogo6");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
