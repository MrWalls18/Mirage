using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]private CharacterController controller;
    [SerializeField] private PlayerStats myStats;

    public float walkingSpeed;
    private float defaultSpeed;

    private void Awake()
    {
        defaultSpeed = walkingSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");

        Vector3 move = transform.right * xInput + transform.forward * zInput + -transform.up;

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            myStats.isRunning = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            myStats.isRunning = false;
            ResetSpeed();
        }

        controller.Move(move * walkingSpeed * Time.deltaTime);


    }

    public void ResetSpeed()
    {
        walkingSpeed = defaultSpeed;
    }
}
