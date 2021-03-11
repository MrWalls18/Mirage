using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> eSpawner;
    [SerializeField] private GameObject player;
    [SerializeField] private float minSpawnDistance;
    [SerializeField] private float maxSpawnDistance;
    [SerializeField] private float eSpawnerTimer;

    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject hallucinatedEnemyPrefab;
    private GameObject enemyClone;
    private int randomSpawnPos;
    private MeshRenderer s_renderer;

   // private bool didEnemySpawn = false;

    void Awake()
    {
        eSpawner = new List<GameObject>();

        eSpawner.AddRange(GameObject.FindGameObjectsWithTag("eSpawner"));

    }

    void Start()
    {
        //will spawn enemies every x amount of seconds
        InvokeRepeating("SetSpawnPoint", 5f, eSpawnerTimer);
    }

    void SetSpawnPoint()
    {
       // while (!didEnemySpawn)
       // {
            //Picks a random spawnPoint from the list of enemy spawn points
            randomSpawnPos = UnityEngine.Random.Range(0, eSpawner.Count);

            s_renderer = eSpawner[randomSpawnPos].GetComponentInChildren<MeshRenderer>();

            //if the player is not too close, not too far,
            //and if the spawn point is out of the camera frame
            if (Vector3.Distance(player.transform.position, eSpawner[randomSpawnPos].transform.position) > minSpawnDistance &&
                    Vector3.Distance(player.transform.position, eSpawner[randomSpawnPos].transform.position) < maxSpawnDistance &&
                        !s_renderer.isVisible)
            {
                //If the player is hallucinating,
                //spawn a "fake" enemy
                if (player.GetComponent<PlayerStats>().isHallucinating)
                {
                    enemyClone = Instantiate(hallucinatedEnemyPrefab, eSpawner[randomSpawnPos].transform.position, eSpawner[randomSpawnPos].transform.rotation);
                    
                }
                //If not, spawn a real enemy
                else
                {
                    enemyClone = Instantiate(enemyPrefab, eSpawner[randomSpawnPos].transform.position, eSpawner[randomSpawnPos].transform.rotation);
                }
                //break;
            }
        //}
    }
}
