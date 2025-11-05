using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class UIRede : MonoBehaviourPunCallbacks
{    
    public Text textoStatus;  // Mostra mensagens na tela se a gente quiser
    private string nomeSala = "Sala1"; // Nome fixo da sala ou podemos mudar o nome voces verificam ai

    void Start()
    {
        if (textoStatus != null)
            textoStatus.text = "Conectando ao servidor...";
        // Define o nome do jogador (pega salvo ou cria um novo aleatório)
        PhotonNetwork.NickName = PlayerPrefs.GetString("usuarioAtual", "Jogador_" + Random.Range(1000, 9999));
        // Conecta ao servidor Photon
        PhotonNetwork.ConnectUsingSettings();
        // Faz com que todos os jogadores carreguem a mesma cena automaticamente
        PhotonNetwork.AutomaticallySyncScene = true;
        // Ajustes para mobile
        Application.targetFrameRate = 60;              // Mantém 60 FPS
        Screen.sleepTimeout = SleepTimeout.NeverSleep; // Impede que a tela apague
    }
    // Quando conectar ao servidor Photon
    public override void OnConnectedToMaster()
    {
        if (textoStatus != null)
            textoStatus.text = "Conectado ao servidor! Entrando ou criando sala...";
        // Tenta entrar na sala "Sala1"
        // Se ela não existir, o Photon cria automaticamente
        PhotonNetwork.JoinOrCreateRoom(nomeSala, new RoomOptions
        {
            MaxPlayers = 4,  // Máximo de 4 jogadores
            IsVisible = true,
            IsOpen = true
        }, TypedLobby.Default);
    }
    // Quando o jogador entra na sala com sucesso
    public override void OnJoinedRoom()
    {
        if (textoStatus != null)
            textoStatus.text = "Entrou na sala: " + PhotonNetwork.CurrentRoom.Name;
        // Apenas o primeiro jogador (o líder) carrega a cena do jogo
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("CenaJogo"); // Nome da cena do seu jogo
        }
    }
    // Se falhar ao entrar na sala
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        if (textoStatus != null)
            textoStatus.text = "Falha ao entrar na sala: " + message + "\nCriando sala...";

        // Cria a sala se não conseguir entrar
        PhotonNetwork.CreateRoom(nomeSala, new RoomOptions { MaxPlayers = 4 });
    }
    // Se falhar ao criar a sala
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        if (textoStatus != null)
            textoStatus.text = "Falha ao criar sala: " + message + "\nTentando entrar novamente...";

        // Se a criação falhar, tenta entrar na sala existente
        PhotonNetwork.JoinRoom(nomeSala);
    }
    // Quando um jogador sai da sala
    public override void OnPlayerLeftRoom(Player outroJogador)
    {
        if (textoStatus != null)
            textoStatus.text = $"{outroJogador.NickName} saiu da sala.";

        // O Photon automaticamente passa a liderança (MasterClient) para outro jogador
        // Aqui apenas mostramos quem é o novo líder
        if (PhotonNetwork.IsMasterClient)
        {
            if (textoStatus != null)
                textoStatus.text += "\nNovo líder: " + PhotonNetwork.NickName;
        }
    }
    // Quando o líder da sala muda
    public override void OnMasterClientSwitched(Player novoLider)
    {
        if (textoStatus != null)
            textoStatus.text = $"Novo líder da sala: {novoLider.NickName}";
    }
}
