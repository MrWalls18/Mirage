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

    //, rockContainer, guide;
    //collider to detect collisions with coyote
    //public BoxCollider coll;

    //public GameObject rockParent;

    //how far can the player reach to pick up a rock
    //public float pickUpRange;
    

    //Vector3 rockPos;

    //player slot container
    
    //public static bool handFull = false;

    //public GameObject item;
    //public GameObject tempParent;

    /*private void Start()
    {
        if (!canPickUp)
        {
            rb.isKinematic = false;
            coll.isTrigger = false;
        }
        if (canPickUp)
        {
            rb.isKinematic = true;
            coll.isTrigger = true;
            handFull = true;
        }
    }*/

    private void Update()
    {
        //check distance between player and pickup
        //Vector3 distanceToPlayer = player.position - transform.position;

        if(Input.GetKeyDown(KeyCode.F))
        {
            
            RaycastHit hit;
            Ray directionRay = new Ray(transform.position, transform.forward);
            Debug.Log("hit is equal to " + directionRay);
            //OnDrawGizmos();
            if (Physics.Raycast(directionRay, out hit, 20f))
            {
                //Debug.DrawLine(transform.position, hit.point, Color.red);
                Debug.Log("did i hit the rock? i hit " + hit.collider.tag);
                if(hit.collider.tag == "Rock")
                {
                    carryObject = true;
                    canPickUp = true;
                    Debug.Log("carryObject = " + carryObject);
                    Debug.Log("canPickUp = " + canPickUp);
                    if(carryObject == true)
                    {
                        rock = hit.collider.gameObject;
                        rock.transform.SetParent(rockHolder);
                        rock.gameObject.transform.position = rockHolder.position;
                        rock.GetComponent<Rigidbody>().isKinematic = true;
                        rock.GetComponent<Rigidbody>().useGravity = false;
                    }
                }
            }
            #region delete later
            /*if (!handFull)
            {
                Debug.Log("i'm in the second if statement");
                rock.transform.SetParent(rockParent.transform);
                Debug.Log("my parent is " + rock.transform.parent);
                GetComponent<Rigidbody>().isKinematic = true;
                rock.GetComponent<Rigidbody>().velocity = Vector3.zero;
                rock.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

                if (canPickUp && Input.GetKeyDown(KeyCode.F))
                {
                    //Throw();
                    GetComponent<Rigidbody>().isKinematic = false;
                    transform.parent = null;
                    rock.GetComponent<Rigidbody>().AddForce(rockParent.transform.forward * throwForce);
                    handFull = false;
                }
            }*/
            //Equip();
            #endregion
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
            if (canPickUp)
            {
                rockHolder.DetachChildren();
                rock.GetComponent<Rigidbody>().isKinematic = false;
                rock.GetComponent<Rigidbody>().useGravity = true;
                rock.GetComponent<Rigidbody>().AddRelativeForce(transform.forward * throwForce);
            }
        }
        /*else
        {
            rockPos = rock.transform.position;
            rock.transform.SetParent(null);
            rock.transform.position = rockPos;
        }*/
        
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
        Debug.Log("my parent is " + transform.parent);
        //transform.SetParent(rockContainer);
        Debug.Log("is my parent the rock container? " + transform.parent);
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
