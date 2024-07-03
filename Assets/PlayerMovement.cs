using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Move")]
    public float moveSpeed;

    [Header("KeyBind")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Basic")]
    public Transform PlayerCamera;   

    private float horizontalInput;   
    private float verticalInput;     

    private Vector3 moveDirection;  
    private Rigidbody rbFirstPerson; 
    void Start()
    {
        rbFirstPerson = GetComponent<Rigidbody>();
        rbFirstPerson.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        MyInput();
        SpeedControl();
    }
    private void FixedUpdate()
    {
        MovePlayer(); 
    }
    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer()
    {  
        moveDirection = PlayerCamera.forward * verticalInput + PlayerCamera.right * horizontalInput;
        rbFirstPerson.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
    }
    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rbFirstPerson.velocity.x, 0f, rbFirstPerson.velocity.z);
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rbFirstPerson.velocity = new Vector3(limitedVel.x, rbFirstPerson.velocity.y, limitedVel.z);
        }
    }
}

