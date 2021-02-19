using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    [SerializeField] private float hallucinationTimer;
    private float hallucinationCountdown;

    private float runSpeed;

    private float timer;

    [HideInInspector] public bool isRunning = false;
    public bool isHallucinating;

    private void Awake()
    {
        isHallucinating = false;
        sanity = 20f;
        maxSanity = sanity;
        maxStamina = 10f;
        minStamina = 5f;
       // hallucinationTimer = 120f;

        stamina = maxStamina;
        runSpeed = movePlayer.moveSpeed * sprintSpeedMultiplier;

        hallucinationCountdown = hallucinationTimer;

        

    }
    private void Update()
    {
        CalculateSanity(sanityDepletionRate);

        Sprint();



        hallucinationCountdown -= Time.deltaTime;

        if (hallucinationCountdown <= 0)
        {
            Hallucination();
            hallucinationCountdown = hallucinationTimer;
        }

        //Debug.Log(stamina);
    }

    //Deteriorates sanity and a certain speed
    private void CalculateSanity(float depletionSpeed)
    {
        sanity -= Time.deltaTime * depletionSpeed;

        CalculateMaxStamina();
    }

    //Takes sanity into account to calculate for max stamina
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
            movePlayer.moveSpeed = runSpeed;

            stamina -= Time.deltaTime;
            if (stamina < 0)
            {
                stamina = 0;
                isRunning = false;
                movePlayer.moveSpeed = movePlayer.defaultSpeed;
            }
        }

        else if (stamina < maxStamina)
        {
            movePlayer.moveSpeed = movePlayer.defaultSpeed;
            stamina += Time.deltaTime / 2f;
        }

        else
        {
            movePlayer.moveSpeed = movePlayer.defaultSpeed;
        }
    }


    //Adds to the players sanity
    public void DrinkWater()
    {
        sanity += 5f;

        if (sanity > maxSanity)
        {
            sanity = maxSanity;
        }

    }


    private void Hallucination()
    {
        float sanityPercentage;

        sanityPercentage = (sanity / maxSanity) * 100;

        if (sanityPercentage < 25f)
        {
            if (Random.value > 0.2f)
                isHallucinating = true;
            else
                isHallucinating = false;
        }
        else if (sanityPercentage < 50f)
        {
            if (Random.value > 0.3)
                isHallucinating = true;
            else
                isHallucinating = false;
        }
        else if (sanityPercentage < 75f)
        {
            if (Random.value > 0.4)
                isHallucinating = true;
            else
                isHallucinating = false;
        }
        else
        {
            if (Random.value > 0.5)
                isHallucinating = true;
            else
                isHallucinating = false;
        }

       // Debug.Log(sanityPercentage);
    }
}

