using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{
    [SerializeField] private float transformSpeed = 1f;

    void Update()
    {
        transform.RotateAround(Vector3.zero, Vector3.right, transformSpeed * Time.deltaTime);
        transform.LookAt(Vector3.zero);
    }
}
