using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;

    //public Transform player;
    public GameObject player;
    public float distanceToPlayer;


    public LayerMask whatIsGround, whatIsPlayer;

    //building speed for stalking player
    //public float minSpeed = 3;
    //public float maxSpeed = 6;
    public float speed = 5f;
    //public float currentTime;

    //time between speed ups
    public float timeToIncrease = 5f;
    //how much to speed up
    public float speedIncrement = 0.2f;

    //roam
    //public Vector3 walkTo;
    //bool walkToSet;
    //public float walkToRange = 10f;

    //attack

    //state
    public float sightRange, attackRange;
    public bool playerInSight, playerInAttackRange;

    private PlayerStats myStats;

    public Animator animator;

    private void Awake()
    {
        //find the player
        //player = GameObject.Find("Player").transform;
        player = FindObjectOfType<PlayerMovement>().gameObject;
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        myStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        

        //start timer, may need to move this
        //currentTime = Time.time + timeToIncrease;

        //animator.Play(IdleState);
    }

    public void Start()
    {
        //make sure enemy is a navmesh agent
        agent = GetComponent<NavMeshAgent>();

    }

    private void Update()
    {
        //can you see/attack the player?
        //playerInSight = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        //playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);


        //state machine

        //if (!playerInSight && !playerInAttackRange) Patrol();

        /*if (playerInSight && !playerInAttackRange)
        {
            StalkPlayer();
            minSpeed += speedIncrement;
            currentTime = Time.time + timeToIncrease;
        }*/

        //if (playerInAttackRange && playerInSight) Attack();
    }

    //patrol if you don't detect the player
    /*private void Patrol()
    {
        if (!walkToSet) SearchWalkPoint();

        if (walkToSet)
        {
            agent.SetDestination(walkTo);
        }

        Vector3 distanceToWalkTo = transform.position - walkTo;

        if(distanceToWalkTo.magnitude < 1f)
        {
            walkToSet = false;
        }
    }*/

    //find a walk point for the coyote to go to
    /*private void SearchWalkPoint()
    {
        //calculate random point in your range
        float randomZ = Random.Range(-walkToRange, walkToRange);
        float randomX = Random.Range(-walkToRange, walkToRange);

        walkTo = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkTo, -transform.up, 2f, whatIsGround))
        {
            walkToSet = true;
        }
    }*/

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
