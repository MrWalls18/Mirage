using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compass : SingletonPattern<Compass>
{
    [Header("Compass Direction")]
    private Vector3 v3;
    public Transform player;
    public Quaternion direction;
    public GameObject needle;

    public Transform north;

    [Header("for UI Manager")]
    public GameObject compassParent;
    public bool hasCompass;
    public bool isCompassHallucinating = false;

    private void EnableCompass()
    {
        if (hasCompass && !compassParent.activeInHierarchy)
        {
            compassParent.SetActive(true);
        }
    }

    private void Update()
    {
        if (hasCompass)
        {
            IdentifyDirection();
        }
    }

    public void IdentifyDirection()
    {
        v3.y = player.eulerAngles.y + 90;
        v3.y = 360 - v3.y;
        if (isCompassHallucinating)
        {
            v3.y += 180;
        }
        needle.transform.localEulerAngles = v3;
    }

}
