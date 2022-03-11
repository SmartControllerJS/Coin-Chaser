using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using UnityEngine.UI; // added to change score number
using System.Collections;

public class CharacterController2D : MonoBehaviour
{
	[SerializeField] private float m_JumpForce = 600f;							// Amount of force added when the player jumps.
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;	// How much to smooth out the movement
	[SerializeField] private bool m_AirControl = false;							// Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask m_WhatIsGround;							// A mask determining what is ground to the character
	[SerializeField] private Transform m_GroundCheck;							// A position marking where to check if the player is grounded.

	const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
	private bool m_Grounded;            // Whether or not the player is grounded.
	private Rigidbody2D m_Rigidbody2D;
	private bool m_FacingRight = true;  // For determining which way the player is currently facing.
	private Vector3 m_Velocity = Vector3.zero;

    private Collider2D colliding;
	ParticleSystem JumpParticle
	{
        get
        {
            if (_CachedJumpParticle == null)
                _CachedJumpParticle = GetComponent<ParticleSystem>();
            return _CachedJumpParticle;
        }
    }
	public ParticleSystem _CachedJumpParticle;

	List<ParticleSystem> decreaseParticles	// List of particles next to other player's scores
	{
        get
        {
			for (int particle=0; particle < _CachedDecreaseParticleList.Count; particle++){
				if (_CachedDecreaseParticleList[particle] == null)
				{
					_CachedDecreaseParticleList[particle] = GetComponent<ParticleSystem>();
				}
			}
            return _CachedDecreaseParticleList;
        }
    }
	public List<ParticleSystem> _CachedDecreaseParticleList;
		
	public List<GameObject> otherPlayerList; // List of other players (not including this player)

    [SerializeField] public int coins = 0;	// Score count
	[SerializeField] public Text coinText;	// Score text object
	[SerializeField] private float pushForce = 20f;

	[Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;

	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
        colliding = GetComponent<Collider2D>();

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();
		
		// Make sure particle effects don't activate on player spawn
		JumpParticle.Stop(true);

		foreach (ParticleSystem otherScoreParticle in decreaseParticles){
			otherScoreParticle.GetComponent<ParticleSystem>().Stop();
		}
	}

	private void FixedUpdate()
	{
		bool wasGrounded = m_Grounded;
		m_Grounded = false;

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				m_Grounded = true;
				if (!wasGrounded)
					OnLandEvent.Invoke();
			}
		}

	}


	public void Move(float move, bool jump)
	{
		
		//only control the player if grounded or airControl is turned on
		if (m_Grounded || m_AirControl)
		{

			// Move the character by finding the target velocity
			Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
			// And then smoothing it out and applying it to the character
			m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

			// If the input is moving the player right and the player is facing left...
			if (move > 0 && !m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
			// Otherwise if the input is moving the player left and the player is facing right...
			else if (move < 0 && m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
		}
		// If the player should jump...
		if (m_Grounded && jump)
		{
			// Add a vertical force to the player.
			m_Grounded = false;
			m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
		}
	}


	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Coin")
        {
            Destroy(collision.gameObject);
            coins += 10;
			coinText.text = coins.ToString();
        }

        if (collision.tag == "JumpUp" && m_JumpForce == 600f)
        {
			Destroy(collision.gameObject);
			m_JumpForce = 800f;
			
			// Start timer for power effect
			StartCoroutine(ResetJumpPower(10f));
			
        }

		if (collision.tag == "DecreaseCoin")
        {
			Destroy(collision.gameObject);
			
			StartCoroutine(DecreaseScore(2f));
			
        }

    }

	private void OnCollisionEnter2D(Collision2D other)
	{
		// if a player collides with another, push them away from another
		if (other.gameObject.tag == "Player")
		{
			if (other.gameObject.transform.position.y == transform.position.y)
			{
				if (other.gameObject.transform.position.x > transform.position.x)
				{
					// Move player left
					m_Rigidbody2D.velocity = new Vector2(-pushForce, m_Rigidbody2D.velocity.y);
				} 
				else
				{
					// Move player right
					m_Rigidbody2D.velocity = new Vector2(+pushForce, m_Rigidbody2D.velocity.y);
				}
			}
			
		}
	}

	private IEnumerator ResetJumpPower(float waitTime)
	{
		// Add particle effect
		JumpParticle.GetComponent<ParticleSystem>();
		JumpParticle.Play();

		yield return new WaitForSeconds(waitTime);
		m_JumpForce = 600f;

		JumpParticle.Stop();
	}

	private IEnumerator DecreaseScore(float waitTime)
	{
		int i = 0;

		// Decrease the score of all other players by 5
		foreach (GameObject otherPlayer in otherPlayerList) {
			CharacterController2D script = otherPlayer.GetComponent<CharacterController2D>();
			if (script.coins > 0) {
				script.coins = script.coins - 5;
				script.coinText.text = (script.coins).ToString();

				// Attempt at particles. Doesn't work for unknown reason
				decreaseParticles[i].GetComponent<ParticleSystem>().Play();

				// Start timer for power effect
				
			}
			i++;
		}

		yield return new WaitForSeconds(waitTime);

		foreach (ParticleSystem otherScoreParticle in decreaseParticles){
			otherScoreParticle.GetComponent<ParticleSystem>().Stop();
		}

	}

}
