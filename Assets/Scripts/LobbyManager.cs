using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    private readonly string gameVersion = "1";

    public Text connectionInfoText;
    public Button joinButton;
    
    private void Start()
    {
        PhotonNetwork.GameVersion = gameVersion;
        PhotonNetwork.ConnectUsingSettings();
        
        joinButton.interactable = false;
        connectionInfoText.text = "Connecting to Master Server...";
    }
    
    public override void OnConnectedToMaster()
    {
        joinButton.interactable = true;
        connectionInfoText.text = "Online : Connected to Master Server !";
    }
    
    public override void OnDisconnected(DisconnectCause cause)
    {
        joinButton.interactable = false;
        connectionInfoText.text = $"OffLine : Connection Disabled {cause.ToString()} - Try reconnecting...";

        PhotonNetwork.ConnectUsingSettings();
    }
    
    public void Connect()
    {
        joinButton.interactable = false;

        if (PhotonNetwork.IsConnected)
        {
            connectionInfoText.text = "Connecting to Random Room...";

            

            PhotonNetwork.JoinLobby();


            SceneManager.LoadScene("WaitingRoom");
        }
        else
        {
            connectionInfoText.text = "OffLine : Connection Disabled - Try reconnecting...";

            PhotonNetwork.ConnectUsingSettings();
        }
    }
    
    
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        connectionInfoText.text = "There is no empty room. Creating new Room.";
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 4 });
        Debug.Log("maked new room");
    }

    //public void GoToStage()
    //public override void OnJoinedRoom()
    //{
    //    connectionInfoText.text = "Connected with Room...";
    //    //PhotonNetwork.LoadLevel("Main");
    //    //PhotonNetwork.LoadLevel("Stage01");
    //    PhotonNetwork.LoadLevel("WaitingRoom");
    //    
    //    //SceneManager.LoadScene("WaitingRoom");
    //}
}