using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffyGravityFlip : MonoBehaviour
{
	Rigidbody2D rb;
	SpriteRenderer spriteRenderer;
	Animator anim;
	PlayerStats playerStats;
	
	PlayerKickingTSO playerKickingTSO;
	BuffyLeechBlast buffyLeechBlast;
	
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		anim = GetComponent<Animator>();
		playerStats = GetComponent<PlayerStats>();
		
		playerKickingTSO = GetComponent<PlayerKickingTSO>();
		buffyLeechBlast = GetComponent<BuffyLeechBlast>();
    }

    void Update()
    {
		if ((Input.GetKeyDown("f")) && (!playerStats.playerMidGravityShift) && (!playerStats.playerMidTeleport) && (!playerStats.playerMidShielding) && (!playerKickingTSO.playerMidKickingTSOButForTheCameraGameObject) && (!buffyLeechBlast.playerMidLeechBlast))
		{
			playerStats.playerCanDash = false;
			playerStats.ResetPlayerDashCooldown();
			playerStats.playerMidGravityShift = true;
			playerStats.playerCanMove = false;
			Invoke("GravityInverse", 0.375f);
			Invoke("ResetCooldown", 0.75f);
		}

		anim.SetBool("isGravityShifting", playerStats.playerMidGravityShift);
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
		playerStats.playerMidGravityShift = false;
		spriteRenderer.flipY = !spriteRenderer.flipY;
		playerStats.playerCanMove = true;
		playerStats.playerCanDash = true;
	}
}
