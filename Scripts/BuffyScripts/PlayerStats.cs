using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
	Rigidbody2D rb;
	Animator anim;
	
	[HideInInspector] public bool playerMidActionNoDash = false;
	
	[HideInInspector] public float playerMovementSpeed = 7f;
	[HideInInspector] public bool playerCanMove = true;
	
	[HideInInspector] public bool playerIsDashing = false;
	[HideInInspector] public bool playerIsDashingButResets1MillisecondEarlier = false;
	[HideInInspector] public bool playerCanDash = true;
	
	[HideInInspector] public bool playerMidGravityShift = false;
	
	[HideInInspector] public bool playerMidTeleport = false;
	[HideInInspector] public bool playerQueuingTeleport = false;
	
	[HideInInspector] public bool playerMidShielding = false;
	
	[HideInInspector] public bool playerMidKickingTSO = false;
	[HideInInspector] public bool playerMidKickingTSOButForTheCameraGameObject = false;
	
	[HideInInspector] public bool playerMidLeechBlast = false;
	
	[HideInInspector] public bool midCutscene = false;
	
	[HideInInspector] public bool isTSOBasicAttacking = false;
	
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
	}

	void Update()
	{
		if (playerMidGravityShift || playerMidTeleport || playerQueuingTeleport || playerMidShielding || playerMidKickingTSO || playerMidLeechBlast || playerMidKickingTSOButForTheCameraGameObject)
			playerMidActionNoDash = true;
		else
			playerMidActionNoDash = false;
	}

	// PlayerDashing
    public void ResetPlayerDashCooldown()
	{
		playerIsDashing = false;
		anim.SetBool("isDashing", false);
		rb.velocity = new Vector2(0,0);
	}
	// PlayerDashing
	void ResetTheIsDashingButResets1MillisecondEarlierVariableSoThatTurningWhileDashingIsntGlitched()
	{
		playerIsDashingButResets1MillisecondEarlier = false;
	}
	// PlayerKickingTSO
	void ResetPlayerMidKickingTSOButForTheCameraGameObjectVariable()
	{
		playerMidKickingTSOButForTheCameraGameObject = false;
	}
	
	
}
