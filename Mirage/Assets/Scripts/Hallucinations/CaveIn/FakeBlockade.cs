﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeBlockade : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            this.gameObject.SetActive(false);
        }
    }
}
