using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class LogicaDaVitoria : MonoBehaviourPun
{
    public static LogicaDaVitoria Instance;
    public bool gameEnded = false;

    private void Awake()
    {
        Instance = this;
    }

    public void PlayerWon(string playerName)
    {
        if (!PhotonNetwork.IsMasterClient || gameEnded) return;

        photonView.RPC("AnnounceWinner", RpcTarget.All, playerName);
    }

    [PunRPC]
    void AnnounceWinner(string playerName)
    {
        gameEnded = true;
        Debug.Log($"Vencedor: {playerName}");
    }
}
