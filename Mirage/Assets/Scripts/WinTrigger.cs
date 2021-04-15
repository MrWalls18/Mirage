using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WinTrigger : MonoBehaviour
{
    public GameObject winPanel;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Inside win trigger");

        if (other.tag == "Player")
        {
            Cursor.lockState = CursorLockMode.None;
            Debug.Log("it is player");
            winPanel.SetActive(true);
           // MenuUI.Instance.OpenPanel(winPanel);
           // MenuUI.Instance.canPause = false;
        }
    }
}
