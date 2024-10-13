using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class LevelSelector : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public GameObject levelHolder;    // Refer�ncia ao painel principal na cena
    public GameObject levelIcon;      // Prefab dos bot�es de n�vel
    public GameObject thisCanvas;     // O Canvas ao qual os pain�is e �cones ser�o filhos
    public int numberOfLevels = 50;   // N�mero total de fases
    private Rect panelDimensions;     // Dimens�es do painel
    private Rect iconDimensions;      // Dimens�es de cada �cone
    private int amountPerPage;        // Quantidade de �cones por p�gina
    private int currentLevelCount;    // Contador do n�mero atual de n�veis criados

    private GridLayoutGroup mainGridLayout;  // Refer�ncia ao GridLayoutGroup do painel principal
    private Vector3 panelLocation;    // Localiza��o atual do painel
    public float percentThreshold = 0.2f; // Percentual para determinar o deslizamento
    public float easing = 0.5f;        // Tempo de suaviza��o do movimento
    private int currentPage = 1;      // P�gina atual
    private int totalPages = 1;       // Total de p�ginas

    void Start()
    {
        // Verifica se as refer�ncias est�o atribu�das no inspector
        if (levelHolder == null || levelIcon == null || thisCanvas == null)
        {
            Debug.LogError("Um ou mais objetos n�o est�o atribu�dos no inspector.");
            return;
        }

        // Obt�m as dimens�es do painel e do �cone
        panelDimensions = levelHolder.GetComponent<RectTransform>().rect;
        iconDimensions = levelIcon.GetComponent<RectTransform>().rect;

        // Obt�m o GridLayoutGroup do painel principal
        mainGridLayout = levelHolder.GetComponent<GridLayoutGroup>();
        if (mainGridLayout == null)
        {
            Debug.LogError("O painel principal n�o cont�m um componente GridLayoutGroup.");
            return;
        }

        // Calcula o n�mero m�ximo de �cones por linha e por coluna
        int maxInARow = Mathf.FloorToInt((panelDimensions.width + mainGridLayout.spacing.x) / (mainGridLayout.cellSize.x + mainGridLayout.spacing.x));
        int maxInACol = Mathf.FloorToInt((panelDimensions.height + mainGridLayout.spacing.y) / (mainGridLayout.cellSize.y + mainGridLayout.spacing.y));

        // Calcula a quantidade de �cones por p�gina
        amountPerPage = maxInARow * maxInACol;

        // Calcula o n�mero total de p�ginas
        totalPages = Mathf.CeilToInt((float)numberOfLevels / amountPerPage);

        // Carrega os pain�is (p�ginas) e �cones de n�vel
        LoadPanels(totalPages);
        panelLocation = levelHolder.transform.position; // Define a posi��o inicial do painel
    }

    void LoadPanels(int numberOfPanels)
    {
        // Inicializa o primeiro painel (Painel principal)
        GameObject currentPanel = levelHolder;
        SetUpGrid(currentPanel, mainGridLayout);  // Passa o GridLayoutGroup principal para copiar as configura��es

        // Organiza os �cones de n�vel nas p�ginas
        for (int i = 0; i < numberOfPanels; i++)
        {
            int iconsToCreate = Mathf.Min(numberOfLevels - currentLevelCount, amountPerPage); // Calcula quantos �cones criar por p�gina
            LoadIcons(iconsToCreate, currentPanel);

            // Verifica se h� necessidade de uma nova p�gina (se n�o couber no painel principal)
            if (currentLevelCount < numberOfLevels)
            {
                // Cria nova p�gina
                currentPanel = CreateNewPage(i + 1);
            }
        }
    }

    GameObject CreateNewPage(int pageIndex)
    {
        // Cria um novo GameObject para a nova p�gina
        GameObject newPage = new GameObject("Page-" + pageIndex);
        newPage.AddComponent<RectTransform>();
        newPage.transform.SetParent(levelHolder.transform.parent, false);

        // Ajusta o tamanho e a posi��o da nova p�gina
        RectTransform rt = newPage.GetComponent<RectTransform>();
        rt.sizeDelta = levelHolder.GetComponent<RectTransform>().sizeDelta;
        rt.anchorMin = levelHolder.GetComponent<RectTransform>().anchorMin;
        rt.anchorMax = levelHolder.GetComponent<RectTransform>().anchorMax;
        rt.pivot = levelHolder.GetComponent<RectTransform>().pivot;
        rt.localPosition = new Vector2(levelHolder.GetComponent<RectTransform>().rect.width * pageIndex, 0);

        // Configura o GridLayoutGroup para a nova p�gina copiando as configura��es do painel principal
        SetUpGrid(newPage, mainGridLayout);

        return newPage;
    }

    void SetUpGrid(GameObject panel, GridLayoutGroup sourceGridLayout)
    {
        // Adiciona um GridLayoutGroup ao painel para organizar os �cones
        GridLayoutGroup grid = panel.GetComponent<GridLayoutGroup>();
        if (grid == null) grid = panel.AddComponent<GridLayoutGroup>();

        // Copia as configura��es do GridLayoutGroup do painel principal
        grid.cellSize = sourceGridLayout.cellSize;
        grid.spacing = sourceGridLayout.spacing;
        grid.startCorner = sourceGridLayout.startCorner;
        grid.startAxis = sourceGridLayout.startAxis;
        grid.childAlignment = sourceGridLayout.childAlignment;
        grid.constraint = sourceGridLayout.constraint;
        grid.constraintCount = sourceGridLayout.constraintCount;
    }

    void LoadIcons(int numberOfIcons, GameObject parentObject)
    {
        for (int i = 0; i < numberOfIcons; i++)
        {
            currentLevelCount++; // Incrementa o n�mero total de n�veis criados

            // Instancia o �cone de n�vel
            GameObject icon = Instantiate(levelIcon);
            icon.transform.SetParent(parentObject.transform, false); // Define o �cone como filho do painel

            // Nomeia o �cone com o n�mero correto de n�vel
            icon.name = "Level " + currentLevelCount;

            // Atualiza o texto dentro do �cone com o n�mero do n�vel
            TextMeshProUGUI levelText = icon.GetComponentInChildren<TextMeshProUGUI>();
            if (levelText != null)
            {
                levelText.SetText("Level " + currentLevelCount);
            }
            else
            {
                Debug.LogError("O prefab 'levelIcon' n�o cont�m um componente TextMeshProUGUI.");
            }
        }

        // For�a o GridLayoutGroup a reorganizar os elementos
        LayoutRebuilder.ForceRebuildLayoutImmediate(parentObject.GetComponent<RectTransform>());
    }

    public void OnDrag(PointerEventData data)
    {
        float difference = data.pressPosition.x - data.position.x;
        levelHolder.transform.position = panelLocation - new Vector3(difference, 0, 0);
    }

    public void OnEndDrag(PointerEventData data)
    {
        float percentage = (data.pressPosition.x - data.position.x) / Screen.width;
        Vector3 newLocation = panelLocation;
        if (Mathf.Abs(percentage) >= percentThreshold)
        {
            if (percentage > 0 && currentPage < totalPages)
            {
                currentPage++;
                newLocation += new Vector3(-Screen.width, 0, 0);
            }
            else if (percentage < 0 && currentPage > 1)
            {
                currentPage--;
                newLocation += new Vector3(Screen.width, 0, 0);
            }
            StartCoroutine(SmoothMove(levelHolder.transform.position, newLocation, easing));
            panelLocation = newLocation;
        }
        else
        {
            StartCoroutine(SmoothMove(levelHolder.transform.position, panelLocation, easing));
        }
    }

    IEnumerator SmoothMove(Vector3 startpos, Vector3 endpos, float seconds)
    {
        float t = 0f;
        while (t <= 1.0)
        {
            t += Time.deltaTime / seconds;
            levelHolder.transform.position = Vector3.Lerp(startpos, endpos, Mathf.SmoothStep(0f, 1f, t));
            yield return null;
        }
    }
}
