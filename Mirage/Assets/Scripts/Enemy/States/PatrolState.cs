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
            Debug.Log("I see you");
            isStalking = true;
            animator.SetBool("isPlayerInMinAgroRange", true);
        }
        else if (distanceToPlayer > minAgroRange)
        {
            //TODO: double check this logic
            Debug.Log("I pretend not to see");
            animator.SetBool("isIdleTimeOver", false);

        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    //patrol if you don't detect the player
    /*public void Patrol()
    {
        Debug.Log("I'm on patrol");
        if (!walkToSet) SearchWalkPoint();

        if (walkToSet)
        {
            //set the destination to the walkpoint from searchwalkpoint
            agent.SetDestination(walkTo);
            if (!agent.pathPending)
            {
                //if you've reached your destination do something
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    walkToSet = false;
                    //if the agent doesn't have a path to a point, or they are stopped
                    if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                    {
                        walkToSet = false;
                    }
                }
            }
        }
    }

    //find a walk point for the coyote to go to
    public void SearchWalkPoint()
    {
        Debug.Log("I'm looking for a walk point");
        #region old walkpoint calculation
        //calculate random point in your range
        /*float randomZ = Random.Range(-walkToRange, walkToRange);
        float randomX = Random.Range(-walkToRange, walkToRange);

        walkTo = new Vector3(enemy.transform.position.x + randomX, enemy.transform.position.y, enemy.transform.position.z + randomZ);
        //
        if (Physics.Raycast(walkTo, -enemy.transform.up, 2f, reference.whatIsGround))
        {
            walkToSet = true;
        }
        #endregion
        //find a walk point after you've moved to one already
        walkTo = new Vector3(Random.insideUnitSphere.x * walkToRange, reference.transform.position.y, Random.insideUnitSphere.z * walkToRange);
        walkToSet = true;
        Debug.Log("i'm going to " + walkTo);

        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;

        if (NavMesh.SamplePosition(walkTo, out hit, walkToRange, 1))
        {
            //if walkPoint is on the NavMesh, go to it
            finalPosition = hit.position;
            walkTo = finalPosition;
        }
    }*/

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

    /*private void Patrol()
    {
        Debug.Log("I'm on patrol");
        if (!walkToSet) SearchWalkPoint();

        if (walkToSet)
        {
            //set the destination to the walkpoint from searchwalkpoint
            reference.agent.SetDestination(walkTo);

            if (!reference.agent.pathPending)
            {
                //if you've reached your destination do something
                if (reference.agent.remainingDistance <= reference.agent.stoppingDistance)
                {
                    walkToSet = false;
                    //if the agent doesn't have a path to a point, or they are stopped
                    if (!reference.agent.hasPath || reference.agent.velocity.sqrMagnitude == 0f)
                    {
                        walkToSet = false;
                    }
                }
            }
        }
    }

    private void SearchWalkPoint()
    {
        Debug.Log("I'm looking for a walk point");
        #region old walkpoint calculation
        //calculate random point in your range
        /*float randomZ = Random.Range(-walkToRange, walkToRange);
        float randomX = Random.Range(-walkToRange, walkToRange);

        walkTo = new Vector3(enemy.transform.position.x + randomX, enemy.transform.position.y, enemy.transform.position.z + randomZ);
        //
        if (Physics.Raycast(walkTo, -enemy.transform.up, 2f, reference.whatIsGround))
        {
            walkToSet = true;
        }
        #endregion
        //find a walk point after you've moved to one already
        walkTo = new Vector3(Random.insideUnitSphere.x * walkToRange, enemy.transform.position.y, Random.insideUnitSphere.z * walkToRange);
        walkToSet = true;
        Debug.Log("i'm going to " + walkTo);

        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;

        if (NavMesh.SamplePosition(walkTo, out hit, walkToRange, 1))
        {
            //if walkPoint is on the NavMesh, go to it
            finalPosition = hit.position;
            walkTo = finalPosition;
        }
    }*/
}
