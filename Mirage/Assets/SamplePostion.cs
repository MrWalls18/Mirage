using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SamplePostion : MonoBehaviour
{
    public float range = 10.0f;
    public int coyoteCount = 5;
    private int coyoteCounter = 0;

    [SerializeField] private GameObject wildCoyotePrefab;
    private GameObject wildCoyoteClone;

    [HideInInspector] public bool wasTriggered = false;

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            NavMeshHit hit;

            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                coyoteCounter++;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }

    void Update()
    {
        Vector3 point;
        if (RandomPoint(transform.position, range, out point) && coyoteCounter <= coyoteCount)
        {

            wildCoyoteClone = Instantiate(wildCoyotePrefab, point, transform.rotation);


            Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f);
        }

        if (coyoteCounter > coyoteCount)
        {
            coyoteCounter = 0;
            wasTriggered = true;
            this.gameObject.GetComponent<SamplePostion>().enabled = false;
        }
        
    }
}
