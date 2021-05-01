using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text maxStamina, stamina, sanity, coinFlip;

    public PlayerStats myStats;

    [SerializeField] private GameObject coyoteLooming;

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

        if ((myStats.sanity / myStats.maxSanity) < 0.8f)
        {
            coyoteLooming.SetActive(true);
        }

    }

    public void DestroyCarKeySpawns()
    {
        GameObject keySpawnManager = GameObject.Find("CarKeySpawnManager");

        Destroy(keySpawnManager);
    }
}
