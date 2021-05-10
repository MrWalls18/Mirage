using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using UnityEngine;
using UnityEngine.UI;

public class SkillCheckTimer : SingletonPattern<SkillCheckTimer>
{
    public static SkillCheckTimer s_instance;

    public GameObject coin, headsUI, tailsUI;
    public Transform coinDropTransform;

    public float coinFlipDuration;
    [HideInInspector] public float maxCoinFlipTime;
    [HideInInspector] public float startAccurateCatchTime, endAccurateCatchTime;
    private bool coinCaught;
    public Text coinFlipText;
    [SerializeField] private PlayerStats stats;

    [HideInInspector] public bool hasCoin = true;

    bool wasLastFlipHeads;

    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        s_instance = this;

        hasCoin = true;
        coinCaught = false;
        maxCoinFlipTime = coinFlipDuration;

        

       // startAccurateCatchTime = coinFlipDuration * 0.45f;
       // endAccurateCatchTime = coinFlipDuration * 0.55f;

    }

    // Update is called once per frame
    void Update()
    {
        coinFlipText.gameObject.SetActive(true);
        coinFlipText.text = "Coin is flipped. . .";
       // Debug.Log(maxCoinFlipTime);

        if (maxCoinFlipTime > 0)
        {
            maxCoinFlipTime -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            StartCoroutine(SetCoinUI(true, 0f));

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
                StartCoroutine(SetCoinUI(false, 0));
                hasCoin = false;
                DropCoin();
            }

            StartCoroutine(SetCoinUI(false, 5f));

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
            headsUI.SetActive(false);
            tailsUI.SetActive(false);
            coinFlipText.text = "Coin was dropped!";
            DropCoin();
            maxCoinFlipTime = coinFlipDuration;
            this.GetComponent<PlayerMovement>().enabled = true;


            this.GetComponent<CoinFlip>().enabled = true;

            this.GetComponent<CoinFlip>().skillBar.SetActive(false);

            StartCoroutine(SetCoinUI(false, 0f));


            this.enabled = false;

            
        }
        
    }

    [SerializeField] private SamplePostion checkNavMesh;

    void DropCoin()
    {
        float randomRadius = 5f;

        Vector3 point;

        if (checkNavMesh.RandomPoint(this.transform.position, randomRadius, out point))
        {
            Instantiate(coin, point, transform.rotation);
        }

        //Vector3 coinDropRadius = new Vector3(randomRadius.x + coinDropTransform.position.x, coinDropTransform.position.y, randomRadius.z + coinDropTransform.position.z);
    }

    public IEnumerator SetCoinUI(bool active, float seconds)
    {
        yield return new WaitForSeconds(seconds); //if activating, do so instantly (0 sec); if deactivating, wait 5 seconds before doing so

        headsUI.SetActive(active);
        tailsUI.SetActive(active);

        if (coinFlipText.text == "Coin: Heads" || coinFlipText.text == "Coin: Tails")
            coinFlipText.gameObject.SetActive(active);
    }

    
    
}
