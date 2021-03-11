using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : StateMachineBehaviour
{
    //control the idle time of the enemies
    EnemyAI enemy;

    public float minIdleTime = 3f;
    public float maxIdleTime = 5f;
    public float startTime { get; protected set; }

    protected float idleTime;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("I idled here");
        startTime = Time.time;
        animator.SetBool("isIdleTimeOver", false);
        SetRandomIdleTime();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("I went into update");
        if(Time.time >= startTime + idleTime)
        {
            Debug.Log("i entered at" + startTime);
            Debug.Log("idleTime is set to " + idleTime);
            animator.SetBool("isIdleTimeOver", true);
            
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    private void SetRandomIdleTime()
    {
        idleTime = Random.Range(minIdleTime, maxIdleTime);
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
