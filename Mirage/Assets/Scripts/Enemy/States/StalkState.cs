using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Audio;

public class StalkState : StateMachineBehaviour
{
    //once player is detected, start following them
    EnemyAI enemy;

    public bool playerInAttackRange;
    public bool playerInSight;

    public float sightRange;
    public float increaseSpeedInterval = 10f;
    public float enterStalkTime;

    private float distFromPlayer;
    private float minAttackRange = 3f;

    public bool playerStopped = true;


    //bool for when enemy gets hit by rock
    //public bool hasHitRock = false;

    private void Start()
    {
        //audiosource = enemy.GetComponent<AudioSource>();
    }

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        base.OnStateEnter(animator, stateInfo, layerIndex);
        enemy = animator.GetComponent<EnemyAI>();

        //plays stalk audio
        //AudioManager.Instance.Play("Coyote_running");
        enemy.PlayAudio(0);

        //distFromPlayer = Vector3.Distance(enemy.transform.position, enemy.player.transform.position);

        playerInSight = true;

        //set time coyote enters stalk state
        //may need to rework, player can lose coyote if they leave sight range rn

        //enemy.agent.SetDestination(player.transform.position);

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //create variable for min follow distance, set to like 15,
        //if player moves within 15 units, min follow distance gets updated to 
        //that new value
        //if player stops moving, start a timer, and after 5 seconds start creeping closer
        distFromPlayer = Vector3.Distance(enemy.transform.position, enemy.player.transform.position);
        //if the player gets too far away, return to patrol, and howl
        if (distFromPlayer > 40f && EnemySpawner.Instance.timeRemaining < 700f)
        {
            playerInSight = false;
            animator.SetBool("isPlayerInMinAgroRange", false);
            //AudioManager.Instance.Play("Coyote_howl_day");
            enemy.PlayAudio(1);
        }
        else if(distFromPlayer > 40f && EnemySpawner.Instance.timeRemaining > 700f)
        {
            //go to patrol and howl night time
            playerInSight = false;
            animator.SetBool("isPlayerInMinAgroRange", false);
            //AudioManager.Instance.Play("Coyote_howl_night");
            enemy.PlayAudio(2);
        }
        //play growl audio if you get close enough
        if (distFromPlayer > 10f)
        {
            //AudioManager.Instance.Play("Coyote_growl");
            //enemy.PlayAudio(2);
            enemy.PlayAudio(3);
        }
        if (playerInSight)
        {
            StalkPlayer();
            //Debug.Log("i'm this far away " + distFromPlayer);
            if(distFromPlayer < minAttackRange)
            {
                //kill the player
                animator.SetBool("isPlayerInMinAttackRange", true);
            }
            
            //enemy.agent.SetDestination(enemy.player.transform.position);
            //enemy.speed += enemy.speedIncrement;
            enterStalkTime = Time.deltaTime + increaseSpeedInterval;
        }
        //this will transition the coyote to the retreatState
        if (enemy.hasHitRock)
        {
            enemy.agent.SetDestination(enemy.transform.position);

            animator.SetBool("hitByRock", true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    private void StalkPlayer()
    {
        //simply targets and moves to player

        //set coyote speed to player speed

        //TODO: modify this behaviour so it's more believable
        enemy.agent.SetDestination(enemy.player.transform.position);
        //play footstep audio
        //enemy.PlayAudio(0);
        if (!enemy.hasHitRock)
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
        }
    }

    /*void OnCollisionEnter(Collision collision)
    {
        Debug.Log("i've collided with something");
        if (collision.gameObject.CompareTag("Rock"))
        {
            Debug.Log("i got hit by a rock");
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
