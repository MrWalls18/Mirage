using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase_FSM : StateMachineBehaviour
{
    public GameObject enemy;
    public GameObject player;
    public float speed = 5f;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        enemy = animator.gameObject;
        //find the player in the scene
        player = enemy.GetComponent<EnemyAI>().GetPlayer();
    }
}
