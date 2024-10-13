using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class LevelSelector : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public GameObject levelHolder;    // Referência ao painel principal na cena
    public GameObject levelIcon;      // Prefab dos botões de nível
    public GameObject thisCanvas;     // O Canvas ao qual os painéis e ícones serão filhos
    public int numberOfLevels = 50;   // Número total de fases
    private Rect panelDimensions;     // Dimensões do painel
    private Rect iconDimensions;      // Dimensões de cada ícone
    private int amountPerPage;        // Quantidade de ícones por página
    private int currentLevelCount;    // Contador do número atual de níveis criados

    private GridLayoutGroup mainGridLayout;  // Referência ao GridLayoutGroup do painel principal
    private Vector3 panelLocation;    // Localização atual do painel
    public float percentThreshold = 0.2f; // Percentual para determinar o deslizamento
    public float easing = 0.5f;        // Tempo de suavização do movimento
    private int currentPage = 1;      // Página atual
    private int totalPages = 1;       // Total de páginas

    void Start()
    {
        // Verifica se as referências estão atribuídas no inspector
        if (levelHolder == null || levelIcon == null || thisCanvas == null)
        {
            Debug.LogError("Um ou mais objetos não estão atribuídos no inspector.");
            return;
        }

        // Obtém as dimensões do painel e do ícone
        panelDimensions = levelHolder.GetComponent<RectTransform>().rect;
        iconDimensions = levelIcon.GetComponent<RectTransform>().rect;

        // Obtém o GridLayoutGroup do painel principal
        mainGridLayout = levelHolder.GetComponent<GridLayoutGroup>();
        if (mainGridLayout == null)
        {
            Debug.LogError("O painel principal não contém um componente GridLayoutGroup.");
            return;
        }

        // Calcula o número máximo de ícones por linha e por coluna
        int maxInARow = Mathf.FloorToInt((panelDimensions.width + mainGridLayout.spacing.x) / (mainGridLayout.cellSize.x + mainGridLayout.spacing.x));
        int maxInACol = Mathf.FloorToInt((panelDimensions.height + mainGridLayout.spacing.y) / (mainGridLayout.cellSize.y + mainGridLayout.spacing.y));

        // Calcula a quantidade de ícones por página
        amountPerPage = maxInARow * maxInACol;

        // Calcula o número total de páginas
        totalPages = Mathf.CeilToInt((float)numberOfLevels / amountPerPage);

        // Carrega os painéis (páginas) e ícones de nível
        LoadPanels(totalPages);
        panelLocation = levelHolder.transform.position; // Define a posição inicial do painel
    }

    void LoadPanels(int numberOfPanels)
    {
        // Inicializa o primeiro painel (Painel principal)
        GameObject currentPanel = levelHolder;
        SetUpGrid(currentPanel, mainGridLayout);  // Passa o GridLayoutGroup principal para copiar as configurações

        // Organiza os ícones de nível nas páginas
        for (int i = 0; i < numberOfPanels; i++)
        {
            int iconsToCreate = Mathf.Min(numberOfLevels - currentLevelCount, amountPerPage); // Calcula quantos ícones criar por página
            LoadIcons(iconsToCreate, currentPanel);

            // Verifica se há necessidade de uma nova página (se não couber no painel principal)
            if (currentLevelCount < numberOfLevels)
            {
                // Cria nova página
                currentPanel = CreateNewPage(i + 1);
            }
        }
    }

    GameObject CreateNewPage(int pageIndex)
    {
        // Cria um novo GameObject para a nova página
        GameObject newPage = new GameObject("Page-" + pageIndex);
        newPage.AddComponent<RectTransform>();
        newPage.transform.SetParent(levelHolder.transform.parent, false);

        // Ajusta o tamanho e a posição da nova página
        RectTransform rt = newPage.GetComponent<RectTransform>();
        rt.sizeDelta = levelHolder.GetComponent<RectTransform>().sizeDelta;
        rt.anchorMin = levelHolder.GetComponent<RectTransform>().anchorMin;
        rt.anchorMax = levelHolder.GetComponent<RectTransform>().anchorMax;
        rt.pivot = levelHolder.GetComponent<RectTransform>().pivot;
        rt.localPosition = new Vector2(levelHolder.GetComponent<RectTransform>().rect.width * pageIndex, 0);

        // Configura o GridLayoutGroup para a nova página copiando as configurações do painel principal
        SetUpGrid(newPage, mainGridLayout);

        return newPage;
    }

    void SetUpGrid(GameObject panel, GridLayoutGroup sourceGridLayout)
    {
        // Adiciona um GridLayoutGroup ao painel para organizar os ícones
        GridLayoutGroup grid = panel.GetComponent<GridLayoutGroup>();
        if (grid == null) grid = panel.AddComponent<GridLayoutGroup>();

        // Copia as configurações do GridLayoutGroup do painel principal
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
            currentLevelCount++; // Incrementa o número total de níveis criados

            // Instancia o ícone de nível
            GameObject icon = Instantiate(levelIcon);
            icon.transform.SetParent(parentObject.transform, false); // Define o ícone como filho do painel

            // Nomeia o ícone com o número correto de nível
            icon.name = "Level " + currentLevelCount;

            // Atualiza o texto dentro do ícone com o número do nível
            TextMeshProUGUI levelText = icon.GetComponentInChildren<TextMeshProUGUI>();
            if (levelText != null)
            {
                levelText.SetText("Level " + currentLevelCount);
            }
            else
            {
                Debug.LogError("O prefab 'levelIcon' não contém um componente TextMeshProUGUI.");
            }
        }

        // Força o GridLayoutGroup a reorganizar os elementos
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
