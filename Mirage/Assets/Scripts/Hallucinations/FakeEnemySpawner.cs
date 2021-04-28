using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeEnemySpawner : SingletonPattern<FakeEnemySpawner>
{
   // public List<GameObject> eSpawner;
    [SerializeField] private GameObject fakeEnemyPrefab;
    private GameObject fakeEnemyClone;
    public int timeBetweenSpawns;

    // Start is called before the first frame update
    void Start()
    {
        // eSpawner = new List<GameObject>();


        //  eSpawner.AddRange(GameObject.FindGameObjectsWithTag("eSpawner"));

        StartCoroutine("SpawnFakeEnemy", timeBetweenSpawns);
    }

   

    public void ChangeFakeEnemySpawnRate(float timer)
    {
        StopCoroutine("SpawnFakeEnemy");

        StartCoroutine("SpawnFakeEnemy", timer);
    }

    IEnumerator SpawnFakeEnemy(float timer)
    {
        while (true)
        {
           // Debug.Log("Sanity: " + PlayerStats.Instance.SanityPercent);
            Debug.Log("Timer: " + timer);
            yield return new WaitForSeconds(timer);

            EnemySpawner.Instance.SetSpawnPoint(fakeEnemyPrefab, fakeEnemyClone);
        }
    }
}
