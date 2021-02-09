using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text maxStamina, stamina, sanity;

    public PlayerStats myStats;

    private void Update()
    {
        maxStamina.text = myStats.maxStamina.ToString();
        stamina.text = myStats.stamina.ToString();
        sanity.text = myStats.sanity.ToString();
    }
}
