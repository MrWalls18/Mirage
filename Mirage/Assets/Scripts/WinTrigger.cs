using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    public int winPanel = 1;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Inside win trigger");

        if (other.tag == "Player")
        {
            Cursor.lockState = CursorLockMode.None;
            Debug.Log("it is player");
            MenuUI.Instance.OpenPanel(winPanel);
            MenuUI.Instance.canPause = false;
        }
    }
}
