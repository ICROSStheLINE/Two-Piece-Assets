using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKickingTSO : MonoBehaviour
{
	Animator anim;
	Rigidbody2D rb;
	GameObject truthSeekingOrb;
	[SerializeField] GameObject truthSeekingOrbPrefab;
	GameObject tsoBeingKicked;
	[SerializeField] GameObject tsoBeingKickedPrefab;
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

	static readonly float teleportingAnimationDurationSpeedMultiplier = 1;
	static readonly float teleportingAnimationDuration = 0.667f / teleportingAnimationDurationSpeedMultiplier;
//	static readonly float teleportingAnimationFrames = 8;

	[HideInInspector] public bool playerMidKickingTSO = false;
	bool ableToTeleport = false;
	[HideInInspector] public bool playerMidKickingTSOButForTheCameraGameObject = false;

    void Start()
    {
		anim = GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D>();
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
		// Kick out ball
        if ((Input.GetKeyDown("h")) && (!playerGravityFlip.playerMidGravityShift) && (!playerTeleporting.playerMidTeleport) && (!playerShielding.playerMidShielding) && (!tsoBasicAttack.isTSOBasicAttacking) && (anim.GetFloat("verticalVelocity") == 0f) && (!ableToTeleport) && (!playerMidKickingTSO))
		{
			playerMidKickingTSOButForTheCameraGameObject = true;
			
			// Cancel Movement
			playerDashing.canDash = false;
			playerDashing.ResetDashCooldown();
			playerMovement.playerCanMove = false;
			
			playerMidKickingTSO = true;
			
			// Animate
			anim.SetBool("kickingTSO", true);
			Invoke("SpawnOrbProjectile", tsoProjectileSpawn);
			Invoke("ResetAbilityToTPCooldown", tsoProjectileSpawn + 1f);
			Invoke("ResetCooldown", kickingAnimationDuration);
		}
		// Teleport to ball
		else if ((Input.GetKeyDown("h")) && (!playerGravityFlip.playerMidGravityShift) && (!playerTeleporting.playerMidTeleport) && (!playerShielding.playerMidShielding) && (!tsoBasicAttack.isTSOBasicAttacking) && (anim.GetFloat("verticalVelocity") == 0f) && (ableToTeleport))
		{
			// Cancel Movement
			playerDashing.canDash = false;
			playerDashing.ResetDashCooldown();
			playerMovement.playerCanMove = false;
			
			CancelInvoke("ResetCooldown");
			CancelInvoke("ResetAbilityToTPCooldown");
			ableToTeleport = false;
			// Animate
			anim.SetBool("kickingTSO", false);
			anim.SetBool("kickingTSOP2", true);
			
			Invoke("ResetCooldown", teleportingAnimationDuration);
			TeleportToBall();
			FreezeConstraints();
			Invoke("UnfreezeConstraints", teleportingAnimationDuration + 0.2f);
		}
    }


	void UnfreezeConstraints()
	{
		rb.constraints = RigidbodyConstraints2D.FreezeRotation;
	}

	void FreezeConstraints()
	{
		rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
	}

	void TeleportToBall()
	{
		gameObject.transform.position = tsoBeingKicked.transform.position;
		Destroy(tsoBeingKicked);
	}

	void SpawnOrbProjectile()
	{
		ableToTeleport = true;
		if (Mathf.Sign(gameObject.transform.localScale.x) == 1)
		{
			tsoBeingKicked = Instantiate(tsoBeingKickedPrefab, (gameObject.transform.position + new Vector3(2,0,0)), Quaternion.Euler(0,0,0));
		}
		else
		{
			tsoBeingKicked = Instantiate(tsoBeingKickedPrefab, (gameObject.transform.position + new Vector3(-2,0,0)), Quaternion.Euler(0,0,180));
		}
	}

	void ResetAbilityToTPCooldown()
	{
		ableToTeleport = false;
	}

	void ResetCooldown()
	{
		playerDashing.canDash = true;
		playerMovement.playerCanMove = true;
		
		anim.SetBool("kickingTSO", false);
		anim.SetBool("kickingTSOP2", false);
		playerMidKickingTSO = false;
		
		Invoke("ResetPlayerMidKickingTSOButForTheCameraGameObjectVariable", 0.2f);
	}
	
	void ResetPlayerMidKickingTSOButForTheCameraGameObjectVariable()
	{
		playerMidKickingTSOButForTheCameraGameObject = false;
	}
}
