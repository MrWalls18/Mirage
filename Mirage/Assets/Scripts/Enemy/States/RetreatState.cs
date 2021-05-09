using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RetreatState : StateMachineBehaviour
{
    //if hit by rock, run from player for a time
    EnemyAI enemy;

    float timeElapsed;
    //public float retreatTime = 5f;
    public float retreatStartTime { get; private set; }

    private float retreatDistance = 20f;

    private Vector3 runDirection;

    private float distFromPlayer;

    bool isRetreating = true;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.GetComponent<EnemyAI>();
        //retreatStartTime = Time.time;
        Retreat();
        Debug.Log("i should run away...");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //enemy.agent.SetDestination(enemy.retreat.transform.position);
        //after a certain amount of time, we want the AI to follow the player again
        //Retreat();
        distFromPlayer = Vector3.Distance(enemy.transform.position, enemy.player.transform.position);

        enemy.agent.SetDestination(runDirection);

        //timeElapsed += Time.time;
        if (distFromPlayer > retreatDistance)
        {
            Debug.Log("i'm waiting to stalk again");
            isRetreating = false;
            animator.SetBool("hitByRock", false);
            //timeElapsed = 0;
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
        animator.SetBool("hitByRock", false);
    }

    public void Retreat()
    {
        //Vector3 runDirection;
        enemy.agent.ResetPath();
        Debug.Log("RETREAT!!");
        if(SamplePostion.Instance.RandomPoint(enemy.transform.position, retreatDistance, out runDirection))
        {
            enemy.agent.isStopped = false;
            //enemy.agent.SetDestination(runDirection);
            
        }
        Debug.Log("my runDirection is " + runDirection);
        //Vector3 retreatDirection = Vector3.MoveTowards(enemy.transform.position, enemy.player.transform.position, -enemy.speed * Time.deltaTime);
        Vector3 retreatDirection = enemy.transform.forward * -1 * retreatDistance;
        Vector3 firstDestination = enemy.transform.position + retreatDirection;

        SetPath(firstDestination);
    }

    private void SetPath(Vector3 destination)
    {
        NavMeshHit hit;
        bool navMeshFound = NavMesh.SamplePosition(destination, out hit, 1.0f, NavMesh.AllAreas);
        //NavMesh.FindClosestEdge(enemy.transform.position, out hit, NavMesh.AllAreas);

        if(navMeshFound == true)
        {
            Debug.Log("I've found a point on the navmesh");
            NavMeshPath path = new NavMeshPath();
            enemy.agent.CalculatePath(hit.position, path);

            if(path.status != NavMeshPathStatus.PathInvalid)
            {
                enemy.agent.SetDestination(hit.position);
                if (enemy.transform.position == hit.position)
                {
                    enemy.animator.SetBool("hitByRock", false);
                }
            }
        }

    }

}
