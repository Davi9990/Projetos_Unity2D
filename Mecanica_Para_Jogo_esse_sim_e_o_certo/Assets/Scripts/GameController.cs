using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private GameObject painel;

    private int differencesFound;

    void Start()
    {
        painel.SetActive(false);
        differencesFound = 0;
        Difference.DifferenceClicked += CheckResults;
    }

    private void CheckResults()
    {
       differencesFound += 1;
        Debug.Log("Diferenças encontradas: " + differencesFound); // Adiciona uma linha de depuração

        if (differencesFound == 3)
        {
            painel.SetActive(true);
        }
    }

    private void OnDestroy()
    {
        Difference.DifferenceClicked -= CheckResults;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
