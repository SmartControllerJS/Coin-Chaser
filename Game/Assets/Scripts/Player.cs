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
    private float horizontal_value = 0;
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

        if (leftPressed)
        {
            horizontalInput = horizontalInput - 1;
        }

        if (rightPressed)
        {
            horizontalInput = horizontalInput + 1;
            rightPressed = false;
        }

    }

    private void FixedUpdate()
    {
        rigidbodyComponent.velocity = new Vector3(horizontalInput, rigidbodyComponent.velocity.y, 0);


        if (Physics.OverlapSphere(groundCheckTransform.position, 0.1f, playerMask).Length == 0)
        {
            return;
        }

        if (jumpKeyWasPressed)
        {
            rigidbodyComponent.AddForce(Vector3.up * 6, ForceMode.VelocityChange);
            jumpKeyWasPressed = false;
        }

        leftPressed = false;
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
        Debug.Log("Log: right was pressed");
        rightPressed = true;
    }

    public void LeftButtonDown()
    {
        leftPressed = true;
        Debug.Log("Log: Left was pressed");
    }

    public void RightButtonUp()
    {
        rightPressed = false;
        Debug.Log("Log: Right was unpressed");
    }

    public void StopMovement()
    {
        leftPressed = false;
        Debug.Log("Log: Movement stopped");
        horizontal_value = 0;
    }

}
