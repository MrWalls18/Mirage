﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkWater : MonoBehaviour
{
    private PlayerStats myStats;
    private PlayerMove movePlayer;
    public float waterTimer;
    private float timer;

    // Start is called before the first frame update
    void Awake()
    {
        myStats = this.GetComponent<PlayerStats>();
        movePlayer = this.GetComponent<PlayerMove>();
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
               // Debug.Log("Hit something");
              //  Debug.Log(hit.collider.gameObject.name);
                if (hit.collider.tag == "Water")
                {
                    StartCoroutine(DrinkTimer());
                    myStats.DrinkWater(hit.collider.GetComponent<WaterSource>().waterPoints, hit.collider.name);
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