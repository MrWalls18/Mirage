using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinFlip : MonoBehaviour
{
    [SerializeField] SkillCheckTimer coinFlip;

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
            coinFlip.hasCoin = false;
            coinFlip.enabled = true;
            
            this.GetComponent<PlayerMovement>().enabled = false;

            skillBar.SetActive(true);

            skillBar.GetComponent<SkillCheckBar>().ResetBar();

            this.enabled = false;
        }


    }

}
