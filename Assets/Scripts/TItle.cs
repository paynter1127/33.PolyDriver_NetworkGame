using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TItle : MonoBehaviour
{

    public GameObject TapToStart;

    private void Start()
    {
        Screen.SetResolution(1280 / 2, 720 / 2, true);
    }

    // Update is called once per frame
    void Update()
    {
        if ((int)(Time.time) % 2 == 0) TapToStart.SetActive(false);
        if ((int)(Time.time) % 2 == 1) TapToStart.SetActive(true);
        Debug.Log(Time.time);

        if(Input.touchCount > 0)
        {
            SceneManager.LoadScene("SignIn");
        }

        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            SceneManager.LoadScene("SignIn");
        }
    }



}
