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



    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {

        if (!rattlingSound.isPlaying && !attackSound.isPlaying)
        {
            rattleWarning();
        }


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
            Collider[] attackPlayer = Physics.OverlapSphere(transform.position, 40f, player);
            //plays audio for rattling
            foreach (Collider targetPlayer in attackPlayer)
            {
           
                rattlingSound.Play();
            Debug.Log("I played");

            }
       // }
    }

    public void rattleAttack()
    {
      //  if (PlayerStats.Instance.isHallucinating == true)
       // {
            // detect player within range
            Collider2D[] attackPlayer = Physics2D.OverlapCircleAll(transform.position, 15f, player);
            foreach (Collider2D targetPlayer in attackPlayer)
            {
                //Rattlesnake appears.
                //Attack Animation function goes here when rattlesnake gets the function
                //attempted attack function goes here
               attackSound.Play();
            }
     //   }
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
