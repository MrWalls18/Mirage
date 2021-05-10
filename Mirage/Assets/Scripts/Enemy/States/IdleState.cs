using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : StateMachineBehaviour
{
    //control the idle time of the enemies
    //EnemyAI enemy;

    public float minIdleTime = 3f;
    public float maxIdleTime = 5f;
    public float startTime { get; protected set; }

    protected float idleTime = 3f;
    public float timeElapsed;
    

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        startTime = Time.deltaTime;
        //timeElapsed += Time.deltaTime;
        animator.SetBool("isIdleTimeOver", false);
        
        SetRandomIdleTime();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {   
        
        timeElapsed += Time.deltaTime;

        if (timeElapsed >= startTime + idleTime)
        {
            animator.SetBool("isIdleTimeOver", true);
            timeElapsed = 0;
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

}
