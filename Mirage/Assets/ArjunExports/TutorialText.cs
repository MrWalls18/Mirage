using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class TutorialText : MonoBehaviour
{
    public string tutorialText;
    public TMP_Text textBox;

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("1");
        if (other.CompareTag("Player"))
        {
            Think(tutorialText);
            Debug.Log("2");
        }
    }


    public void Think(string text)
    {
        textBox.text = text;
    }

}
