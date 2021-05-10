using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPickUp : MonoBehaviour
{
    [SerializeField] private bool isCompass;
    [SerializeField] private bool isKeys;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (isCompass)
            {
                Compass.Instance.hasCompass = true;
                InventoryUI.Instance.setIndex(2);
            }
            if (isKeys)
            {
                InventoryUI.Instance.hasKeys = true;
                InventoryUI.Instance.setIndex(3);
            }
            Destroy(gameObject);
        }
    }


}
