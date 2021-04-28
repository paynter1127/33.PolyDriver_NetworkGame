using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class LapComplete : MonoBehaviourPunCallbacks
{

    public PhotonView PV;
    public GameObject controller;


    public GameObject LapCompleteTrig;
    public GameObject HalfLapTrig;

    public GameObject MinuteDisplay;
    public GameObject SecondDisplay;
    public GameObject MilliDisplay;

    //현재 랩 알리는 UI
    public GameObject LapCounter;

    public int LapsDone;
    public int DemandedLap = 1;

    //승리 트리거
    public ParticleSystem[] victoryParticle;
    public GameObject gameOver;

    private void Start()
    {
        PV = photonView;
    }

    


    private void VParticle()
    {
        victoryParticle[0].Play();
    }
    private void VParticle1()
    {
        victoryParticle[1].Play();
    }
    private void VParticle2()
    {
        victoryParticle[2].Play();
        gameOver.SetActive(true);//승리 팝업창
    }

    private void OnTriggerEnter(Collider other)
    {
        //랩 숫자 증가
        LapsDone++;

        //승리 처리
        if(LapsDone == DemandedLap)
        {
            Debug.Log("승리");

            //파티클
            InvokeRepeating("VParticle", 0f, 3f);
            InvokeRepeating("VParticle1", 0.5f, 3f);
            InvokeRepeating("VParticle2", 1f, 3f);

            


            //승리 시 컨트롤러 정지
            PV.RPC("deActiveController", RpcTarget.All);
        }


        if (LapTImeManager.MilliCount <= 9)
        {
            MilliDisplay.GetComponent<Text>().text = "0" + LapTImeManager.MilliCount;
        }
        else
        {
            MilliDisplay.GetComponent<Text>().text = "" + LapTImeManager.MilliCount;
        }

        if (LapTImeManager.SecondCount <= 9)
        {
            SecondDisplay.GetComponent<Text>().text = "0" + LapTImeManager.SecondCount + ".";
        }
        else
        {
            SecondDisplay.GetComponent<Text>().text = "" + LapTImeManager.SecondCount + ".";
        }

        if (LapTImeManager.MinuteCount <= 9)
        {
            MinuteDisplay.GetComponent<Text>().text = "0" + LapTImeManager.MinuteCount + ":";
        }
        else
        {
            MinuteDisplay.GetComponent<Text>().text = "" + LapTImeManager.MinuteCount + ":";
        }

        PlayerPrefs.SetInt("MinSave", LapTImeManager.MinuteCount);
        PlayerPrefs.SetInt("SecSave", LapTImeManager.SecondCount);
        PlayerPrefs.SetFloat("MilSave", LapTImeManager.MilliCount);

        LapTImeManager.MinuteCount = 0;
        LapTImeManager.SecondCount = 0;
        LapTImeManager.MilliCount = 0;

        LapCounter.GetComponent<Text>().text = "" + LapsDone + " ";
        

        HalfLapTrig.SetActive(true);
        LapCompleteTrig.SetActive(false);
    }


    [PunRPC]
    public void deActiveController()
    {
        controller.SetActive(false);
    }
}
