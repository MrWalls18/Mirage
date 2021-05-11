using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeBlockade : MonoBehaviour
{

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            this.gameObject.SetActive(true);
            this.gameObject.GetComponent<BoxCollider>().isTrigger = false;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !this.CompareTag("EntryCaveIn"))
        {
            this.gameObject.SetActive(false);
        }
    }

}
