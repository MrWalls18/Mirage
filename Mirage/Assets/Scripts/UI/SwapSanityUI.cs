using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapSanityUI : MonoBehaviour
{
    public GameObject lowSanity, medLowSanity, medHighSanity, highSanity;

    [SerializeField] PlayerStats myStats;


    float sanityPercent;

    // Update is called once per frame
    void Update()
    {

        sanityPercent = (myStats.sanity / myStats.maxSanity) * 100;

        if (sanityPercent < 25f)
        {
            lowSanity.SetActive(true);
            medLowSanity.SetActive(false);
            medHighSanity.SetActive(false);
            highSanity.SetActive(false);
        }
        else if (sanityPercent < 50f)
        {
            lowSanity.SetActive(false);
            medLowSanity.SetActive(true);
            medHighSanity.SetActive(false);
            highSanity.SetActive(false);
        }
        else if (sanityPercent < 75f)
        {
            lowSanity.SetActive(false);
            medLowSanity.SetActive(false);
            medHighSanity.SetActive(true);
            highSanity.SetActive(false);
        }
        else
        {
            lowSanity.SetActive(false);
            medLowSanity.SetActive(false);
            medHighSanity.SetActive(false);
            highSanity.SetActive(true);
        }

    }
}
