using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKickingTSO : MonoBehaviour
{
	Animator anim;
	GameObject truthSeekingOrb;
	TSOBasicAttack tsoBasicAttack;
	PlayerMovement playerMovement;
	BuffyGravityFlip playerGravityFlip;
	PlayerTeleporting playerTeleporting;
	PlayerDashing playerDashing;
	PlayerShielding playerShielding;

	static readonly float kickingAnimationDurationSpeedMultiplier = 1;
	static readonly float kickingAnimationDuration = 0.833f / kickingAnimationDurationSpeedMultiplier;
	static readonly float kickingAnimationFrames = 10;
	static readonly float tsoProjectileSpawn = (5 / kickingAnimationFrames) * kickingAnimationDuration;
	
	[HideInInspector] public bool playerMidKickingTSO = false;
	bool actionOnCooldown = false;

    void Start()
    {
		anim = GetComponent<Animator>();
		truthSeekingOrb = GameObject.FindWithTag("Truth Seeking Orb");
		tsoBasicAttack = truthSeekingOrb.GetComponent<TSOBasicAttack>();
        playerMovement = GetComponent<PlayerMovement>();
		playerGravityFlip = GetComponent<BuffyGravityFlip>();
		playerTeleporting = GetComponent<PlayerTeleporting>();
		playerDashing = GetComponent<PlayerDashing>();
		playerShielding = GetComponent<PlayerShielding>();
    }


    void Update()
    {
        if ((Input.GetKeyDown("h")) && (!playerGravityFlip.playerMidGravityShift) && (!playerTeleporting.playerMidTeleport) && (!playerShielding.playerMidShielding) && (!tsoBasicAttack.isTSOBasicAttacking) && (!actionOnCooldown) && (!playerMidKickingTSO))
		{
			// Kick out ball
			playerDashing.canDash = false;
			playerDashing.ResetDashCooldown();
			playerMovement.playerCanMove = false;
			
			playerMidKickingTSO = true;
			actionOnCooldown = true;
			
			anim.SetBool("kickingTSO", true);
			Invoke("ResetCooldown", kickingAnimationDuration);
		}
		else if ((Input.GetKeyDown("h")) && (!playerGravityFlip.playerMidGravityShift) && (!playerTeleporting.playerMidTeleport) && (!playerShielding.playerMidShielding) && (!tsoBasicAttack.isTSOBasicAttacking) && (!actionOnCooldown) && (playerMidKickingTSO))
		{
			// Teleport to ball
		}
    }
	
	void ResetCooldown()
	{
		playerDashing.canDash = true;
		playerMovement.playerCanMove = true;
		
		anim.SetBool("kickingTSO", false);
		actionOnCooldown = false;
		playerMidKickingTSO = false;
	}
}
