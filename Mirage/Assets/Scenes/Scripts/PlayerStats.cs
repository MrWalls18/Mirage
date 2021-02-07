using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    public float sprintSpeedMultiplier;
    public float sanityDepletionRate;

    [SerializeField] private float sprintTimer;
    [SerializeField] private PlayerMovement movePlayer;

    [HideInInspector] public float sanity;
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
       maxStamina = 10f;
        minStamina = 5f;
        hallucinationTimer = 120f;

        stamina = maxStamina;
        runSpeed = movePlayer.speed * sprintSpeedMultiplier;

    }
    private void Update()
    {
        CalculateSanity();

        Sprint();


        Debug.Log(stamina);
    }

    private void CalculateSanity()
    {
        sanity -= Time.deltaTime * sanityDepletionRate;

        CalculateMaxStamina();
    }

    private void CalculateMaxStamina()
    {
        maxStamina = sanity / 2;

        if (stamina > maxStamina)
        {
            stamina = maxStamina;
        }
    }


    public void Sprint()
    {
        if (isRunning)
        {
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

    
    public void ResetSprint(float originalSpeed)
    {
        movePlayer.speed = originalSpeed;
    }
}

