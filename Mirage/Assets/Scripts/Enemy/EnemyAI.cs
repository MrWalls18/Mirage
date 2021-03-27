using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    //building speed for stalking player
    public float minSpeed = 3;
    public float maxSpeed = 6;
    public float currentTime;
    //time between speed ups
    public float timeToIncrease = 5f;
    //how much to speed up
    public float speedIncrement = 0.2f;

    //roam
    public Vector3 walkTo;
    bool walkToSet;
    public float walkToRange = 10f;

    //attack

    //state
    public float sightRange, attackRange, contactTimer;
    public bool playerInSight, playerInAttackRange;

    private PlayerStats myStats;
    private float timer;

    public GameObject losePanel;


    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        myStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        agent = GetComponent<NavMeshAgent>();
        currentTime = Time.time + timeToIncrease;

        timer = Time.time + contactTimer;
    }


    private void Update()
    {
        if (this.gameObject != null)
        {
            //can you see/attack the player?
            playerInSight = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

            //state machine
            if (!playerInSight && !playerInAttackRange) Patrol();
            if (playerInSight && !playerInAttackRange)
            {
                StalkPlayer();
                minSpeed += speedIncrement;
                currentTime = Time.time + timeToIncrease;
            }
            if (playerInAttackRange && playerInSight) Attack();
        }

    }

    private void Patrol()
    {
        //Reggie's Edit
        //despawn if no contact after x amount of seconds
        if (timer < Time.time)
        {
            Destroy(this.gameObject);
        }

        if (this.gameObject != null)
        {
            if (!walkToSet) SearchWalkPoint();

            if (walkToSet)
            {
                agent.SetDestination(walkTo);
            }

            Vector3 distanceToWalkTo = transform.position - walkTo;

            if (distanceToWalkTo.magnitude < 1f)
            {
                walkToSet = false;
            }
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
        timer = Time.time + contactTimer;
    }

    private void Attack()
    {
        //melee attack goes here
        agent.SetDestination(transform.position);
        transform.LookAt(player);

        //Reggie's Code
        //Added 2/23/21 @ 11:42am
        if (this.gameObject.tag == "FakeEnemy")
        {
            Debug.Log("Fake Enemy Attack");
            Debug.Log("Damage sanity");
            myStats.sanity -= 5f;
            Destroy(this.gameObject);
        }

        else if (this.gameObject.tag == "Enemy")
        {
            Cursor.lockState = CursorLockMode.None;
            MenuUI.Instance.OpenPanel(3);
        }
            
    }

    private void Retreat()
    {
        //agent.SetDestination();
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
