using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoinFlip : MonoBehaviour
{
    //[SerializeField] SkillCheckTimer coinFlip;
    [SerializeField] SkillCheckBar barReset;
    [SerializeField] RandomizeMarkerPosition marker;

    //hacky solution to tutorial section; fix later
    public TMP_Text subtitle;
    bool firstFlip = true;

    public GameObject skillBar;

    private void Start()
    {
        skillBar.SetActive(false);
        firstFlip = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerMovement.Instance.isGrounded && InventoryUI.Instance.index == 0)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && SkillCheckTimer.s_instance.hasCoin)
            { 
                marker.RandomizeMarker();

                AudioManager.Instance.Play("CoinFlip");

                //coinFlip.hasCoin = false;
                SkillCheckTimer.s_instance.hasCoin = false;
                //coinFlip.enabled = true;
                SkillCheckTimer.s_instance.enabled = true;


                this.GetComponent<PlayerMovement>().enabled = false;

                skillBar.SetActive(true);

                barReset.ResetBar();

                this.enabled = false;


                //hacky solution to tutorial section; fix later
                if (firstFlip == true)
                {
                    StartCoroutine(TooltipTimer());
                    firstFlip = false;
                }

                //hacky solution to tutorial section; fix later
                IEnumerator TooltipTimer()
                {
                    yield return new WaitForSeconds(2);
                    subtitle.text = "If you're hallucinating, the coin will always land on the same side. \n Otherwise, it behaves like a regular coin: 50-50 every flip.";
                    yield return new WaitForSeconds(15);
                    subtitle.gameObject.SetActive(false);
                }
            }
        }


    }

}
