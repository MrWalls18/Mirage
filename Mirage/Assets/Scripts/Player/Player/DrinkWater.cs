using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkWater : MonoBehaviour
{
    private PlayerStats myStats;
    private PlayerMovement movePlayer;
    public float waterTimer;
    private float timer;

    [SerializeField] private GameObject waterUI;

    // Start is called before the first frame update
    void Awake()
    {
        myStats = this.GetComponent<PlayerStats>();
        movePlayer = this.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hitUI;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hitUI, 5f))
        {
            if (hitUI.collider.tag == "Water")
            {
                waterUI.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    StartCoroutine(DrinkTimer());
                    myStats.DrinkWater(hitUI.collider.GetComponent<WaterSource>().waterPoints, hitUI.collider.name);


                    hitUI.collider.gameObject.GetComponent<WaterSource>().waterPoints -= 1;
                    if (hitUI.collider.gameObject.GetComponent<WaterSource>().waterPoints < 0)
                        hitUI.collider.gameObject.GetComponent<WaterSource>().waterPoints = 0;

                  /*  if (hitUI.collider.name == "Lake")
                    {
                        hitUI.collider.transform.position = new Vector3(hitUI.collider.transform.position.x, hitUI.collider.transform.position.y - 1f, hitUI.collider.transform.position.z);
                    }
                    */
                }
            }

        }
        else
        {
            waterUI.SetActive(false);
        }

    }

    

    IEnumerator DrinkTimer()
    {
        movePlayer.enabled = false;
        yield return new WaitForSeconds(waterTimer);
        movePlayer.enabled = true;
    }
}
