using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    private PlayerStats myStats;
    private PlayerMovement movePlayer;
    public float waterTimer;
    private float timer;

    [SerializeField] private GameObject waterUI;


    GameManager myGM;
    // Start is called before the first frame update
    void Awake()
    {
        myGM = GameObject.Find("Game Manager").GetComponent<GameManager>();

        myStats = this.GetComponent<PlayerStats>();
        movePlayer = this.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 5f))
        {
            if (hit.collider.tag == "Water")
            {
                ActivateDrinkWater(hit);
            }

            if (hit.collider.tag == "FakeKey")
            {
                if(Input.GetKeyDown(KeyCode.E))
                {
                    hit.collider.gameObject.GetComponent<FakeKey>().ResetKeyLocations();
                }
            }

            if (hit.collider.name == "CarKey")
            {
                if(Input.GetKeyDown(KeyCode.E))
                {
                    myGM.HasKey = true;
                    myGM.DestroyCarKeySpawns();
                }
                
            }
        }
        else
        {
            waterUI.SetActive(false);
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
}
