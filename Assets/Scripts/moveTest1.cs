using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class moveTest1 : MonoBehaviour
{

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


    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();

       
    }

    // Update is called once per frame
    void Update()
    {
        //if (!photonView.IsMine) return;

        SpeedText.text = " " + (curSpeed * 13.531).ToString("N2") + "Km/h";






        //방향키 조작
        if (Input.GetAxis("Vertical") != 0)
        {
            if (curSpeed >= maxSpeed) curSpeed = maxSpeed;
            else if(curSpeed <= -maxSpeed) curSpeed = -maxSpeed;
            else curSpeed += 8 * Time.deltaTime * Input.GetAxis("Vertical");
        }
        else
        {
            if (curSpeed <= 0) curSpeed = 0;
            else curSpeed -= 3 * Time.deltaTime;
        }



        //위치 이동 업데이트
        tr.Translate(Vector3.forward * curSpeed * Time.deltaTime, Space.Self);
        tr.Rotate(Vector3.up * Time.deltaTime * rotSpeed * Input.GetAxis("Horizontal"));




        //모바일 터치 기능
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


}
