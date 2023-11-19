using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShielding : MonoBehaviour
{
	Animator anim;
	[SerializeField] GameObject theHitbox;
	PlayerMovement playerMovement;
	BuffyGravityFlip playerGravityFlip;
	PlayerTeleporting playerTeleporting;
	PlayerDashing playerDashing;
	PlayerKickingTSO playerKickingTSO;

	readonly float shieldingStageOneSpeed = (0.5f / 2);
	readonly float shieldingStageTwoSpeed = (0.5f / 2);

	[HideInInspector] public bool playerMidShielding = false;
	bool playerShieldIsUp = false;

    void Start()
    {
        anim = GetComponent<Animator>();
		playerMovement = GetComponent<PlayerMovement>();
		playerGravityFlip = GetComponent<BuffyGravityFlip>();
		playerTeleporting = GetComponent<PlayerTeleporting>();
		playerDashing = GetComponent<PlayerDashing>();
		playerKickingTSO = GetComponent<PlayerKickingTSO>();
    }

    void Update()
    {
        if ((Input.GetKey("v")) && (!playerGravityFlip.playerMidGravityShift) && (!playerTeleporting.playerMidTeleport) && (!playerMidShielding) && (!playerKickingTSO.playerMidKickingTSOButForTheCameraGameObject))
		{
			playerDashing.canDash = false;
			playerDashing.ResetDashCooldown();
			playerMovement.playerCanMove = false;


			ShieldStartup();
			Invoke("ShieldMiddle", shieldingStageOneSpeed);
		}
		else if ((!Input.GetKey("v")) && (!playerGravityFlip.playerMidGravityShift) && (!playerTeleporting.playerMidTeleport) && (playerShieldIsUp))
		{
			ShieldEnd();
			Invoke("FinishShielding", shieldingStageTwoSpeed);
		}
    }

	void ShieldStartup()
	{
		anim.SetInteger("shieldingStage", 1);
		playerMidShielding = true;
	}

	void ShieldMiddle()
	{
		anim.SetInteger("shieldingStage", 2);
		playerShieldIsUp = true;
		GameObject referenceObject = Instantiate(theHitbox, gameObject.transform.position + new Vector3(1f * Mathf.Sign(gameObject.transform.localScale.x),0,0), gameObject.transform.rotation);
		referenceObject.transform.parent = gameObject.transform;
	}

	void ShieldEnd()
	{
		anim.SetInteger("shieldingStage", 3);
		playerShieldIsUp = false;
		Destroy(GameObject.FindWithTag("PlayerShieldHitbox"));
	}

	void FinishShielding()
	{
		playerDashing.canDash = true;
		playerMovement.playerCanMove = true;

		playerMidShielding = false;
		anim.SetInteger("shieldingStage", 0);
	}
}
