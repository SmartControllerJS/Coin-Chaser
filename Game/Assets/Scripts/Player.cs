using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
    public Animator m_Animator;

    // ground check
    public UnityEvent OnLandEvent;
    [SerializeField] private LayerMask m_WhatIsGround;
    [SerializeField] private Transform m_GroundCheck;
    private bool m_Grounded;
    const float k_GroundedRadius = .2f;

    // Start is called before the first frame update
    void Awake()
    {
        rigidbodyComponent = GetComponent<Rigidbody2D>();
        originalPos = gameObject.transform.position;

        if (OnLandEvent == null){
            OnLandEvent = new UnityEvent();
        }
			
    }

    // Update is called once per frame
    void Update()
    {
        if (upPressed || Input.GetKeyDown("space"))
        {
            jumpKeyWasPressed = true;
            upPressed = false;
            m_Animator.SetBool("IsJumping", true);
        }

        horizontalInput = Input.GetAxisRaw("Horizontal");
        m_Animator.SetFloat("Speed", horizontalInput);
        
    }

    private void FixedUpdate()
    {
        //bool wasGrounded = m_Grounded;
        //m_Grounded = false;
        //Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        //for (int i = 0; i < colliders.Length; i++)
		//{
		//	if (colliders[i].gameObject != gameObject)
		//	{
		//		m_Grounded = true;
		//		if (!wasGrounded){
        //            OnLandEvent.Invoke();
        //        }
		//	}
		//}



        // Horizontal movement using buttons
        if (leftPressed)
        {
            rigidbodyComponent.velocity = new Vector2(-1,rigidbodyComponent.velocity.y);
            m_Animator.SetFloat("Speed", -1);
        }
        else if (rightPressed) 
        {
            rigidbodyComponent.velocity = new Vector2(1,rigidbodyComponent.velocity.y);
            m_Animator.SetFloat("Speed", 1);
        }
        else
        {
            rigidbodyComponent.velocity = new Vector2(horizontalInput*4, rigidbodyComponent.velocity.y);
        }
        

        // Collision check using a shere overlap
        if (Physics2D.OverlapCircleAll(groundCheckTransform.position, 0.1f, playerMask).Length == 0)
        {   
            //m_Animator.SetBool("IsJumping", false);
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

    private void JumpButton(String message)
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
