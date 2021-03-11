using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCheckTimer : MonoBehaviour
{
    public GameObject coin;
    public Transform coinDropTransform;

    [SerializeField]private float coinFlipDuration;
    private float maxCoinFlipTime, startAccurateCatchTime, endAccurateCatchTime;
    private bool coinCaught;
    [SerializeField]private Text coinFlipText;
    [SerializeField]private PlayerStats stats;

    [HideInInspector] public bool hasCoin = true;

    // Start is called before the first frame update
    void Awake()
    {
        hasCoin = true;
        coinCaught = false;
        maxCoinFlipTime = coinFlipDuration;

        startAccurateCatchTime = coinFlipDuration * 0.45f;
        endAccurateCatchTime = coinFlipDuration * 0.55f;

    }

    // Update is called once per frame
    void Update()
    {
        coinFlipText.text = "Coin is flipped. . .";
        Debug.Log(maxCoinFlipTime);

        if (maxCoinFlipTime > 0)
        {
            maxCoinFlipTime -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (maxCoinFlipTime > startAccurateCatchTime && maxCoinFlipTime < endAccurateCatchTime)
            {
                coinCaught = true;
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
            }

            else
            {
                coinFlipText.text = "Coin was dropped!";
                hasCoin = false;
                DropCoin();
            }

            coinCaught = false;
            maxCoinFlipTime = coinFlipDuration;
            this.GetComponent<PlayerMove>().enabled = true;
            this.enabled = false;
        }

        if (maxCoinFlipTime <= 0)
        {
            hasCoin = false;
            coinFlipText.text = "Coin was dropped!";
            DropCoin();
            maxCoinFlipTime = coinFlipDuration;
            this.GetComponent<PlayerMove>().enabled = true;
            this.enabled = false;
        }
        
    }


    void DropCoin()
    {
        Vector3 randomRadius = Random.insideUnitSphere * 5;

        Vector3 coinDropRadius = new Vector3(randomRadius.x + coinDropTransform.position.x, coinDropTransform.position.y, randomRadius.z + coinDropTransform.position.z);

        Instantiate(coin, coinDropRadius, transform.rotation);
    }
}
