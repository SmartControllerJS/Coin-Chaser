using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private bool jumpKeyWasPressed;
    private float horizontalInput;
    private Rigidbody rigidbodyComponent;
    private bool isGrounded;
    [SerializeField] private Transform groundCheckTransform;
    [SerializeField] private LayerMask playerMask;
    private bool upPressed = false;
    private bool rightPressed = false;
    private bool leftPressed = false;
    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        rigidbodyComponent = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (upPressed)
        {
            jumpKeyWasPressed = true;
            upPressed = false;
        }

        horizontalInput = Input.GetAxis("Horizontal");

    }

    private void FixedUpdate()
    {
        // Horizontal movement using buttons
        Debug.Log("Left value is :" + leftPressed);
        if (leftPressed)
        {
            rigidbodyComponent.velocity = new Vector3(-1,rigidbodyComponent.velocity.y,0);
        }
        else if (rightPressed) 
        {
            rigidbodyComponent.velocity = new Vector3(1,rigidbodyComponent.velocity.y,0);
        }
        else
        {
            rigidbodyComponent.velocity = new Vector3(horizontalInput, rigidbodyComponent.velocity.y, 0);
        }

        // Collision check using a shere overlap
        if (Physics.OverlapSphere(groundCheckTransform.position, 0.1f, playerMask).Length == 0)
        {
            return;
        }

        // Jump movement check
        if (jumpKeyWasPressed)
        {
            rigidbodyComponent.AddForce(Vector3.up * 6, ForceMode.VelocityChange);
            jumpKeyWasPressed = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            Destroy(other.gameObject);
        }
    }

    private void JumpButton()
    {
        Debug.Log("Log: Jump was pressed");
        upPressed = true;
    }

    public void RightButtonDown()
    {
        rightPressed = true;
        Debug.Log("Log: right was pressed");
    }

    public void LeftButtonDown()
    {
        leftPressed = true;
        Debug.Log("Log: Left was pressed");
    }

    public void StopMovement()
    {
        leftPressed = false;
        rightPressed = false;
        Debug.Log("Log: Movement stopped");
    }

}
