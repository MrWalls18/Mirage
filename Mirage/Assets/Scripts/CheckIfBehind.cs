using System.Collections;
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

    public Transform spawnPos;

    private float chanceToSpawn;

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
            yield return new WaitForSeconds(timeInBetweenSpawns);
            SpawnCoyote();
        }
    }


    void SpawnCoyote()
    {
        Vector3 spawnPoint;
        SanityCheck();
        
        if (coyoteClone == null && SamplePostion.Instance.RandomPoint(transform.position, 2f, out spawnPoint) && Random.value < chanceToSpawn)
        {
            coyoteClone = Instantiate(fakeCoyotePrefab, spawnPoint, transform.rotation);

            coyoteClone.transform.LookAt(target);
            coyoteClone.SetActive(false);
            timer = Time.time + coyoteUndetectedLifetime;

            hasSpotted = false;
        }
    }

    void SanityCheck()
    {
        if(PlayerStats.Instance.SanityPercent < 10f)
        {
            chanceToSpawn = 0.2f;
        }
        else if (PlayerStats.Instance.SanityPercent < 20f)
        {
            chanceToSpawn = 0.16f;
        }
        else if (PlayerStats.Instance.SanityPercent < 30f)
        {
            chanceToSpawn = 0.12f;
        }
        else if (PlayerStats.Instance.SanityPercent < 40f)
        {
            chanceToSpawn = 0.08f;
        }
        else if (PlayerStats.Instance.SanityPercent < 50f)
        {
            chanceToSpawn = 0.04f;
        }
        else
        {
            chanceToSpawn = 0f;
        }

        Debug.Log(chanceToSpawn);
    }

    bool IsPlayerFacingCoyote()
    {
        Vector3 toTarget = (coyoteClone.transform.position - target.position).normalized;

        if (Vector3.Dot(toTarget, target.transform.forward) > 0)
        {
            
            coyoteClone.transform.LookAt(target);
            return true;
        }

        else
        {
            if (Vector3.Distance(coyoteClone.transform.position, target.transform.position) > 25f)
            {
                coyoteClone.transform.position = Vector3.MoveTowards(coyoteClone.transform.position, target.position, 10f);
            }
            return false;
        }
    }


}
