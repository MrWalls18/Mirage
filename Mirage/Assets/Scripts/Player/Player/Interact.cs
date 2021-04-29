using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interact : MonoBehaviour
{
    private PlayerStats myStats;
    private PlayerMovement movePlayer;
    public float waterTimer;
    public float boneTextTimer = 3;
    public float raycastDistance = 20f;
    private float timer;

    private Text boneText;
    private Text E_Tooltip;




    [SerializeField] private GameObject waterUI;


    GameManager myGM;
    // Start is called before the first frame update
    void Awake()
    {
        myGM = GameObject.Find("Game Manager").GetComponent<GameManager>();

        myStats = this.GetComponent<PlayerStats>();
        movePlayer = this.GetComponent<PlayerMovement>();

        boneText = GameObject.Find("BoneText").GetComponent<Text>();
        if (boneText.gameObject.activeSelf == true) boneText.gameObject.SetActive(false);

        E_Tooltip = GameObject.Find("E_Tooltip").GetComponent<Text>();
        if (boneText.gameObject.activeSelf == true) boneText.gameObject.SetActive(false);


    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.transform.position + Vector3.forward, Camera.main.transform.forward, out hit, raycastDistance))
        {
           // Debug.Log(hit.collider.name);
            if (hit.collider.tag == "Water")
            {
                Debug.Log("Hit water");
                E_Tooltip.gameObject.SetActive(true);

                ActivateDrinkWater(hit);
            }
            else E_Tooltip.gameObject.SetActive(false);

            if (hit.collider.tag == "FakeKey")
            {
                E_Tooltip.gameObject.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    hit.collider.gameObject.GetComponent<FakeKey>().ResetKeyLocations();
                }
            }
            else E_Tooltip.gameObject.SetActive(false);

            if (hit.collider.name == "CarKey")
            {
                E_Tooltip.gameObject.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    myGM.HasKey = true;
                    myGM.DestroyCarKeySpawns();
                }
                
            }
            else E_Tooltip.gameObject.SetActive(false);


            if (hit.collider.tag == "Bone")
            {
                E_Tooltip.gameObject.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    //disable bone
                    hit.collider.gameObject.SetActive(false);

                    StartCoroutine("BoneTextTimer");
                }
            }
            else E_Tooltip.gameObject.SetActive(false);
        }
        else
        {            
            waterUI.SetActive(false);
            E_Tooltip.gameObject.SetActive(false);
        }

    }

    private void ActivateDrinkWater(RaycastHit hitWater)
    {
        waterUI.SetActive(true);
        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(DrinkTimer());
            myStats.DrinkWater(hitWater.collider.GetComponent<WaterSource>().waterPoints, hitWater.collider.name);


            hitWater.collider.gameObject.GetComponent<WaterSource>().waterPoints -= 1;
            if (hitWater.collider.gameObject.GetComponent<WaterSource>().waterPoints < 0)
                hitWater.collider.gameObject.GetComponent<WaterSource>().waterPoints = 0;

            if (hitWater.collider.name == "Lake")
            {
                hitWater.collider.transform.position = new Vector3(hitWater.collider.transform.position.x, hitWater.collider.transform.position.y - 1f, hitWater.collider.transform.position.z);
            }
        }
    }

    IEnumerator DrinkTimer()
    {
        movePlayer.enabled = false;
        Debug.Log("Player is drinking...");
        yield return new WaitForSeconds(waterTimer);
        Debug.Log("Player is done.");
        movePlayer.enabled = true;
    }

    //controls bone find UI
    IEnumerator BoneTextTimer()
    {
        myStats.bonesFound++;
        boneText.text = "Bones Found: " + myStats.bonesFound + "/8";

        boneText.gameObject.SetActive(true);
        yield return new WaitForSeconds(boneTextTimer);

        boneText.gameObject.SetActive(false);
    }
}
