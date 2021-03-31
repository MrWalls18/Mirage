using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptedHallucination : MonoBehaviour
{
    [SerializeField] private GameObject scriptedEnemies, boulderBlockade;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            scriptedEnemies.SetActive(true);
        }
    }
}
