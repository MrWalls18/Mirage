using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    
    public float moveSpeed;
    
    public float turnSpeed = 10f;

    public float height = .5f;
    public float heightPadding = 0.05f;
    public LayerMask ground;
    public float maxGroundAngle = 140;

    //so we can see direction vector
    public bool debug;

    Vector2 movement;
    float angle;
    //current ground angle
    float groundAngle;

    Quaternion playerRotation;
    new Transform camera;

    Vector3 forward;
    RaycastHit hitInfo;
    public bool isGrounded;

    [HideInInspector] public float defaultSpeed;
    [SerializeField] private PlayerStats myStats;
    [SerializeField] private Camera mainCam;

    void Awake()
    {
        isGrounded = true;
        defaultSpeed = moveSpeed;
    }

    void Start()
    {
        camera = Camera.main.transform;
    }
    
    //inputs handled here
    void Update()
    {
        acquireInput();
        CalculateDirection();
        CalculateForward();
        CalculateGroundAngle();
        OnGround();
        ApplyGravity();
        gibDebugLines();

        //faces player in the direction of last input
        if(Mathf.Abs(Input.GetAxis("Horizontal")) < 1 && Mathf.Abs(Input.GetAxis("Vertical")) < 1) return;
        
        CalculateDirection();
        Rotate();
        Move();
        
        
       
    }




    //is the player on the ground
    void OnGround()
    {
        Debug.Log("before OnGround if statement, isGrounded is set too " + isGrounded);
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hitInfo, height + heightPadding, ground))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
        Debug.Log("isGrounded is set too " + isGrounded);
    }

    //using either w,a,s,d or arrow keys to move player
    void acquireInput()
    {
        Input.GetAxisRaw("Horizontal");
        Input.GetAxisRaw("Vertical");
    }

    //calculate player direction relative to the camera angle
    void CalculateDirection()
    {
        angle = Mathf.Atan2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")); //gives radians for direction
        angle = Mathf.Rad2Deg * angle; //0 to 360 angles
        angle += camera.eulerAngles.y; //move/rotate relative to camera
    }

    //rotate the player so they face where they move
    void Rotate()
    {
        playerRotation = Quaternion.Euler(0, angle, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, playerRotation, turnSpeed * Time.deltaTime);
    }

    //move the player
    void Move()
    {
        if(groundAngle >= maxGroundAngle)
        {
            return;
        }

        transform.position += forward * moveSpeed * Time.deltaTime;

        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            myStats.isRunning = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            myStats.isRunning = false;
        }
        
    }

    void CalculateForward()
    {
        if (!isGrounded)
        {
            forward = transform.forward;
            return;
        }

        forward = Vector3.Cross(hitInfo.normal, -transform.right);
    }

    //calculate if player can go up the angle or not
    void CalculateGroundAngle()
    {
        if (!isGrounded)
        {
            groundAngle = 90;
            return;
        }

        groundAngle = Vector3.Angle(hitInfo.normal, transform.forward);
    }



    //idk what else you expect this to do
    void ApplyGravity()
    {
        if (!isGrounded)
        {
            transform.position += Physics.gravity * Time.deltaTime;
        }
    }

    //for debugging purposes
    void gibDebugLines()
    {
        if (!debug)
        {
            return;
        }

        Debug.DrawLine(transform.position, transform.position + Vector3.forward * height * 2, Color.blue, 2, false);
        Debug.DrawLine(transform.position, transform.position - Vector3.up * height, Color.green, 2, false);
    }

}
