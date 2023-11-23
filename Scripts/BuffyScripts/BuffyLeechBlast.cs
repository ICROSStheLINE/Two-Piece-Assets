using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffyLeechBlast : MonoBehaviour
{
	Animator anim;
	TSOBasicAttack tsoBasicAttack;
	PlayerMovement playerMovement;
	BuffyGravityFlip playerGravityFlip;
	PlayerTeleporting playerTeleporting;
	PlayerDashing playerDashing;
	PlayerShielding playerShielding;
	PlayerKickingTSO playerKickingTSO;
	[SerializeField] GameObject projectilePrefab;

	static readonly float animationDurationMultiplier = 1f;
	static readonly float animationDuration = 2.25f / animationDurationMultiplier;
	static readonly float animationFrames = 27f;
	static readonly float blastProjectileSpawn = (27f / animationFrames) * animationDuration;
	static readonly float animationWDurationMultiplier = 1f;
	static readonly float animationWDuration = 0.917f / animationDurationMultiplier;
	static readonly float animationLDurationMultiplier = 1f;
	static readonly float animationLDuration = 0.5f / animationDurationMultiplier;

	[HideInInspector] public bool playerMidLeechBlast = false;


    void Start()
    {
        anim = GetComponent<Animator>();
		playerMovement = GetComponent<PlayerMovement>();
		playerGravityFlip = GetComponent<BuffyGravityFlip>();
		playerTeleporting = GetComponent<PlayerTeleporting>();
		playerDashing = GetComponent<PlayerDashing>();
		playerShielding = GetComponent<PlayerShielding>();
		playerKickingTSO = GetComponent<PlayerKickingTSO>();
    }

    void Update()
    {
        if ((Input.GetKeyDown("b")) && (!playerGravityFlip.playerMidGravityShift) && (!playerTeleporting.playerMidTeleport) && (!playerShielding.playerMidShielding) && (anim.GetFloat("verticalVelocity") == 0f) && (!playerKickingTSO.playerMidKickingTSO) && (!playerMidLeechBlast))
		{
			playerDashing.canDash = false;
			playerDashing.ResetDashCooldown();
			playerMovement.playerCanMove = false;
			
			playerMidLeechBlast = true;
			anim.SetBool("isLeechBlasting", true);
			
			Invoke("SpawnProjectile", blastProjectileSpawn);

		}
    }
	
	void SpawnProjectile()
	{
		if (Mathf.Sign(gameObject.transform.localScale.x) == 1)
		{
			Instantiate(projectilePrefab, (gameObject.transform.position + new Vector3(0,0,0)), Quaternion.Euler(0,0,0));
		}
		else
		{
			Instantiate(projectilePrefab, (gameObject.transform.position + new Vector3(0,0,0)), Quaternion.Euler(0,0,180));
		}
	}

	void AnimateL()
	{
		anim.SetBool("isLeechBlastingL", true);
		anim.SetBool("isLeechBlasting", false);
		Invoke("ResetCooldown", animationLDuration);
	}

	void AnimateW()
	{
		anim.SetBool("isLeechBlastingW", true);
		anim.SetBool("isLeechBlasting", false);
		Invoke("ResetCooldown", animationWDuration);
	}

	void ResetCooldown()
	{
		playerDashing.canDash = true;
		playerMovement.playerCanMove = true;
		
		playerMidLeechBlast = false;
		anim.SetBool("isLeechBlasting", false);
		anim.SetBool("isLeechBlastingW", false);
		anim.SetBool("isLeechBlastingL", false);
	}
}
