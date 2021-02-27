using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCheckTimer : MonoBehaviour
{
    [SerializeField]private float coinFlipDuration;
    private float maxCoinFlipTime;
    private bool coinCaught;
    [SerializeField]private Text coinFlipText;
    [SerializeField]private PlayerStats stats;

    // Start is called before the first frame update
    void Awake()
    {
        coinCaught = false;
        maxCoinFlipTime = coinFlipDuration;
    }

    // Update is called once per frame
    void Update()
    {
        coinFlipText.text = "Coin is flipped. . .";

        if (!coinCaught && maxCoinFlipTime > 0)
        {
            maxCoinFlipTime -= Time.deltaTime;
            coinCaught = Input.GetKeyDown(KeyCode.Space);
        }

        if (coinCaught)
        {
            if (stats.isHallucinating)
            {
                coinFlipText.text = "Coin: Heads";
            }
            else
            {
                if (Random.value > 0.5)
                {
                    coinFlipText.text = "Coin: Heads";
                }
                else
                {
                    coinFlipText.text = "Coin: Tails";
                }
            }

            coinCaught = false;
            maxCoinFlipTime = coinFlipDuration;
            this.enabled = false;
        }

        if (!coinCaught && maxCoinFlipTime <= 0)
        {
            coinFlipText.text = "Coin was dropped!";
            maxCoinFlipTime = coinFlipDuration;
            this.enabled = false;
        }
    }
}
