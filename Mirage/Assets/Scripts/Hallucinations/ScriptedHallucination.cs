using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptedHallucination : MonoBehaviour
{
    [SerializeField] private GameObject hallucination
        ;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            hallucination.SetActive(true);
        }
    }
}
