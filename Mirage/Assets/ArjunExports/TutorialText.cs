using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class TutorialText : MonoBehaviour
{
    public string tutorialText;
    public TMP_Text textBox;
    public float timer = 5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            textBox.gameObject.SetActive(true);
            timer = 5f;
            Think(tutorialText);
        }
    }

    /*private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            timer = 0f;
            if (textBox.gameObject.activeInHierarchy)
            {
                textBox.gameObject.SetActive(false);
            }
        }
    }*/

    private void Update()
    {
        if (timer >= 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            if (textBox.gameObject.activeInHierarchy)
            {
                textBox.gameObject.SetActive(false);
            }
        }
    }


    public void Think(string text)
    {
        textBox.text = text;
    }



}