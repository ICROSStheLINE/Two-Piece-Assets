using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffyGravityFlip : MonoBehaviour
{
	Rigidbody2D rb;
	SpriteRenderer spriteRenderer;
	Animator anim;
	PlayerMovement playerMovement;
	PlayerDashing playerDashing;
	PlayerTeleporting playerTeleporting;
	PlayerShielding playerShielding;
	PlayerKickingTSO playerKickingTSO;
	BuffyLeechBlast buffyLeechBlast;

	[HideInInspector] public bool playerMidGravityShift = false;
	
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		anim = GetComponent<Animator>();
		playerMovement = GetComponent<PlayerMovement>();
		playerDashing = GetComponent<PlayerDashing>();
		playerTeleporting = GetComponent<PlayerTeleporting>();
		playerShielding = GetComponent<PlayerShielding>();
		playerKickingTSO = GetComponent<PlayerKickingTSO>();
		buffyLeechBlast = GetComponent<BuffyLeechBlast>();
    }

    void Update()
    {
		if ((Input.GetKeyDown("f")) && (!playerMidGravityShift) && (!playerTeleporting.playerMidTeleport) && (!playerShielding.playerMidShielding) && (!playerKickingTSO.playerMidKickingTSOButForTheCameraGameObject) && (!buffyLeechBlast.playerMidLeechBlast))
		{
			playerDashing.canDash = false;
			playerDashing.ResetDashCooldown();
			playerMidGravityShift = true;
			playerMovement.playerCanMove = false;
			Invoke("GravityInverse", 0.375f);
			Invoke("ResetCooldown", 0.75f);
		}

		anim.SetBool("isGravityShifting", playerMidGravityShift);
    }

	void GravityInverse()
	{
		rb.gravityScale *= -100;
		gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x,gameObject.transform.localScale.y * -1,gameObject.transform.localScale.z);
		spriteRenderer.flipY = !spriteRenderer.flipY;
		Invoke("GravityReset", 0.1f);
	}

	void GravityReset()
	{
		rb.gravityScale /= 100;
	}

	void ResetCooldown()
	{
		playerMidGravityShift = false;
		spriteRenderer.flipY = !spriteRenderer.flipY;
		playerMovement.playerCanMove = true;
		playerDashing.canDash = true;
	}
}
