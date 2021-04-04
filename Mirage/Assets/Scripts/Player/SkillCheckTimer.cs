using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCheckTimer : MonoBehaviour
{
    public GameObject coin, headsUI, tailsUI;
    public Transform coinDropTransform;

    public float coinFlipDuration;
    [HideInInspector] public float maxCoinFlipTime;
    [HideInInspector] public float startAccurateCatchTime, endAccurateCatchTime;
    private bool coinCaught;
    [SerializeField]private Text coinFlipText;
    [SerializeField]private PlayerStats stats;

    [HideInInspector] public bool hasCoin = true;

    bool wasLastFlipHeads;

    // Start is called before the first frame update
    void Awake()
    {
        hasCoin = true;
        coinCaught = false;
        maxCoinFlipTime = coinFlipDuration;

        

       // startAccurateCatchTime = coinFlipDuration * 0.45f;
       // endAccurateCatchTime = coinFlipDuration * 0.55f;

    }

    // Update is called once per frame
    void Update()
    {
        coinFlipText.text = "Coin is flipped. . .";
       // Debug.Log(maxCoinFlipTime);

        if (maxCoinFlipTime > 0)
        {
            maxCoinFlipTime -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (maxCoinFlipTime < startAccurateCatchTime && maxCoinFlipTime > endAccurateCatchTime)
            {
                coinCaught = true;
                hasCoin = true;
            } 


            if (coinCaught)
            {
                if (stats.isHallucinating)
                {
                    if (wasLastFlipHeads)
                    {
                        coinFlipText.text = "Coin: Heads";
                        headsUI.SetActive(true);
                        tailsUI.SetActive(false);
                    }

                    else
                    {
                        coinFlipText.text = "Coin: Tails";
                        headsUI.SetActive(false);
                        tailsUI.SetActive(true);
                    }
                    




                }
                else
                {
                    if (Random.value > 0.5)
                    {
                        headsUI.SetActive(true);
                        tailsUI.SetActive(false);
                        coinFlipText.text = "Coin: Heads";

                        //isHeads = true;
                        wasLastFlipHeads = true;
                    }
                    else
                    {
                        headsUI.SetActive(false);
                        tailsUI.SetActive(true);
                        coinFlipText.text = "Coin: Tails";

                       // isHeads = false;
                        wasLastFlipHeads = false;
                    }
                }
            }

            else
            {
                headsUI.SetActive(false);
                tailsUI.SetActive(false);
                coinFlipText.text = "Coin was dropped!";
                hasCoin = false;
                DropCoin();
            }

            coinCaught = false;
            maxCoinFlipTime = coinFlipDuration;
            this.GetComponent<PlayerMovement>().enabled = true;
           

            this.GetComponent<CoinFlip>().enabled = true;
            this.GetComponent<CoinFlip>().skillBar.SetActive(false);

             this.enabled = false;
        }

        if (maxCoinFlipTime <= 0)
        {
            hasCoin = false;
            coinFlipText.text = "Coin was dropped!";
            DropCoin();
            maxCoinFlipTime = coinFlipDuration;
            this.GetComponent<PlayerMovement>().enabled = true;


            this.GetComponent<CoinFlip>().enabled = true;

            this.GetComponent<CoinFlip>().skillBar.SetActive(false);

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
