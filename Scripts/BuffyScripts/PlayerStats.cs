using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
	Rigidbody2D rb;
	Animator anim;

	[HideInInspector] public float playerMovementSpeed = 7f;
	[HideInInspector] public bool playerCanMove = true;
	
	[HideInInspector] public bool playerIsDashing = false;
	[HideInInspector] public bool playerIsDashingButResets1MillisecondEarlier = false;
	[HideInInspector] public bool playerCanDash = true;
	
	[HideInInspector] public bool playerMidGravityShift = false;
	
	[HideInInspector] public bool playerMidTeleport = false;
	
	[HideInInspector] public bool playerMidShielding = false;
	
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
	}

    public void ResetPlayerDashCooldown()
	{
		playerIsDashing = false;
		anim.SetBool("isDashing", false);
		rb.velocity = new Vector2(0,0);
	}
	
	void ResetTheIsDashingButResets1MillisecondEarlierVariableSoThatTurningWhileDashingIsntGlitched()
	{
		playerIsDashingButResets1MillisecondEarlier = false;
	}
}
