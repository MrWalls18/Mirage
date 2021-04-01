using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomizeMarkerPosition : MonoBehaviour
{
    const float MAX_FILL_AMOUNT = 0.15f;
    const float MIN_FILL_AMOUNT = 0.025f;


    private Image marker;
    [SerializeField] private PlayerStats myStats;
    [SerializeField] private SkillCheckTimer coinTimer;

    private void Awake()
    {
        marker = this.GetComponent<Image>();
    }

    private enum FillOrigin
    {
        Bottom,
        Right,
        Top,
        Left
    };



    public void RandomizeMarker()
    {
        
        float fillAmount;
        bool isClockwise;

        fillAmount = (myStats.sanity / myStats.maxSanity) * (MAX_FILL_AMOUNT - MIN_FILL_AMOUNT) + MIN_FILL_AMOUNT ;
        isClockwise = (Random.value > 0.5);

        marker.fillOrigin = Random.Range(0, 5);
        marker.fillAmount = fillAmount;
        marker.fillClockwise = isClockwise;

        if (isClockwise)
        {
            switch(marker.fillOrigin) 
            {
                case (int)FillOrigin.Bottom:
                    coinTimer.endAccurateCatchTime = coinTimer.coinFlipDuration - (1.0f);

                    coinTimer.startAccurateCatchTime = coinTimer.coinFlipDuration - (1.0f - (coinTimer.coinFlipDuration * fillAmount));
                    break;
                case (int)FillOrigin.Right:
                    coinTimer.endAccurateCatchTime = coinTimer.coinFlipDuration - (1.5f);

                    coinTimer.startAccurateCatchTime = coinTimer.coinFlipDuration - (1.5f - (coinTimer.coinFlipDuration * fillAmount));
                    break;
                case (int)FillOrigin.Top:
                    marker.fillOrigin = (int)FillOrigin.Left;
                    break;
                case (int)FillOrigin.Left:
                    coinTimer.endAccurateCatchTime = coinTimer.coinFlipDuration - (0.5f);

                    coinTimer.startAccurateCatchTime = coinTimer.coinFlipDuration - (0.5f - (coinTimer.coinFlipDuration * fillAmount));
                    break;
                default:
                    break;
            }
        }
        else
        {
            switch (marker.fillOrigin)
            {
                case (int)FillOrigin.Bottom:
                    coinTimer.startAccurateCatchTime = coinTimer.coinFlipDuration - (1.0f);

                    coinTimer.endAccurateCatchTime = coinTimer.coinFlipDuration - (1.0f + (coinTimer.coinFlipDuration * fillAmount));
                    break;
                case (int)FillOrigin.Right:
                    coinTimer.startAccurateCatchTime = coinTimer.coinFlipDuration - (1.5f);

                    coinTimer.endAccurateCatchTime = coinTimer.coinFlipDuration - (1.5f + (coinTimer.coinFlipDuration * fillAmount));
                    break;
                case (int)FillOrigin.Top:
                    marker.fillOrigin = (int)FillOrigin.Left;
                    break;
                case (int)FillOrigin.Left:
                    coinTimer.startAccurateCatchTime = coinTimer.coinFlipDuration - 0.5f;

                    coinTimer.endAccurateCatchTime = coinTimer.coinFlipDuration - (0.5f + (coinTimer.coinFlipDuration * fillAmount));
                    break;
                default:
                    break;
            }
        }

        Debug.Log("Start timer: " + coinTimer.startAccurateCatchTime);
        Debug.Log("End timer: " + coinTimer.endAccurateCatchTime);


    }
}
