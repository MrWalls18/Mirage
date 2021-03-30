using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]private CharacterController controller;

    [SerializeField]private float walkingSpeed; 

    // Update is called once per frame
    void Update()
    {
        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");

        Vector3 move = transform.right * xInput + transform.forward * zInput + -transform.up;

        controller.Move(move * walkingSpeed * Time.deltaTime);

    }
}
