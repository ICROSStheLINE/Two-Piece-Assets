using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	Rigidbody2D rb;
	Animator anim;
	SpriteRenderer spriteRenderer;
	
	[SerializeField] float movementSpeed = 7f;
	float force = 0f;
	float playerXScale;
	public bool playerCanMove = true;
	PlayerDashing playerDashing;
	PlayerTeleporting playerTeleporting;
	PlayerShielding playerShielding;
	BuffyGravityFlip playerGravityFlip;

	///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		playerXScale = gameObject.transform.localScale.x;
		playerDashing = GetComponent<PlayerDashing>();
		playerTeleporting = GetComponent<PlayerTeleporting>();
		playerShielding = GetComponent<PlayerShielding>();
		playerGravityFlip = GetComponent<BuffyGravityFlip>();
    }

    // Use FixedUpdate instead of Update because FixedUpdate is more friendly with Rigidbody2D physics
    void FixedUpdate()
    {
		CheckNormalMovement();
		BeginMovement();
		
		CheckIfFallingAndAnimateAccordingly();
    }

	void CheckNormalMovement()
	{
		if (playerCanMove)
		{
			if (Input.GetKey("d"))
			{
				force += movementSpeed * Time.deltaTime;
				if (!playerDashing.isDashingButResets1MillisecondEarlier)
				{
					gameObject.transform.localScale = new Vector3(playerXScale,gameObject.transform.localScale.y,playerXScale);
				}
			}
			if (Input.GetKey("a"))
			{
				force -= movementSpeed * Time.deltaTime;
				if (!playerDashing.isDashingButResets1MillisecondEarlier)
				{
					gameObject.transform.localScale = new Vector3(-playerXScale,gameObject.transform.localScale.y,playerXScale);
				}
			}
		}
	}
	
	void BeginMovement()
	{
		// P.S. Don't combine transform.position with rigidbody stuff LOL
		rb.position += new Vector2(force, 0);

		anim.SetFloat("FORCE", Mathf.Abs(force));

		force = 0;
	}
	
	void CheckIfFallingAndAnimateAccordingly()
	{
		anim.SetFloat("verticalVelocity", Mathf.Abs(rb.velocity.y));
		
		if ((playerDashing.isDashing) || (playerGravityFlip.playerMidGravityShift) || (playerTeleporting.playerMidTeleport) || (playerShielding.playerMidShielding))
			anim.SetFloat("verticalVelocity", 0f);
	}
}
