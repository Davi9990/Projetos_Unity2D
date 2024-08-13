using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemys : MonoBehaviour
{
    Rigidbody2D rbGoomba; // Refer�ncia ao Rigidbody2D do Goomba, usado para controlar seu movimento
    GameObject currentPrefab; // Armazena a refer�ncia ao prefab atual que o Goomba est� usando
    public GameObject prefabUm, prefabDois; // Prefabs diferentes que podem ser atribu�dos ao Goomba
    [SerializeField] float speed = 2f; // Velocidade de movimento do Goomba
    [SerializeField] Transform point1, point2; // Pontos usados para detectar colis�es ou mudan�as de dire��o
    [SerializeField] LayerMask layer; // LayerMask para definir com quais camadas o Goomba pode colidir
    [SerializeField] bool isColliding; // Vari�vel para verificar se o Goomba est� colidindo
    private bool isDead = false; // Estado para verificar se o Goomba est� morto

    private void Awake()
    {
        // Inicializa a refer�ncia ao Rigidbody2D do Goomba no in�cio do jogo
        rbGoomba = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        // Define o prefab inicial do Goomba como prefabUm
        ChangePrefab(prefabUm);
    }

    private void FixedUpdate()
    {
        // Define a velocidade do Goomba, movendo-o na dire��o atual
        rbGoomba.velocity = new Vector2(speed, rbGoomba.velocity.y);

        // Verifica se h� colis�o entre os pontos point1 e point2 usando o LayerMask especificado
        isColliding = Physics2D.Linecast(point1.position, point2.position, layer);

        // Se houver colis�o, inverte a escala do Goomba para que ele mude de dire��o
        if (isColliding)
        {
            transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
            speed *= -1; // Inverte a velocidade para mover na dire��o oposta
        }
    }

    void Update()
    {
        // Inicia a coroutine para alternar entre os prefabs com base no tempo definido
        StartCoroutine(ChangePrefabCoroutine());
    }

    void ChangePrefab(GameObject newPrefab)
    {
        // Destroi o prefab atual se ele j� estiver instanciado
        if (currentPrefab != null)
        {
            Destroy(currentPrefab);
        }

        // Instancia o novo prefab na posi��o e rota��o atuais do Goomba
        currentPrefab = Instantiate(newPrefab, transform.position, transform.rotation);

        // Define o novo prefab como filho do Goomba para que ele siga seus movimentos
        currentPrefab.transform.parent = transform;
    }

    System.Collections.IEnumerator ChangePrefabCoroutine()
    {
        // Espera 2 segundos antes de mudar para prefabUm
        yield return new WaitForSeconds(2);
        ChangePrefab(prefabUm);

        // Espera mais 2 segundos antes de mudar para prefabDois
        yield return new WaitForSeconds(2);
        ChangePrefab(prefabDois);
    }
}
