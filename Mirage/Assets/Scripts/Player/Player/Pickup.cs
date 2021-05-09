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
    //public Rigidbody rb;

    public Transform rockHolder;
    public float throwForce;
    public bool carryObject;
    public GameObject rock;
    public bool canPickUp;

    public float pickUpScale = 0.7f;
    Vector3 oldScale;

    //, rockContainer, guide;
    //collider to detect collisions with coyote
    //public BoxCollider coll;

    //public GameObject rockParent;

    //how far can the player reach to pick up a rock
    //public float pickUpRange;

    private void Start()
    {
        
        /*if (!canPickUp)
        {
            rb.isKinematic = false;
            coll.isTrigger = false;
        }
        if (canPickUp)
        {
            rb.isKinematic = true;
            coll.isTrigger = true;
            handFull = true;
        }*/
    }

    private void Update()
    {
        //check distance between player and pickup
        //Vector3 distanceToPlayer = player.position - transform.position;

        if(Input.GetKeyDown(KeyCode.E))
        {
            
            RaycastHit hit;
            Ray directionRay = new Ray(transform.position, transform.forward);
            Vector3 tempSize = transform.localScale;
             
            //OnDrawGizmos();
            if (Physics.Raycast(directionRay, out hit, 5000f))
            {
                if(hit.collider.tag == "Rock")
                {
                    oldScale = rock.transform.localScale;
                    carryObject = true;
                    canPickUp = true;
                    if(carryObject == true)
                    {
                        rock = hit.collider.gameObject;
                        rock.transform.SetParent(rockHolder);
                        rock.gameObject.transform.position = rockHolder.position;
                        rock.transform.localScale = new Vector3(pickUpScale, pickUpScale, pickUpScale);
                        rock.GetComponent<Rigidbody>().isKinematic = true;
                        rock.GetComponent<Rigidbody>().useGravity = false;
                    }
                }
            }
           
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            carryObject = false;
            canPickUp = false;
        }
        if(carryObject == false)
        {
            rockHolder.DetachChildren();
            rock.GetComponent<Rigidbody>().isKinematic = false;
            rock.GetComponent<Rigidbody>().useGravity = true;
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (rock.transform.parent != null)
            {
                rockHolder.DetachChildren();
                rock.GetComponent<Rigidbody>().isKinematic = false;
                rock.GetComponent<Rigidbody>().useGravity = true;
                rock.transform.localScale = oldScale;
                rock.GetComponent<Rigidbody>().AddForce(gameObject.transform.forward * throwForce);
                rock.transform.parent = null;
            }
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward);
    }

    /*private void Equip()
    {
        //check if hands are full
        canPickUp = true;
        handFull = true;
        //set rock as child of player, and add to hand
        //transform.SetParent(rockContainer);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);

        rb.isKinematic = true;
        coll.isTrigger = true;
    }*/

    /*private void Throw()
    {
        canPickUp = false;
        handFull = false;

        //throw the rock
        rockContainer.GetComponent<Rigidbody>().AddForce(rockContainer.transform.forward * throwForce);

        transform.SetParent(null);

        rb.isKinematic = false;
        coll.isTrigger = false;

        //setting rocks speed to players speed
        //rb.velocity = player.GetComponent<Rigidbody>().velocity;

        //rb.AddForce(rockContainer.forward * throwForce, ForceMode.Impulse);
        //rb.AddForce(rockContainer.up * throwForce, ForceMode.Impulse);

        float random = Random.Range(-1f, 1f);
    }*/
}
