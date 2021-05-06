using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowlHallucination : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered Howl");
        if (other.CompareTag("Player"))
        {
            Debug.Log("Should play sound");
            AudioManager.instance.Play("Coyote_howl_day");
        }
    }
}
