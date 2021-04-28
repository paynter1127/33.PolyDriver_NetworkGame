﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class halfPointTrigger : MonoBehaviour
{
    public GameObject lapCompleteTrig;
    public GameObject halfLapTrig;

    private void OnTriggerEnter(Collider other)
    {
        lapCompleteTrig.SetActive(true);
        halfLapTrig.SetActive(false);
    }


}
