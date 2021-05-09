using System.Collections;
using System.Collections.Generic;
using TMPro;
//using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : SingletonPattern<PlayerStats>
{

    public float sprintSpeedMultiplier;
    public float sanityDepletionRate;

    [SerializeField] private float sprintTimer;
    [SerializeField] private PlayerMovement movePlayer;
    [SerializeField] private SamplePostion coyoteAmbush;

    private float sanityPercent;
    public float SanityPercent { get { return sanityPercent; } }
    [HideInInspector] public float sanity;
    [HideInInspector] public float maxSanity;
    [HideInInspector] public float stamina;
    [HideInInspector] public float maxStamina;
    private float minStamina;
    [SerializeField] private float hallucinationTimer;
    private float hallucinationCountdown;

    private float runSpeed;

    private float timer;

    [HideInInspector] public bool isRunning = false;
    public bool isHallucinating;

    [HideInInspector] public int bonesFound = 0;

    //Predetermines variables
    protected override void Awake()
    {
        base.Awake();

        isHallucinating = false;
        sanity = 20f;
        maxSanity = sanity;
        maxStamina = 10f;
        minStamina = 5f;
        sanityPercent = 100f;

        bonesFound = 0;

        stamina = maxStamina;
        runSpeed = movePlayer.walkingSpeed * sprintSpeedMultiplier;

        hallucinationCountdown = hallucinationTimer;

    }
    private void Update()
    {
        CalculateSanity(sanityDepletionRate);

        Sprint();


        //Hallucination timer
        hallucinationCountdown -= Time.deltaTime;

        //Checks if the player will be hallucinating
        //after timer hits zero
        if (hallucinationCountdown <= 0)
        {
            Hallucination();
            hallucinationCountdown = hallucinationTimer;
        }

    }

    //Deteriorates sanity and a certain speed
    private void CalculateSanity(float depletionSpeed)
    {
        sanity -= Time.deltaTime * depletionSpeed;

        if (sanity < 1f)
        {
            sanity = 1f;
        }

        CalculateMaxStamina();
    }

    //Takes sanity into account to calculate for max stamina
    private void CalculateMaxStamina()
    {
        maxStamina = sanity / 2;

        if (maxStamina < minStamina )
        {
            maxStamina = minStamina;
        }

        if (stamina > maxStamina)
        {
            stamina = maxStamina;
        }

        
    }

   
    public void Sprint()
    {
       
        if (isRunning && PlayerMovement.Instance.isGrounded)
        {
            //Sanity depletes faster when sprinting
            CalculateSanity(sanityDepletionRate);
            movePlayer.walkingSpeed = runSpeed;

            //Stamina deplete as you are running
            stamina -= Time.deltaTime;
            if (stamina < 0)
            {
                stamina = 0;
                isRunning = false;
               movePlayer.ResetSpeed();
            }
        }

        //Regenerates stamina if player isn't running
        else if (stamina < maxStamina)
        {
            movePlayer.ResetSpeed();
            stamina += Time.deltaTime / 2f;
        }

        //If player isn't running and they are at maxStamina
        //They revert back to their walking speed
        else
        {
            movePlayer.ResetSpeed();
        }
    }

    public Text waterText;
    public Text waterSourceText;
    //Adds to the players sanity
    public void DrinkWater(float waterPoints, string waterName)
    {
        sanity += waterPoints;

        waterText.text = waterPoints.ToString();
        waterSourceText.text = waterName;
        if (sanity > maxSanity)
        {
            sanity = maxSanity;
        }

        stamina += waterPoints;

    }

    //Checks to see if player will be hallucinating
    private void Hallucination()
    {
        float sanityPercentage;

        sanityPercentage = (sanity / maxSanity) * 100;

        sanityPercent = sanityPercentage;

        //The lower the sanity percentage,
        //the higher chance of the player experiencing a hallucination
        if (sanityPercentage < 25f)
        {
            //80% chance of hallucinating
            //CoyoteAmbush hallucination occurs if hallucinating
            if (Random.value > 0.2f)
            {
                isHallucinating = true;
                if (!coyoteAmbush.wasTriggered)
                {
                    coyoteAmbush.enabled = true;
                }
            }
            else
                isHallucinating = false;
            //Fake enemy spawner now take 1/4 of the original time to spawn 
            FakeEnemySpawner.Instance.ChangeFakeEnemySpawnRate(FakeEnemySpawner.Instance.timeBetweenSpawns / 4f);
        }
        else if (sanityPercentage < 50f)
        {
            //70% chance of hallucinating
            if (Random.value > 0.3)
                isHallucinating = true;            
            else
                isHallucinating = false;

            //Fake enemy spawner now take 1/3 of the original time to spawn 
            FakeEnemySpawner.Instance.ChangeFakeEnemySpawnRate(FakeEnemySpawner.Instance.timeBetweenSpawns / 3f);
        }
        else if (sanityPercentage < 75f)
        {
            //60% chance at hallucinating
            if (Random.value > 0.4)
                isHallucinating = true;
            else
                isHallucinating = false;

            //Fake enemy spawner now take 1/2 of the original time to spawn 
            FakeEnemySpawner.Instance.ChangeFakeEnemySpawnRate(FakeEnemySpawner.Instance.timeBetweenSpawns / 2f);
        }
        else
        {
            //50% chance at hallucinating
            if (Random.value > 0.5)
                isHallucinating = true;
            else
                isHallucinating = false;

            //Fake enemy spawner is set to the original time to spawn 
            FakeEnemySpawner.Instance.ChangeFakeEnemySpawnRate(FakeEnemySpawner.Instance.timeBetweenSpawns);
        }

    }
}

