using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : EnemyBase_FSM
{
    //if enemy exists for 30 seconds and doesn't detect the player, destroy the coyote
    //move around the scene so it looks more natural
    private EnemyAI reference;
    //public GameObject dummy;
    

    public NavMeshAgent agent;
    public Animator anim;
    //[HideInInspector] public NavMeshAgent agent;

    public bool playerInSight;
    public float sightRange;
    public float minAgroRange = 20f;

    float timeElapsed;
    bool isStalking = false;
    public float currentTime;

    public float distanceToPlayer;

    public Vector3 walkTo;
    bool walkToSet;
    public float walkToRange = 30f;

    private void Start()
    {
        //enemy = reference.agent.gameObject;
        //reference = scriptRef.GetComponent<EnemyAI>();
        //anim = enemy.GetComponent<Animator>();
        
        //reference = transform.parent.GetComponent<EnemyAI>();
        //dummy = reference.GetComponent<EnemyAI>();
        
        //scriptRef = FindObjectOfType<EnemyAI>();
        
        //reference = GetComponent<EnemyAI>();
        //distanceToPlayer = Vector3.Distance(enemy.transform.position, player.transform.position);
    }

    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        reference = animator.GetComponent<EnemyAI>();
        //reference = GetComponent<EnemyAI>();
        //agent = GetComponent<NavMeshAgent>();

        //reference.GetComponent<EnemyAI>().Patrol();
        distanceToPlayer = Vector3.Distance(enemy.transform.position, player.transform.position);

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        //patrol needs to be called every frame otherwise it will only find one point
        reference.Patrol();

        if (distanceToPlayer < minAgroRange)
        {

            isStalking = true;
            animator.SetBool("isPlayerInMinAgroRange", true);
        }
        else if (distanceToPlayer > minAgroRange)
        {
            animator.SetBool("isIdleTimeOver", false);
        }
    }
}
