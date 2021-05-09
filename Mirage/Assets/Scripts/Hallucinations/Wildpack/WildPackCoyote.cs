using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WildPackCoyote : MonoBehaviour
{
    [SerializeField] private NavMeshAgent coyoteAgent;

    private Transform playerPosition;

    

    private float timer;
    [SerializeField] private float wildpackMeshDuration;

    [SerializeField] private MeshRenderer coyoteMesh;
    [SerializeField] private MeshCollider coyoteCollider;
    
    
    // Start is called before the first frame update
    void Start()
    {
        timer = Time.time + wildpackMeshDuration;

    }

    // Update is called once per frame
    void Update()
    {


        if (timer < Time.time && !coyoteMesh.isVisible)
        {
            coyoteMesh.enabled = false;
            coyoteCollider.enabled = false;
        }

        if (!coyoteMesh.enabled)
        {
            coyoteAgent.enabled = false;

            transform.position += transform.forward * Time.deltaTime * coyoteAgent.speed * 2;

            if (Vector3.Distance(this.transform.position, playerPosition.position) > 75)
            {
                Destroy(this.gameObject);
            }
        }

        else
        { 
            playerPosition = PlayerStats.Instance.transform;
            coyoteAgent.SetDestination(playerPosition.position);
        }

        
    }
}
