using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity = 100f;

    [SerializeField] private Transform playerBody;

    private float xRotation = 0f;


    // Start is called before the first frame update
    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Gets mouse movement on X and Y axis
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;


        xRotation -= mouseY; //decreases x rotation based on mouse y


        xRotation = Mathf.Clamp(xRotation, -90f, 90f); //locks rotation between up and down


        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);



        //Rotates player body around the Y axis (Vector3.up)
        playerBody.Rotate(Vector3.up * mouseX);
    }
}