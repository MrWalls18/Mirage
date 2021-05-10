using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoyoteJumpscare : MonoBehaviour
{
    public GameObject blink;
    public GameObject jumpScare;

    IEnumerator Blink()
    {
        blink.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        PlayerMovement.Instance.walkingSpeed = PlayerMovement.Instance.defaultSpeed;
        blink.SetActive(false);
        jumpScare.SetActive(true);

    }
    IEnumerator TimeToDestroy()
    {
        yield return new WaitForSeconds(1f);
        Destroy(jumpScare);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (jumpScare != null)
            {
                StartCoroutine(Blink());
                StartCoroutine(TimeToDestroy());
            }
        }
    }



}
