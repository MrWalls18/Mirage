using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> eSpawner;
    [SerializeField]private GameObject player;
    [SerializeField] private float minSpawnDistance;
    [SerializeField] private float maxSpawnDistance;
    [SerializeField] private float eSpawnerTimer;

    [SerializeField] private GameObject enemyPrefab;
    private GameObject enemyClone;
    private int randomSpawnPos;
    private SpriteRenderer s_renderer;

    private bool didEnemySpawn = false;

    void Awake()
    {
        eSpawner = new List<GameObject>();

        eSpawner.AddRange(GameObject.FindGameObjectsWithTag("eSpawner"));
    }

    void Start()
    {
        InvokeRepeating("SetSpawnPoint", 5f, eSpawnerTimer);
    }

    void SetSpawnPoint()
    {
        while (!didEnemySpawn)
        {
            randomSpawnPos = UnityEngine.Random.Range(0, eSpawner.Count);

            s_renderer = eSpawner[randomSpawnPos].GetComponent<SpriteRenderer>();

            if (Vector3.Distance(player.transform.position, eSpawner[randomSpawnPos].transform.position) > minSpawnDistance &&
                    Vector3.Distance(player.transform.position, eSpawner[randomSpawnPos].transform.position) < maxSpawnDistance &&
                        !s_renderer.isVisible)
            {
                enemyClone = Instantiate(enemyPrefab, eSpawner[randomSpawnPos].transform.position, eSpawner[randomSpawnPos].transform.rotation);
                break;
            }
        }
    }
}
