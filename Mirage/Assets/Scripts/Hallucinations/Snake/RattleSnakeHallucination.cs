using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RattleSnakeHallucination : MonoBehaviour
{


    public AudioSource rattlingSound;
    public AudioSource attackSound;

    public LayerMask player;
    private Transform target;
    private Vector3 snakeMovement;

    public float rattleRangeSize;
    public float rattleAttackSize;

    public bool attacking = false;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        snakeBehavior();


    }



    private void OnDrawGizmosSelected()
    {

        Gizmos.DrawWireSphere(transform.position, rattleRangeSize);
        Gizmos.DrawWireSphere(transform.position, rattleAttackSize);
    }


    public void rattleWarning()
    {
        //if (PlayerStats.Instance.isHallucinating == true)
        // {

        if (attacking == false && !rattlingSound.isPlaying)
        {
            Collider[] attackPlayer = Physics.OverlapSphere(transform.position, 40f, player);
            //plays audio for rattling
            foreach (Collider targetPlayer in attackPlayer)
            {
                    rattlingSound.Play();
            }
        }
       // }
    }

    public void rattleAttack()
    {
       // if (PlayerStats.Instance.isHallucinating == true)
       // {
                // detect player within range
                Collider[] attackPlayer = Physics.OverlapSphere(transform.position, 15f, player);
                foreach (Collider targetPlayer in attackPlayer)
                {
                   if (!attackSound.isPlaying)
                   {
                     attacking = true;
                     rattlingSound.Stop();
                     //Rattlesnake appears.
                     //Attack Animation function goes here when rattlesnake gets the function
                     //attempted attack function goes here
                     attackSound.Play();
                     //Debug.Log("In the process of working things out");
                   }

                }

      //  }
    }

    //This function is made to compile all of it's behaviors into one action. More will be added on this later.
    public void snakeBehavior()
    {
        rattleWarning();
        rattleAttack();
    }



    void facePlayer()
    {
        //these 3 lines allow the enemy to face the player.
        Vector3 direction = target.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //enemyRB.rotation = angle;

        direction.Normalize();
        snakeMovement = direction;
    }




}
