
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private PlayerStats myStats;

    public float speed;
    [SerializeField]public float defaultSpeed;

    public Camera mainCam;

    private void Awake()
    {
        defaultSpeed = speed;
    }
    // Update is called once per frame
    void Update()
    {
        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");

        Vector3 move = transform.right * xInput + transform.forward * zInput + -transform.up;

        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            myStats.isRunning = true;
        }

        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            myStats.isRunning = false;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;

            if(Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out hit))
            {
                if (hit.collider.tag == "Water")
                    myStats.DrinkWater();
            }
        }

        controller.Move(move * speed * Time.deltaTime);

        Debug.Log(speed);
    }
}