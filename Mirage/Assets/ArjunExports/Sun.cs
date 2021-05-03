using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sun : SingletonPattern<Sun>
{
    [Header("Rotates at 1 degree per second, set to 6 for 1 minute. Divide this number by the amount multiplied for the amount of time you want it to take")]
    [SerializeField] private float transformSpeed = 1f;
    public float timer = 0f;
    public float sunMultiplier = 1.0f;
    public float sunPercentage;
    [SerializeField] private float daylightAngle = 240f;

    private void Update()
    {
        MoveSunlight(sunMultiplier);
    }

    public void MoveSunlight(float multiplier)
    {
        transform.RotateAround(Vector3.zero, Vector3.right, transformSpeed * sunMultiplier * Time.deltaTime);
        transform.LookAt(Vector3.zero);
        timer += (Time.deltaTime * sunMultiplier);
        sunPercentage = (timer * transformSpeed) / daylightAngle;
    }


}
