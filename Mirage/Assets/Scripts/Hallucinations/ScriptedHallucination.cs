using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptedHallucination : MonoBehaviour
{
    [SerializeField] private GameObject disappearingObject, appearingObject;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (disappearingObject != null)
                disappearingObject.SetActive(false);

            if (appearingObject != null)
                appearingObject.SetActive(true);
        }
    }
}
