using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : StateMachineBehaviour
{
    //if enemy exists for 30 seconds and doesn't detect the player, destroy the coyote
    //move around the scene so it looks more natural
    public EnemyAI reference;
    public GameObject enemy;
    public GameObject player;
    public Animator anim;

    public bool playerInSight;
    public float sightRange;
    public float minAgroRange = 20f;

    public float minSpeed = 3;
    public float maxSpeed = 6;
    public float currentTime;

    public float distanceToPlayer;

    public Vector3 walkTo;
    bool walkToSet;
    public float walkToRange = 10f;

    private void Awake()
    {
        anim = enemy.GetComponent<Animator>();
        
        distanceToPlayer = Vector3.Distance(enemy.transform.position, player.transform.position);
        //playerInSight = Physics.CheckSphere(reference.transform.position, sightRange, reference.whatIsPlayer);
    }


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("I'm in the enter function of Patrol state");
        
        Patrol();
        
        
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        distanceToPlayer = Vector3.Distance(enemy.transform.position, player.transform.position);

        if (distanceToPlayer < minAgroRange)
        {
            Debug.Log("I see you");
            animator.SetBool("isPlayerInMinAgroRange", true);
        }
        else if (distanceToPlayer > minAgroRange)
        {
            //TODO: double check this logic
            //Patrol();
            Debug.Log("I pretend not to see");
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
        Debug.Log("I'm on patrol");
        if (!walkToSet) SearchWalkPoint();

        if (walkToSet)
        {
            reference.agent.SetDestination(walkTo);
        }

        Vector3 distanceToWalkTo = enemy.transform.position - walkTo;

        if (distanceToWalkTo.magnitude < 1f)
        {
            walkToSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        Debug.Log("I'm looking for a walk point");
        //calculate random point in your range
        float randomZ = Random.Range(-walkToRange, walkToRange);
        float randomX = Random.Range(-walkToRange, walkToRange);

        walkTo = new Vector3(enemy.transform.position.x + randomX, enemy.transform.position.y, enemy.transform.position.z + randomZ);
        //
        if (Physics.Raycast(walkTo, -enemy.transform.up, 2f, reference.whatIsGround))
        {
            walkToSet = true;
        }
    }
}
