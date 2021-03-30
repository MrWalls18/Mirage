using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Text maxStamina, stamina, sanity, coinFlip;

    public PlayerStats myStats;

    bool isGameOver = false;

    private void Update()
    {
        
        maxStamina.text = myStats.maxStamina.ToString();
        stamina.text = myStats.stamina.ToString();
        sanity.text = myStats.sanity.ToString();

        

    }
    
    public void EndGame()
    {
        if (isGameOver == false)
        {
            isGameOver = true;
            SceneManager.LoadScene("GameOver");
        }
    }
}
