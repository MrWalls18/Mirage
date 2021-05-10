using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoyoteJumpscare : MonoBehaviour
{
    public GameObject blink;
    public GameObject jumpScare;
    private GameObject jumpScareClone;
    public Transform spawnPos;
    private bool didPlay = false;

    public float spawnDistance;

    IEnumerator Blink()
    {
        blink.SetActive(true);
        PlayerMovement.Instance.walkingSpeed = 0;
        yield return new WaitForSeconds(0.5f);
        PlayerMovement.Instance.walkingSpeed = PlayerMovement.Instance.defaultSpeed;
        blink.SetActive(false);
        didPlay = true;

        SpawnJumpscare();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(Blink());
        }
    }

    void SpawnJumpscare()
    {
        //spawnPos = PlayerStats.Instance.transform.position + PlayerStats.Instance.transform.forward * spawnDistance;
        jumpScareClone = Instantiate(jumpScare, spawnPos.position, spawnPos.rotation);
       // jumpScareClone.GetComponent<Animator>().enabled = true;
    }


}
