using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEnemyBlockage : MonoBehaviour
{
    [SerializeField] private GameObject scriptedEnemies;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("TriggerEntered");
        if (other.tag == "Player")
        {
            scriptedEnemies.SetActive(false);
        }
    }
}
