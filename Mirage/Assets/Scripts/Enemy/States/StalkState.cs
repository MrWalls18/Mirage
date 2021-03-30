using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StalkState : StateMachineBehaviour
{
    //once player is detected, start following them
    EnemyAI enemy;

    public bool playerInAttackRange;
    public bool playerInSight;

    public float sightRange;
    public float increaseSpeedInterval = 10f;
    public float enterStalkTime;

    //bool for when enemy gets hit by rock
    public bool hasHitRock;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerInSight = Physics.CheckSphere(enemy.transform.position, sightRange, enemy.whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(enemy.transform.position, enemy.attackRange, enemy.whatIsPlayer);
        
        //set time coyote enters stalk state
        //may need to rework, player can lose coyote if they leave sight range rn
        enterStalkTime = Time.time;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //have enemy speed match player speed,
        //create variable for min follow distance, set to like 15,
        //if player moves within 15 units, min follow distance gets updated to 
        //that new value
        //if player stops moving, start a timer, and after 5 seconds start creeping closer
        if (playerInSight && !playerInAttackRange)
        {
            StalkPlayer();
            enemy.agent.SetDestination(enemy.player.position);
            enemy.speed += enemy.speedIncrement;
            enterStalkTime = Time.time + increaseSpeedInterval;
        }
        else if (playerInAttackRange)
        {
            animator.SetBool("isPlayerInMinAttackRange", true);
        }
        else if (hasHitRock)
        {
            animator.SetBool("hitByRock", true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}

    private void StalkPlayer()
    {
        //simply targets and moves to player
        //TODO: modify this behaviour so it's more believable
        enemy.agent.SetDestination(enemy.player.position);
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Rock"))
        {
            hasHitRock = true;
        }
    }
}
