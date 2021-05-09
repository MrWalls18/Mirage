﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIfBehind : MonoBehaviour
{
    public Transform target;
    public int timeInBetweenSpawns;
    public float coyoteUndetectedLifetime;
    private float timer;

    [SerializeField] private GameObject fakeCoyotePrefab;
    private GameObject coyoteClone;

    private bool hasSpotted = false;

    private void Awake()
    {
        StartCoroutine(CloseEnemyTimer());
    }

    void Update()
    {
        if (coyoteClone != null)
        {

            if (IsPlayerFacingCoyote())
            {
                coyoteClone.SetActive(true);
                hasSpotted = true;
                timer = 0f;
            }
            else if (!hasSpotted && timer < Time.time)
            {
                Destroy(coyoteClone);
            }
        }
    }



    IEnumerator CloseEnemyTimer()
    {
        while (true)
        {
            Debug.Log("Waiting for " + timeInBetweenSpawns + " seconds");
            yield return new WaitForSeconds(timeInBetweenSpawns);
            SpawnCoyote();
            Debug.Log("Wait time passed");
        }
    }


    void SpawnCoyote()
    {
        Vector3 spawnPoint;
        
        if (coyoteClone == null && SamplePostion.Instance.RandomPoint(transform.position, 2f, out spawnPoint) )
        {
            coyoteClone = Instantiate(fakeCoyotePrefab, spawnPoint, transform.rotation);

            coyoteClone.transform.LookAt(target);
            coyoteClone.SetActive(false);
            timer = Time.time + coyoteUndetectedLifetime;

            hasSpotted = false;
        }
    }


    bool IsPlayerFacingCoyote()
    {
        Vector3 toTarget = (coyoteClone.transform.position - target.position).normalized;

        if (Vector3.Dot(toTarget, target.transform.forward) > 0)
        {
            Debug.Log("Player is facing wolf");
            return true;
        }

        else
        {
            if (Vector3.Distance(coyoteClone.transform.position, target.transform.position) > 25f)
            {
                coyoteClone.transform.position = Vector3.MoveTowards(coyoteClone.transform.position, target.position, 10f);
            }
            Debug.Log("Player is looking away from wolf");
            return false;
        }
    }


}