using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    public float sprintSpeedMultiplier;
    public float sanityDepletionRate;

    [SerializeField] private float sprintTimer;
    [SerializeField] private PlayerMove movePlayer;

    [HideInInspector] public float sanity;
    private float maxSanity;
    [HideInInspector] public float stamina;
    [HideInInspector] public float maxStamina;
    private float minStamina;
    private float hallucinationTimer;

    private float runSpeed;

    private float timer;

    [HideInInspector] public bool isRunning = false;

    private void Awake()
    {
        sanity = 20f;
        maxSanity = sanity;
       maxStamina = 10f;
        minStamina = 5f;
        hallucinationTimer = 120f;

        stamina = maxStamina;
        runSpeed = movePlayer.speed * sprintSpeedMultiplier;

    }
    private void Update()
    {
        CalculateSanity(sanityDepletionRate);

        Sprint();


        Debug.Log(stamina);
    }

    private void CalculateSanity(float depletionSpeed)
    {
        sanity -= Time.deltaTime * depletionSpeed;

        CalculateMaxStamina();
    }

    private void CalculateMaxStamina()
    {
        maxStamina = sanity / 2;

        if (stamina > maxStamina)
        {
            stamina = maxStamina;
        }

        if (maxStamina < minStamina )
        {
            maxStamina = minStamina;
        }
    }


    public void Sprint()
    {
        if (isRunning)
        {
            CalculateSanity(sanityDepletionRate * 2);
            movePlayer.speed = runSpeed;

            stamina -= Time.deltaTime;
            if (stamina < 0)
            {
                stamina = 0;
                isRunning = false;
                movePlayer.speed = movePlayer.defaultSpeed;
            }
        }

        else if (stamina < maxStamina)
        {
            movePlayer.speed = movePlayer.defaultSpeed;
            stamina += Time.deltaTime / 2f;
        }

        else
        {
            movePlayer.speed = movePlayer.defaultSpeed;
        }
    }


    public void DrinkWater()
    {
        sanity += 5f;

        if (sanity > maxSanity)
        {
            sanity = maxSanity;
        }

    }
}

