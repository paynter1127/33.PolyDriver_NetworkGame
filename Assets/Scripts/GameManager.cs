using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;



public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager Instance
    {
        get
        {
            if (instance == null) instance = FindObjectOfType<GameManager>();

            return instance;
        }
    }

    private static GameManager instance;

    public Transform[] spawnPositions;
    public GameObject[] playerPrefab;

    public GameObject testCar;


    //옵션 창 오브젝트들
    public GameObject askLeftPopUP;


    private void Start()
    {
        askLeftPopUP.SetActive(false);

        if (PhotonNetwork.CountOfPlayers == 0)
        {
            testCar.SetActive(true);
        }
        else
        {
            testCar.SetActive(false);
            SpawnPlayer();
            
        }
    }

    private void SpawnPlayer()
    {
        var localPlayerIndex = PhotonNetwork.LocalPlayer.ActorNumber - 1; //1,2,3,4 로 생성
        var spawnPosition = spawnPositions[localPlayerIndex % spawnPositions.Length];
        
        PhotonNetwork.Instantiate(playerPrefab[localPlayerIndex].name, spawnPosition.position, spawnPosition.rotation);
    }


    public override void OnLeftRoom()
    {
        Debug.Log("OnLeftRoom 호출");
        SceneManager.LoadScene("Lobby");
    }


    public void askLeftRoom()
    {
        askLeftPopUP.SetActive(true);
    }

    public void Yes()
    {
        Debug.Log("Yes");
        
        if (PhotonNetwork.CountOfPlayers == 0)
        {
            Debug.Log("솔플 떠나기 호출");
            SceneManager.LoadScene("SignIn");
        }
        else
        {
            Debug.Log("leaveRoom 호출");
            PhotonNetwork.LeaveRoom(false);
        }
    }

    public void No()
    {
        Debug.Log("No");
        askLeftPopUP.SetActive(false);
    }
}