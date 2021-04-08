using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Animator animator;

    //public Transform player;
    public GameObject player;
    public float distanceToPlayer;

    public Vector3 walkTo;
    bool walkToSet;
    public float walkToRange = 30f;

    public LayerMask whatIsGround, whatIsPlayer;

    public float speed = 5f;

    //time between speed ups
    /*public float timeToIncrease = 5f;
    //how much to speed up
    public float speedIncrement = 0.2f;*/

    //roam
    //public Vector3 walkTo;
    //bool walkToSet;
    //public float walkToRange = 10f;

    //attack

    //state
    public float sightRange;
    public float attackRange = 30;
    public bool playerInSight, playerInAttackRange;

    private PlayerStats myStats;

    //used to get distance from player
    public GameObject GetPlayer()
    {
        return player;
    }
    
    private void Awake()
    {
        //find the player
        //player = GameObject.Find("Player").transform;
        
        //myStats = GameObject.Find("Player").GetComponent<PlayerStats>();

        //start timer, may need to move this
        //currentTime = Time.time + timeToIncrease;

        //animator.Play(IdleState);
    }

    public void Start()
    {
        //get the animator and other necessities
        animator = GetComponent<Animator>();

        agent = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<PlayerMovement>().gameObject;

        //distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
    }

    private void Update()
    {
        #region old field of view checks
        //can you see/attack the player?
        //playerInSight = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        //playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        //distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        //if distance is less than agroRange, go to other state
        #endregion
        //function to check the distance in the animator
        animator.SetFloat("agroRange", Vector3.Distance(transform.position, player.transform.position));

        #region old state machine
        //state machine
        //if (!playerInSight && !playerInAttackRange) Patrol();

        /*if (playerInSight && !playerInAttackRange)
        {
            StalkPlayer();
            minSpeed += speedIncrement;
            currentTime = Time.time + timeToIncrease;
        }*/

        //if (playerInAttackRange && playerInSight) Attack();
        #endregion
    }


    //patrol if you don't detect the player
    public void Patrol()
    {
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
                        Debug.Log("I reached my destination");
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
        }*/
        #endregion
        //find a walk point after you've moved to one already
        walkTo = new Vector3(Random.insideUnitSphere.x * walkToRange, transform.position.y, Random.insideUnitSphere.z * walkToRange);
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
    }

    //follow the player
    /*private void StalkPlayer()
    {
        agent.SetDestination(player.position);
    }*/

    //attack the player, ending their game
    /*private void Attack()
    {
        //melee attack goes here
        agent.SetDestination(transform.position);
        transform.LookAt(player);

        //Reggie's Code
        //Added 2/23/21 @ 11:42am
        if (gameObject.tag == "FakeEnemy")
        {
            Debug.Log("Fake Enemy Attack");
            Debug.Log("Damage sanity");
            myStats.sanity -= 5f;
            Destroy(gameObject);
        }

        else if (gameObject.tag == "Enemy")
        {


            Debug.Log("I've attacked, player is dead");
        }
            
    }*/

    //retreat when hit by rock
    /*private void Retreat()
    {
        //agent.SetDestination();
    }*/

    private void OnDrawGizmosSelected()
    {
        //so we can see everything going on
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }

}
