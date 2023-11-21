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

	static readonly float animationDurationMultiplier = 1;
	static readonly float animationDuration = 2.25f / animationDurationMultiplier;
	//static readonly float animationFrames = 27;
	//static readonly float blastProjectileSpawn = 0;

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
			playerMidLeechBlast = true;
			anim.SetBool("isLeechBlasting", true);
			Invoke("ResetCooldown", animationDuration);
		}
    }

	void ResetCooldown()
	{
		playerMidLeechBlast = false;
		anim.SetBool("isLeechBlasting", false);
	}
}
