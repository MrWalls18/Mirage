using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayAndNight : MonoBehaviour
{
    public Transform sun;
    public float rotationScale;
   

    // Update is called once per frame
    void Update()
    {
        sun.Rotate(rotationScale * Time.deltaTime, 0f, 0f);
    }
}
