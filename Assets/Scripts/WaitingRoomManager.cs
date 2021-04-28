using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class WaitingRoomManager : MonoBehaviourPunCallbacks
{
    public Text connectionInfoText;
    public Button joinButton;

    //int userNum;

    //연결된 유저 수 만큼 소환
    public GameObject[] nameText;
    public GameObject[] car;
    public GameObject[] carFake;

    private void Start()
    {
        joinButton.interactable = true;

        //Debug.Log(PhotonNetwork.CountOfPlayers - 1);
        nameText[PhotonNetwork.CountOfPlayers - 1].SetActive(true);

        //SpawnConnectedPlayer();
    }

    /*
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
    */

    private void Update()
    {


        int userNum = PhotonNetwork.CountOfPlayers;

        for (int i = 0; i < userNum; i++)
        {
            car[i].SetActive(true);
        }

        for (int i = 0; i < 4; i++)
        {
            carFake[i].transform.Rotate(Vector3.up, Time.deltaTime * 30);
        }


        //if (photonView.IsMine)
        //{
        //    bool activeU = true;
        //    photonView.RPC("activeUser", RpcTarget.All, activeU);
        //}
    }

   


    public void Connect()
    {
        joinButton.interactable = false;

        if (PhotonNetwork.IsConnected)
        {
            connectionInfoText.text = "Connecting to Random Room...";
            PhotonNetwork.JoinRandomRoom();
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
    }
    

    public override void OnJoinedRoom()
    {
        connectionInfoText.text = "Go To RACING !!!";
        Debug.Log(PhotonNetwork.CountOfPlayers);
        PhotonNetwork.LoadLevel("Stage01");
    }

    



}