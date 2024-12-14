using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    public static PhotonManager Instance;
    public TextMeshProUGUI statusText; // Referência ao texto UI

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        //DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        if (statusText != null) statusText.text = "Conectando ao Servidor...";
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        if (statusText != null) statusText.text = "Conectado ao Servidor! Entrando no lobby...";
        PhotonNetwork.JoinLobby();
    }

    public void CreateOrJoinRoom()
    {
        if (statusText != null) statusText.text = "Entrando na sala...";
        RoomOptions roomOptions = new RoomOptions { MaxPlayers = 2 };
        PhotonNetwork.JoinOrCreateRoom("InfinityRoom", roomOptions, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        if (statusText != null) statusText.text = "Entrou na sala! Carregando a Game Scene...";
        PhotonNetwork.LoadLevel("Jogo");
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log($"Jogador {newPlayer.NickName} entrou na sala.");
    }
}
