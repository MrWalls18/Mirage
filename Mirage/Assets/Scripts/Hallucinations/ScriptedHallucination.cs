using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptedHallucination : MonoBehaviour
{
    [SerializeField] private GameObject hallucination, realObject;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            hallucination.SetActive(false);
            realObject.SetActive(true);
        }
    }
}
