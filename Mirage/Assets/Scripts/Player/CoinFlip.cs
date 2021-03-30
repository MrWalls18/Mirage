using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinFlip : MonoBehaviour
{
    [SerializeField] SkillCheckTimer coinFlip;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && coinFlip.hasCoin)
        {
            coinFlip.enabled = true;

            this.GetComponent<PlayerMove>().enabled = false;
        }


    }

}
