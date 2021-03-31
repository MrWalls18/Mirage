using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WildPackCoyote : MonoBehaviour
{
    [SerializeField] private NavMeshAgent coyoteAgent;

    [SerializeField] private Transform wildpackDestination;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        coyoteAgent.SetDestination(wildpackDestination.position);
        
    }
}
