using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase_FSM : StateMachineBehaviour
{
    //this script was meant to contain all of the variables that were
    //used by all the statemachinebehaviour scripts, currently that
    //is the EnemyAI script however, this script may not be necessary
    public GameObject enemyObject;
    public GameObject player;
    public float speed = 20f;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        enemyObject = animator.gameObject;
        //find the player in the scene
        player = enemyObject.GetComponent<EnemyAI>().GetPlayer();
    }
}
