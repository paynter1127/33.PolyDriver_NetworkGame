using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Cinemachine;
using UnityEngine.UI;



public class moveTest : MonoBehaviourPun
{
    public GameObject car;

    Transform tr;
    public float curSpeed = 0.0f;
    public float maxSpeed = 20.0f;
    public float rotSpeed = 30.0f;

    public Text SpeedText;


    //터치
    public BoxCollider gogo;
    public BoxCollider nogo;
    public BoxCollider rightgo;
    public BoxCollider leftgo;

    private void Awake()
    {
        if (photonView.IsMine)
        {
            var CM = GameObject.Find("CMcam").GetComponent<CinemachineVirtualCamera>();
            CM.Follow = transform;
            CM.LookAt = transform;

            var MiniCam = GameObject.Find("MiniCamera").GetComponent<Camera>();
            MiniCam.transform.SetParent(transform);

            var ST = GameObject.Find("Speed Text solo").GetComponent<Text>();
            SpeedText = ST;


            //컨트롤러 버튼 가져오기
            var go1 = GameObject.Find("gogo").GetComponent<BoxCollider>();
            gogo = go1;

            var go2 = GameObject.Find("nogo").GetComponent<BoxCollider>();
            nogo = go2;

            var go3 = GameObject.Find("rightgo").GetComponent<BoxCollider>();
            rightgo = go3;

            var go4 = GameObject.Find("leftgo").GetComponent<BoxCollider>();
            leftgo = go4;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (photonView.IsMine)
        {
            Debug.Log("들어옴");
            tr = GetComponent<Transform>();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (!car.GetPhotonView().IsMine) return;
        if (!photonView.IsMine) return;



        //가속 감속 처리(키보드)
        if (Input.GetAxis("Vertical") != 0)
        {
            Debug.Log("들어옴");
            if (curSpeed >= maxSpeed) curSpeed = maxSpeed;
            else if (curSpeed <= -maxSpeed) curSpeed = -maxSpeed;
            else curSpeed += 8 * Time.deltaTime * Input.GetAxis("Vertical");
            //tr.Translate(Vector3.forward * curSpeed * Time.deltaTime, Space.Self);
        }
        //else
        //{
        //    if (curSpeed <= 0) curSpeed = 0;
        //    else curSpeed -= 3 * Time.deltaTime; //자연 감속 처리
        //}


        //이동, 회전 업데이트
        //tr.Translate(Vector3.forward * curSpeed * Time.deltaTime, Space.Self);
        tr.transform.Rotate(Vector3.up * Time.deltaTime * rotSpeed * Input.GetAxis("Horizontal"));

        if (curSpeed > 0)
        {
            curSpeed -= 3 * Time.deltaTime; //자연 감속 처리
            //tr.Translate(Vector3.forward * curSpeed * Time.deltaTime, Space.Self);

            //if (curSpeed <= 1) curSpeed = 0;
        }
        if (curSpeed < 0)
        {
            curSpeed += 3 * Time.deltaTime; //자연 감속 처리
            //tr.Translate(Vector3.forward * curSpeed * Time.deltaTime, Space.Self);

            //if (curSpeed >= -1) curSpeed = 0;
        }

        tr.Translate(Vector3.forward * curSpeed * Time.deltaTime, Space.Self);



        //모바일 터치 기능
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Stationary)
            {
                
                if (gogo.GetComponent<BoxCollider>().bounds.Contains(Input.GetTouch(0).position))//전진
                {
                    if (curSpeed >= maxSpeed) curSpeed = maxSpeed;
                    else curSpeed += 8 * Time.deltaTime;

                    //tr.Translate(Vector3.forward * curSpeed * Time.deltaTime, Space.Self);
                }
                else if (nogo.GetComponent<BoxCollider>().bounds.Contains(Input.GetTouch(0).position))//후진
                {
                    if (curSpeed <= -maxSpeed) curSpeed = -maxSpeed;
                    else curSpeed -= 8 * Time.deltaTime;

                    //tr.Translate(Vector3.forward * curSpeed * Time.deltaTime, Space.Self);
                }

                //회전
                if (rightgo.GetComponent<BoxCollider>().bounds.Contains(Input.GetTouch(0).position))//우회전
                {
                    tr.transform.Rotate(Vector3.up * Time.deltaTime * rotSpeed);
                }
                else if (leftgo.GetComponent<BoxCollider>().bounds.Contains(Input.GetTouch(0).position))//좌회전
                {
                    tr.transform.Rotate(Vector3.up * Time.deltaTime * rotSpeed * -1);
                }
            }
        }
        

    }

    private void LateUpdate()
    {
        if (!photonView.IsMine) return;
        SpeedText.text = " " + (curSpeed * 13.531).ToString("N2") + "Km/h";
    }


}
