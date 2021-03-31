using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptedHallucination : MonoBehaviour
{
    [SerializeField] private GameObject coyotes
        ;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            coyotes.SetActive(true);
        }
    }
}
