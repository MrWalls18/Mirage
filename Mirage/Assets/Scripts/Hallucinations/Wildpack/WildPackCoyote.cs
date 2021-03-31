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
    
    
    // Start is called before the first frame update
    void Start()
    {
        timer = Time.time + wildpackMeshDuration;

    }

    // Update is called once per frame
    void Update()
    {
        playerPosition = GameObject.Find("Player").transform;

        Debug.Log(coyoteMesh.isVisible);

        if (timer < Time.time && !coyoteMesh.isVisible)
        {
            coyoteMesh.enabled = false;
        }




        coyoteAgent.SetDestination(playerPosition.position);
        
    }
}
