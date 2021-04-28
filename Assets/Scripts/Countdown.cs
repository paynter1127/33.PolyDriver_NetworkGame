using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;


public class Countdown : MonoBehaviourPunCallbacks
{
    public PhotonView PV;

    public GameObject CountDown;
    public AudioSource GetReady;
    public AudioSource GoAudio;
    public AudioSource MusicStage01;
    public GameObject LapTimer;

    public GameObject[] CarControls;

    public GameObject startBtn;

    //public GameObject CarControls;
    //public ScriptableObject CarControls;

    // Start is called before the first frame update
    void Start()
    {
        PV = photonView;

        
        
    }

    

    
    public void startButton()
    {
        Debug.Log("버튼 스타트");
        
        
        if (PhotonNetwork.CountOfPlayers == 0)
        {
            Debug.Log("테스트 시작");
            startBtn.SetActive(false);
            StartCoroutine(CountStart());
        }
        else
        {
            Debug.Log("멀티 시작");
            PV.RPC("startCount", RpcTarget.All);
        }
        
    }



    [PunRPC]
    public void startCount()
    {
        startBtn.SetActive(false);
        StartCoroutine(CountStart());
    }

    IEnumerator CountStart()
    {
        yield return new WaitForSeconds(0.5f);

        CountDown.GetComponent<Text>().text = "3";
        GetReady.Play();
        CountDown.SetActive(true);
        yield return new WaitForSeconds(1);
        CountDown.SetActive(false);

        CountDown.GetComponent<Text>().text = "2";
        GetReady.Play();
        CountDown.SetActive(true);
        yield return new WaitForSeconds(1);
        CountDown.SetActive(false);

        CountDown.GetComponent<Text>().text = "1";
        GetReady.Play();
        CountDown.SetActive(true);
        yield return new WaitForSeconds(1);
        CountDown.SetActive(false);

        GoAudio.Play();
        MusicStage01.Play();
        LapTimer.SetActive(true);

        //for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        //{
        //    CarControls[i].SetActive(true);
        //}

    }
    
}
