using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]private CharacterController controller;
    [SerializeField] private PlayerStats myStats;

    public float walkingSpeed;
    private float defaultSpeed;

    Vector3 velocity;
    public float gravity = -9.81f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;

    public float jumpHeight = 3f;

    private void Awake()
    {
        defaultSpeed = walkingSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

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

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }


        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

    }

    public void ResetSpeed()
    {
        walkingSpeed = defaultSpeed;
    }
}
