using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkWater : MonoBehaviour
{
    private PlayerStats myStats;
    private PlayerMovement movePlayer;
    public float waterTimer;
    private float timer;

    // Start is called before the first frame update
    void Awake()
    {
        myStats = this.GetComponent<PlayerStats>();
        movePlayer = this.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;
           // Debug.Log("In E");

            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit))
            {
                if (hit.collider.tag == "Water")
                {
                    StartCoroutine(DrinkTimer());
                    myStats.DrinkWater(hit.collider.GetComponent<WaterSource>().waterPoints, hit.collider.name);
                    if (hit.collider.name == "Lake")
                    {
                        hit.collider.transform.position = new Vector3(hit.collider.transform.position.x, hit.collider.transform.position.y - 1f, hit.collider.transform.position.z);
                    }
                }
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
