using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.gameObject.GetComponentInParent<SkillCheckTimer>().hasCoin = true;

            Destroy(this.gameObject);
        }
    }
}
