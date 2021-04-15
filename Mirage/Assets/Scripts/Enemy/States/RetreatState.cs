using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RetreatState : StateMachineBehaviour
{
    //if hit by rock, run from player for a time
    EnemyAI enemy;

    float timeElapsed;
    public float retreatTime = 5f;
    public float retreatStartTime { get; private set; }

    public float retreatDistance = 10f;

    bool isRetreating = true;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.GetComponent<EnemyAI>();
        retreatStartTime = Time.time;
        Retreat();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //enemy.agent.SetDestination(enemy.retreat.transform.position);
        //after a certain amount of time, we want the AI to follow the player again
        timeElapsed += Time.time;
        if (timeElapsed >= retreatTime && isRetreating)
        {
            Debug.Log("I'm waiting to stalk again");
            isRetreating = false;
            animator.SetBool("hitByRock", false);
            timeElapsed = 0;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    public void Retreat()
    {
        Debug.Log("I'm retreating from the function");
        //Debug.Log("I'm going in this direction first " + retreatDirection);
        Vector3 retreatDirection = enemy.transform.forward * -1 * retreatDistance;
        Debug.Log("I'm going in this direction " + retreatDirection);
        Vector3 firstDestination = enemy.transform.position + retreatDirection;

        SetPath(firstDestination);
    }

    private void SetPath(Vector3 destination)
    {
        NavMeshHit hit;
        bool navMeshFound = NavMesh.SamplePosition(destination, out hit, 1.0f, NavMesh.AllAreas);

        if(navMeshFound == true)
        {
            NavMeshPath path = new NavMeshPath();
            enemy.agent.CalculatePath(hit.position, path);
            if(path.status != NavMeshPathStatus.PathInvalid)
            {
                enemy.agent.SetDestination(hit.position);
            }
        }
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
}
