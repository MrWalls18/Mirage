using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Audio;

public class StalkState : StateMachineBehaviour
{
    //once player is detected, start following them
    EnemyAI enemy;
    AudioManager manager;

    public bool playerInAttackRange;
    public bool playerInSight;

    public float sightRange;
    public float increaseSpeedInterval = 10f;
    public float enterStalkTime;

    private float distFromPlayer;
    public float minAttackRange = 20f;

    public bool playerStopped = true;

    private RaycastHit hit;


    //bool for when enemy gets hit by rock
    //public bool hasHitRock = false;

    private void Start()
    {
        //manager = FindObjectOfType<AudioManager>();
        //audiosource = enemy.GetComponent<AudioSource>();
        //FindObjectOfType<AudioManager>().Play("Coyote_running");
        //AudioManager.Instance.Play("Coyote_running");
    }

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        enemy = animator.GetComponent<EnemyAI>();

        //plays stalk audio
        AudioManager.Instance.Play("Coyote_running");

        //distFromPlayer = Vector3.Distance(enemy.transform.position, enemy.player.transform.position);

        playerInSight = true;

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //create variable for min follow distance, set to like 15,
        //if player moves within 15 units, min follow distance gets updated to 
        //that new value
        //if player stops moving, start a timer, and after 5 seconds start creeping closer

        distFromPlayer = Vector3.Distance(enemy.transform.position, enemy.player.transform.position);

        if (playerInSight)
        {
            Debug.Log("Player speed is " + PlayerMovement.Instance.walkingSpeed);
            StalkPlayer();
            if (distFromPlayer < minAttackRange)
            {
                //kill the player
                //play from the coyote itself, so need to change the call to the audio source and not the audio
                AudioManager.Instance.Play("Coyote_growl");

                if (distFromPlayer < enemy.attackRange)
                    animator.SetBool("isPlayerInMinAttackRange", true);
            }

            //enemy.agent.SetDestination(enemy.player.transform.position);
            //enemy.speed += enemy.speedIncrement;
            enterStalkTime = Time.deltaTime + increaseSpeedInterval;
        }

        //if the player gets too far away, return to patrol, and howl
        if (distFromPlayer > 160f)
        {
            //check if it's day time
            if (EnemySpawner.Instance.timeRemaining < 700f)
            {
                playerInSight = false;
                animator.SetBool("isPlayerInMinAgroRange", false);
                AudioManager.Instance.Play("Coyote_howl_day");
            }
            else if(EnemySpawner.Instance.timeRemaining > 700f) 
            {
                //go to patrol and howl night time
                playerInSight = false;
                animator.SetBool("isPlayerInMinAgroRange", false);
                
                AudioManager.Instance.Play("Coyote_howl_night");
                
            }

        }
        //this will transition the coyote to the retreatState
        if (enemy.hasHitRock)
        {
            enemy.agent.SetDestination(enemy.transform.position);
            enemy.agent.isStopped = true;
            enemy.agent.ResetPath();

            animator.SetBool("hitByRock", true);

            //enemy.hasHitRock = false;
        }

        /*if (hit.distance < 40f)
            enemy.agent.isStopped = true;
        else
            enemy.agent.isStopped = false;*/
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    private void StalkPlayer()
    {
        //simply targets and moves to player
        //set coyote speed to player speed

        //enemy.agent.velocity.x = PlayerMovement.Instance.walkingSpeed;
        enemy.agent.speed = PlayerMovement.Instance.walkingSpeed;
        Debug.Log("my speed is " + enemy.agent.speed);

        //TODO: modify this behaviour so it's more believable
        enemy.agent.SetDestination(enemy.player.transform.position);
        /*if (enemy.hasHitRock)
        {
            enemy.agent.isStopped = true;
            enemy.animator.SetBool("hitByRock", true);
            Debug.Log("I'm in the StalkPlayer() if statement");
        }*/

        #region hitRock function
        /*if (!enemy.hasHitRock)
        {
            //if player stops, start timer
            if (playerStopped)
            {
                enterStalkTime += Time.deltaTime;
                if (enterStalkTime > 30)
                {
                    //start creeping towards player
                }
            }
            else if (!playerStopped)
            {
                //update min distance to player if needed
                //set speed back to player speed
            }
        }*/
        #endregion
    }

    /*void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Rock"))
        {
            enemy.hasHitRock = true;
        }
    }*?

    /*void OnTriggerEnter(Collider collider)
    {
        //flip the bool if Rock hits coyote
        if (collider.gameObject.CompareTag("Rock"))
        {
            hasHitRock = true; 
        }
    }*/
}
