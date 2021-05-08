using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowlHallucination : MonoBehaviour
{
    private bool playedHallucination = false;

    [SerializeField] AudioSource coyoteHowl;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered Howl");
        if (other.CompareTag("Player") && !playedHallucination)
        {
            Debug.Log("Should play sound");
            AudioManager.instance.Play("Coyote_howl_day");
            playedHallucination = true;
           // coyoteHowl.Play();
        }
    }
}
