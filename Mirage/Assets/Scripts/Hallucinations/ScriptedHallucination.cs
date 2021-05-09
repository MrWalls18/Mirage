using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptedHallucination : MonoBehaviour
{
    private bool playedHallucination;

    [SerializeField] private GameObject disappearingObject, appearingObject;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !playedHallucination)
        {
            if (disappearingObject != null)
                disappearingObject.SetActive(false);

            if (appearingObject != null)
                appearingObject.SetActive(true);

            playedHallucination = true;
        }
    }
}
