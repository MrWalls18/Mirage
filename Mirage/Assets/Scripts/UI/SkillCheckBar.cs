using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCheckBar : MonoBehaviour
{
    private Image skillCheck;
    //public float timeAmt;
   // private float time;

    [SerializeField] private SkillCheckTimer time;

    void Awake()
    {

        skillCheck = this.GetComponent<Image>();
      //  time.maxCoinFlipTime = timeAmt;

        
    }

    // Update is called once per frame
    void Update()
    {
        if (time.maxCoinFlipTime > 0)
        {
           // time -= Time.deltaTime;
            skillCheck.fillAmount = time.maxCoinFlipTime / time.coinFlipDuration;

          //  timeText.text = "Time: " + time.ToString("F");
        }
    }

    public void ResetBar()
    {
        skillCheck.fillAmount = 1f;
       // time = timeAmt;
    }
}
