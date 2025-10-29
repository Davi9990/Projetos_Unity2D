using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GerenciadorMissoes : MonoBehaviour
{
    public List<Transform> locaisDasMissoes = new List<Transform>();
    public SetaMissao seta; 
    private int missaoAtual = 0;

    void Start()
    {
        if (locaisDasMissoes.Count > 0 && seta != null)
        {
            seta.DefinirAlvo(locaisDasMissoes[missaoAtual]);
        }
    }

    // Chame isso quando o jogador completar a missão
    public void CompletarMissao()
    {
        missaoAtual++;

        if (missaoAtual < locaisDasMissoes.Count)
        {
            seta.DefinirAlvo(locaisDasMissoes[missaoAtual]);
            Debug.Log("Nova missão ativada: " + locaisDasMissoes[missaoAtual].name);
        }
        else
        {
            seta.DefinirAlvo(null);
            Debug.Log("Todas as missões foram completadas!");
        }
    }

    // Se quiser reiniciar tudo
    public void ReiniciarMissoes()
    {
        missaoAtual = 0;
        if (locaisDasMissoes.Count > 0)
            seta.DefinirAlvo(locaisDasMissoes[missaoAtual]);
    }
}
