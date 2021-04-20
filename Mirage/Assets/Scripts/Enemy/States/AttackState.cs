using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class AttackState : StateMachineBehaviour
{
    //kill the player
    EnemyAI enemy;

    public bool playerInAttackRange;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Debug.Log("I'm in the attack state already");
        animator.SetBool("isPlayerInMinAttackRange", true);

        //player dies, trigger death screen
        MenuUI.Instance.OpenPanel(0);
        //Destroy(enemy.gameObject);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    
}
