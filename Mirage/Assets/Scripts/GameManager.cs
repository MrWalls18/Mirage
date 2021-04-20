using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text maxStamina, stamina, sanity, coinFlip;

    public PlayerStats myStats;

    private bool hasKey;
    public bool HasKey
    {
        get
        {
            return hasKey;
        }
        set
        {
            hasKey = value;
        }
    }

    private void Update()
    {
        
        maxStamina.text = myStats.maxStamina.ToString();
        stamina.text = myStats.stamina.ToString();
        sanity.text = myStats.sanity.ToString();

        

    }

    public void DestroyCarKeySpawns()
    {
        GameObject keySpawnManager = GameObject.Find("CarKeySpawnManager");

        Destroy(keySpawnManager);
    }
}
