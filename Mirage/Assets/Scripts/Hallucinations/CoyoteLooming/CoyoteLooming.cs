using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class CoyoteLooming : MonoBehaviour
{
    [SerializeField] SamplePostion getCoyotePosition;
    public float rangeToSpawn;
    public Transform player;
    [SerializeField] private GameObject fakeEnemyPrefab;
    private GameObject fakeEnemyClone;

    private bool isSpawned = false;

    // Update is called once per frame
    void Update()
    {
        Vector3 coyotePosition;

        if(!isSpawned && getCoyotePosition.RandomPoint(player.position, rangeToSpawn, out coyotePosition))
        {
            if (Vector3.Distance(player.position, coyotePosition) > rangeToSpawn - 5)
            {
                fakeEnemyClone = Instantiate(fakeEnemyPrefab, coyotePosition, transform.rotation);

                isSpawned = true;
                this.gameObject.SetActive(false);
            }
        }
    }
}
