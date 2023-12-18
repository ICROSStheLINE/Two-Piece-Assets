using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffyLeechBlast : MonoBehaviour
{
	Animator anim;
	TSOBasicAttack tsoBasicAttack;
	PlayerStats playerStats;

	PlayerKickingTSO playerKickingTSO;
	[SerializeField] GameObject projectilePrefab;

	static readonly float animationDurationMultiplier = 1f;
	static readonly float animationDuration = 2.25f / animationDurationMultiplier;
	static readonly float animationFrames = 27f;
	static readonly float blastProjectileSpawn = (27f / animationFrames) * animationDuration;
	static readonly float animationWDurationMultiplier = 1f;
	static readonly float animationWDuration = 0.917f / animationWDurationMultiplier;
	static readonly float animationLDurationMultiplier = 1f;
	static readonly float animationLDuration = 0.5f / animationLDurationMultiplier;

	[HideInInspector] public bool playerMidLeechBlast = false;


    void Start()
    {
        anim = GetComponent<Animator>();
		playerStats = GetComponent<PlayerStats>();
		
		playerKickingTSO = GetComponent<PlayerKickingTSO>();
    }

    void Update()
    {
        if ((Input.GetKeyDown("b")) && (!playerStats.playerMidGravityShift) && (!playerStats.playerMidTeleport) && (!playerStats.playerMidShielding) && (anim.GetFloat("verticalVelocity") == 0f) && (!playerKickingTSO.playerMidKickingTSO) && (!playerMidLeechBlast))
		{
			playerStats.playerCanDash = false;
			playerStats.ResetPlayerDashCooldown();
			playerStats.playerCanMove = false;
			
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
		playerStats.playerCanDash = true;
		playerStats.playerCanMove = true;
		
		playerMidLeechBlast = false;
		anim.SetBool("isLeechBlasting", false);
		anim.SetBool("isLeechBlastingW", false);
		anim.SetBool("isLeechBlastingL", false);
	}
}
