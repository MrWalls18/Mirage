using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    //roam
    public Vector3 walkTo;
    bool walkToSet;
    public float walkToRange;

    //attack

    //state
    public float sightRange, attackRange;
    public bool playerInSight, playerInAttackRange;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }


    private void Update()
    {
        //can you see/attack the player?
        playerInSight = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        //state machine
        if (!playerInSight && !playerInAttackRange) Patrol();
        if (playerInSight && !playerInAttackRange) StalkPlayer();
        if (playerInAttackRange && playerInSight) Attack();

    }

    private void Patrol()
    {
        if (walkToSet) SearchWalkPoint();

        if (walkToSet)
        {
            agent.SetDestination(walkTo);
        }

        Vector3 distanceToWalkTo = transform.position - walkTo;

        if(distanceToWalkTo.magnitude < 1f)
        {
            walkToSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        //calculate random point in your range
        float randomZ = Random.Range(-walkToRange, walkToRange);
        float randomX = Random.Range(-walkToRange, walkToRange);

        walkTo = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkTo, -transform.up, 2f, whatIsGround))
        {
            walkToSet = true;
        }
    }

    private void StalkPlayer()
    {
        agent.SetDestination(player.position);
        Debug.Log("I see you, I'm stalking you");
    }

    private void Attack()
    {
        //melee attack goes here

        agent.SetDestination(transform.position);

        transform.LookAt(player);
        Debug.Log("I've attacked");
    }

    private void OnDrawGizmosSelected()
    {
        //so we can see everything going on
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }

}
