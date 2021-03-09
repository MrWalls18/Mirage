using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// when ready to reference what you're picking
/// up, call 'PlayerStats' to differentiate 
/// between drink water, and throw rock
/// </summary>

public class Pickup : MonoBehaviour
{
    public Rigidbody rb;
    public Transform player, rockContainer, guide;
    public BoxCollider coll;

    //how far can the player reach to pick up a rock
    public float pickUpRange;
    public float throwForce;

    //player slot container
    public bool hasRock;
    public static bool handFull;

    private void Start()
    {
        if (!hasRock)
        {
            rb.isKinematic = false;
            coll.isTrigger = false;
        }
        if (hasRock)
        {
            rb.isKinematic = true;
            coll.isTrigger = true;
            handFull = true;
        }
    }

    private void Update()
    {
        Vector3 distanceToPlayer = player.position - transform.position;
        if(!hasRock && distanceToPlayer.magnitude <= pickUpRange && Input.GetKeyDown(KeyCode.F) && !handFull)
        {
            Equip();
        }
        if(hasRock && Input.GetKeyDown(KeyCode.Space))
        {
            Throw();
        }
    }

    private void Equip()
    {
        //check if hands are full
        hasRock = true;
        handFull = true;
        //set rock as child of player, and add to hand
        transform.SetParent(rockContainer);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);

        rb.isKinematic = true;
        coll.isTrigger = true;
    }

    private void Throw()
    {
        hasRock = false;
        handFull = false;

        guide.GetChild(0).gameObject.GetComponent<Rigidbody>().velocity = transform.forward * throwForce;

        transform.SetParent(null);

        rb.isKinematic = false;
        coll.isTrigger = false;

        //setting rocks speed to players speed
        //rb.velocity = player.GetComponent<Rigidbody>().velocity;

        //rb.AddForce(rockContainer.forward * throwForce, ForceMode.Impulse);
        //rb.AddForce(rockContainer.up * throwForce, ForceMode.Impulse);

        float random = Random.Range(-1f, 1f);
    }
}
