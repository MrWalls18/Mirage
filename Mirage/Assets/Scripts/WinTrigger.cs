using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WinTrigger : MonoBehaviour
{
    public GameObject winPanel;

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
            Cursor.lockState = CursorLockMode.None;
            EndGameUI.Instance.WinGame();
        }
    }
}
