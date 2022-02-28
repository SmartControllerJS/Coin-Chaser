using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator m_Animator;

    public float runSpeed = 40f;
    float horizontalMove = 0f;
    bool jump = false;
    private Vector2 originalPos;

    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (horizontalMove == 0f){
            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        }
        
        m_Animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (Input.GetButtonDown("Jump"))
        {
            JumpButton();
        }
    }

    public void OnLanding()
    {
        m_Animator.SetBool("IsJumping", false);
    }

    void FixedUpdate()
    {
        // call movement function from controller script, 
        // time function makes sure we move at same speed every update
        controller.Move(horizontalMove * Time.fixedDeltaTime, jump);
        jump = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 7)
        {
            Destroy(other.gameObject);
        }
        if (other.gameObject.layer == 8)
        {
            transform.position = originalPos;
        }
    }

    private void JumpButton()
    {
        jump = true;
        m_Animator.SetBool("IsJumping", true);
    }

    public void BeginMovement(int message)
    {
        horizontalMove = message * runSpeed;
    }

    public void StopMovement()
    {
        horizontalMove = 0;
    }




}
