using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : SingletonPattern<EnemySpawner>
{
    public float timeRemaining;
    public List<GameObject> eSpawner, farEnemies;
    [SerializeField] private GameObject player;
    [SerializeField] private float maxSpawnDistance;    
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float maxNumOfEnemies;

    private int currentNumOfEnemies;
    private GameObject enemyClone;
    private int randomSpawnPos;
    private bool timerIsRunning = false;

    public Text timeText;

    private float waitTime = Mathf.FloorToInt(900f / 60f);

    [Range(1.1f, 2.0f)]
    public float maxEnemiesMultiplier;


    private void Start()
    {
        eSpawner = new List<GameObject>();

        eSpawner.AddRange(GameObject.FindGameObjectsWithTag("eSpawner"));

        timerIsRunning = true;

        StartCoroutine(SpawnMultiplier());
    }

    private void FixedUpdate()
    {
        TimerCountdown();
        DestroyFarAwayEnemies();
    }


    //Counts down timer for the game
    void TimerCountdown()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;
            }
        }
    }

    //Destroys enemies that are too far from the player
    void DestroyFarAwayEnemies()
    {
        List<GameObject> tempList = new List<GameObject>();
        farEnemies = new List<GameObject>();

        farEnemies.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
        farEnemies.AddRange(GameObject.FindGameObjectsWithTag("FakeEnemy"));

        int enemyCounter = farEnemies.Count;

        if (farEnemies.Count > 0)
        {
            foreach (GameObject enemy in farEnemies)
            {
                if (Vector3.Distance(enemy.transform.position, player.transform.position) > maxSpawnDistance)
                {
                    //farEnemies.Remove(enemy);
                    tempList.Add(enemy);
                    Destroy(enemy);
                }
            }

            enemyCounter -= tempList.Count;

            foreach (GameObject enemy in tempList)
            {
                Destroy(enemy);
            }

            currentNumOfEnemies = enemyCounter;

            farEnemies.Clear();
            tempList.Clear();
        }
    }
    
    //Displays how much time is left in the game
    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        if (seconds == 59f)
        {
            StopCoroutine("SpawnEnemy");
            waitTime = minutes;
            
            if (waitTime < 3f)
            {
                waitTime = 3f;
            }
            StartCoroutine("SpawnEnemy");
        }

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

    }

    //After each min that passes, the Max number of enemies increases
    //by multiplying the current max and the maxEnemy multiplier
    IEnumerator SpawnMultiplier()
    {
        while (true)
        {
            yield return new WaitForSeconds(60f);
            maxNumOfEnemies = Mathf.CeilToInt(maxNumOfEnemies * maxEnemiesMultiplier);
        }
    }

    //Spawns enemy after x amount of seconds
    IEnumerator SpawnEnemy()
    {
        
        while (true)
        {
            yield return new WaitForSeconds(waitTime);     

            SetSpawnPoint(enemyPrefab, enemyClone);
        }
    }

    public void SetSpawnPoint(GameObject enemyToSpawn, GameObject clone)
    {
        //Picks a random spawnPoint from the list of enemy spawn points
        randomSpawnPos = UnityEngine.Random.Range(0, eSpawner.Count);

       while (Vector3.Distance(eSpawner[randomSpawnPos].transform.position, player.transform.position) > maxSpawnDistance)
        {
            randomSpawnPos = UnityEngine.Random.Range(0, eSpawner.Count);
        }

        //Sends out a Raycast from the spawn point to the player
        //If there is something in between the player and the spawn point,
        // safe to spawn an enemy there
        RaycastHit hit;
        if (currentNumOfEnemies < maxNumOfEnemies && Vector3.Distance(eSpawner[randomSpawnPos].transform.position, player.transform.position) < maxSpawnDistance)
        {
            if (Physics.Raycast(eSpawner[randomSpawnPos].transform.position, (player.transform.position - eSpawner[randomSpawnPos].transform.position), out hit, maxSpawnDistance))
            {
                if (!hit.collider.CompareTag("Player") && !hit.collider.CompareTag("Enemy") && !hit.collider.CompareTag("FakeEnemy"))
                {
                    clone = Instantiate(enemyToSpawn, eSpawner[randomSpawnPos].transform.position, eSpawner[randomSpawnPos].transform.rotation);
                    clone.SetActive(true);
                    clone.GetComponent<Animator>().enabled = true;
                }
            }
        }
        
    }
}
