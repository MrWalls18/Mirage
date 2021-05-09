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
            yield return new WaitForSeconds(timer);

            EnemySpawner.Instance.SetSpawnPoint(fakeEnemyPrefab, fakeEnemyClone);
        }
    }
}
