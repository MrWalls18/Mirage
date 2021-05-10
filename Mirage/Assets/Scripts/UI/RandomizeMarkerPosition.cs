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
    //[SerializeField] private SkillCheckTimer coinTimer;

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

       
        marker.fillAmount = fillAmount;
        marker.fillClockwise = isClockwise; 
        marker.fillOrigin = Random.Range(0, 4);

        

        if (isClockwise)
        {
            switch(marker.fillOrigin) 
            {
                case (int)FillOrigin.Bottom:
                    SkillCheckTimer.s_instance.endAccurateCatchTime = SkillCheckTimer.s_instance.coinFlipDuration - (1.0f);

                    SkillCheckTimer.s_instance.startAccurateCatchTime = SkillCheckTimer.s_instance.coinFlipDuration - (1.0f - (SkillCheckTimer.s_instance.coinFlipDuration * fillAmount));
                    break;
                case (int)FillOrigin.Right:
                    SkillCheckTimer.s_instance.endAccurateCatchTime = SkillCheckTimer.s_instance.coinFlipDuration - (1.5f);

                    SkillCheckTimer.s_instance.startAccurateCatchTime = SkillCheckTimer.s_instance.coinFlipDuration - (1.5f - (SkillCheckTimer.s_instance.coinFlipDuration * fillAmount));
                    break;
                case (int)FillOrigin.Top:
                    marker.fillOrigin = (int)FillOrigin.Left;

                    SkillCheckTimer.s_instance.endAccurateCatchTime = SkillCheckTimer.s_instance.coinFlipDuration - (0.5f);

                    SkillCheckTimer.s_instance.startAccurateCatchTime = SkillCheckTimer.s_instance.coinFlipDuration - (0.5f - (SkillCheckTimer.s_instance.coinFlipDuration * fillAmount));
                    break;
                case (int)FillOrigin.Left:
                    SkillCheckTimer.s_instance.endAccurateCatchTime = SkillCheckTimer.s_instance.coinFlipDuration - (0.5f);

                    SkillCheckTimer.s_instance.startAccurateCatchTime = SkillCheckTimer.s_instance.coinFlipDuration - (0.5f - (SkillCheckTimer.s_instance.coinFlipDuration * fillAmount));
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
                    SkillCheckTimer.s_instance.startAccurateCatchTime = SkillCheckTimer.s_instance.coinFlipDuration - (1.0f);

                    SkillCheckTimer.s_instance.endAccurateCatchTime = SkillCheckTimer.s_instance.coinFlipDuration - (1.0f + (SkillCheckTimer.s_instance.coinFlipDuration * fillAmount));
                    break;
                case (int)FillOrigin.Right:
                    SkillCheckTimer.s_instance.startAccurateCatchTime = SkillCheckTimer.s_instance.coinFlipDuration - (1.5f);

                    SkillCheckTimer.s_instance.endAccurateCatchTime = SkillCheckTimer.s_instance.coinFlipDuration - (1.5f + (SkillCheckTimer.s_instance.coinFlipDuration * fillAmount));
                    break;
                case (int)FillOrigin.Top:
                    marker.fillOrigin = (int)FillOrigin.Left;

                    SkillCheckTimer.s_instance.startAccurateCatchTime = SkillCheckTimer.s_instance.coinFlipDuration - 0.5f;

                    SkillCheckTimer.s_instance.endAccurateCatchTime = SkillCheckTimer.s_instance.coinFlipDuration - (0.5f + (SkillCheckTimer.s_instance.coinFlipDuration * fillAmount));
                    break;
                case (int)FillOrigin.Left:
                    SkillCheckTimer.s_instance.startAccurateCatchTime = SkillCheckTimer.s_instance.coinFlipDuration - 0.5f;

                    SkillCheckTimer.s_instance.endAccurateCatchTime = SkillCheckTimer.s_instance.coinFlipDuration - (0.5f + (SkillCheckTimer.s_instance.coinFlipDuration * fillAmount));
                    break;
                default:

                    break;
            }
        }

        Debug.Log("Start timer: " + SkillCheckTimer.s_instance.startAccurateCatchTime);
        Debug.Log("End timer: " + SkillCheckTimer.s_instance.endAccurateCatchTime);
        Debug.Log(marker.fillOrigin);


    }
}
