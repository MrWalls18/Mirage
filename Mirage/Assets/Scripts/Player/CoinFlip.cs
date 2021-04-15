using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinFlip : MonoBehaviour
{
    [SerializeField] SkillCheckTimer coinFlip;
    [SerializeField] SkillCheckBar barReset;
    [SerializeField] RandomizeMarkerPosition marker;

    public GameObject skillBar;

    private void Start()
    {
        skillBar.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && coinFlip.hasCoin)
        {
            

            marker.RandomizeMarker();
            coinFlip.hasCoin = false;
            coinFlip.enabled = true;
            

            this.GetComponent<PlayerMovement>().enabled = false;

            skillBar.SetActive(true);

            barReset.ResetBar();

            this.enabled = false;
        }


    }

}
