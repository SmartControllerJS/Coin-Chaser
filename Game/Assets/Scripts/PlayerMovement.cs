using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator m_Animator;

    public float runSpeed = 40f;
    float horizontalMove = 0f;
    bool jump = false;
    private Vector2 originalPos;
    public ParticleSystem SpeedParticle{
        get
        {
            if (_CachedSpeedParticle == null)
                _CachedSpeedParticle = GetComponent<ParticleSystem>();
            return _CachedSpeedParticle;
        }
    }
    public ParticleSystem _CachedSpeedParticle;

    void Awake()
    {
        SpeedParticle.Stop();
    }

    void Update()
    {
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
        if (other.gameObject.layer == 8)
        {
            // If the player drops from the map, spawn them back in
            transform.position = originalPos;
        }

        if (other.tag == "SpeedUp" && runSpeed == 40f)
        {   
            // Start timer for power effect
			runSpeed = runSpeed * 1.3f;
            Destroy(other.gameObject);
			StartCoroutine(ResetSpeedPower(10f));
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

    private IEnumerator ResetSpeedPower(float waitTime)
	{
        SpeedParticle.Play();
		yield return new WaitForSeconds(waitTime);
        SpeedParticle.Stop();
        runSpeed = 40f;
	}
}
