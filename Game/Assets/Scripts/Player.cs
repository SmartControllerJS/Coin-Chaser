using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector2 originalPos;
    private bool jumpKeyWasPressed;
    private float horizontalInput;
    private Rigidbody2D rigidbodyComponent;
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
        rigidbodyComponent = GetComponent<Rigidbody2D>();
        originalPos = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (upPressed || Input.GetKeyDown("space"))
        {
            jumpKeyWasPressed = true;
            upPressed = false;
        }

        horizontalInput = Input.GetAxis("Horizontal");

    }

    private void FixedUpdate()
    {
        // Horizontal movement using buttons
        if (leftPressed)
        {
            rigidbodyComponent.velocity = new Vector2(-1,rigidbodyComponent.velocity.y);
        }
        else if (rightPressed) 
        {
            rigidbodyComponent.velocity = new Vector2(1,rigidbodyComponent.velocity.y);
        }
        else
        {
            rigidbodyComponent.velocity = new Vector2(horizontalInput, rigidbodyComponent.velocity.y);
        }

        // Collision check using a shere overlap
        if (Physics2D.OverlapCircleAll(groundCheckTransform.position, 0.1f, playerMask).Length == 0)
        {   
            return;
        }

        // Jump movement check
        if (jumpKeyWasPressed)
        {
            rigidbodyComponent.AddForce(new Vector2(0f, 5f), ForceMode2D.Impulse);
            jumpKeyWasPressed = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 7)
        {
            Destroy(other.gameObject);
        }
        if (other.gameObject.layer == 8)
        {
            Debug.Log("Position = " + originalPos);
            transform.position = originalPos;
        }
    }

    private void JumpButton()
    {
        upPressed = true;
        Debug.Log("Log: Jump was pressed");
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
