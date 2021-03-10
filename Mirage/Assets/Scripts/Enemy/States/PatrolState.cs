using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : StateMachineBehaviour
{
    //move around the scene so it looks more natural
    EnemyAI enemy;

    public bool playerInSight;
    public float sightRange;

    public Vector3 walkTo;
    bool walkToSet;
    public float walkToRange = 10f;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerInSight = Physics.CheckSphere(enemy.transform.position, sightRange, enemy.whatIsPlayer);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (playerInSight)
        {
            animator.SetBool("isPlayerInMinAgroRange", true);
        }
        else if (!playerInSight)
        {
            //TODO: double check this logic
            Patrol();
            animator.SetBool("isIdleTimeOver", false);
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

    private void Patrol()
    {
        if (!walkToSet) SearchWalkPoint();

        if (walkToSet)
        {
            enemy.agent.SetDestination(walkTo);
        }

        Vector3 distanceToWalkTo = enemy.transform.position - walkTo;

        if (distanceToWalkTo.magnitude < 1f)
        {
            walkToSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        //calculate random point in your range
        float randomZ = Random.Range(-walkToRange, walkToRange);
        float randomX = Random.Range(-walkToRange, walkToRange);

        walkTo = new Vector3(enemy.transform.position.x + randomX, enemy.transform.position.y, enemy.transform.position.z + randomZ);

        if (Physics.Raycast(walkTo, -enemy.transform.up, 2f, enemy.whatIsGround))
        {
            walkToSet = true;
        }
    }
}
