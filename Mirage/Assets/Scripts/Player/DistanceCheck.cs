using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceCheck : SingletonPattern<DistanceCheck>
{
    [SerializeField] Transform playerPos;
    private Vector3 lastPosition;
    public float totalDistance;
    [SerializeField] private Transform endPos;

    private void Start()
    {
        playerPos = PlayerStats.Instance.gameObject.transform;
        lastPosition = playerPos.position;

    }

    private void Update()
    {
        CheckDistance();
    }

    private void CheckDistance()
    {
        float distance = Vector3.Distance(lastPosition, playerPos.position);
        totalDistance += distance;
        lastPosition = playerPos.position;
        Debug.Log(totalDistance);
    }

    public float DistanceToEnd()
    {
        return Vector3.Distance(playerPos.position, endPos.position);
    }

}
