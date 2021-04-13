using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveIn : MonoBehaviour
{
    [SerializeField] private GameObject rockPrefab, boulderPrefab;
    private GameObject rockClone, boulderClone;


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(SpawnRocks());
        }
    }

    IEnumerator SpawnRocks()
    {
        rockClone = Instantiate(rockPrefab, transform.position, transform.rotation);
        yield return new WaitForSeconds(0.5f);
    }

}
