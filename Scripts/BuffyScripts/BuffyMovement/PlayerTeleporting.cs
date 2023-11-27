using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleporting : MonoBehaviour
{
	Rigidbody2D rb;
	Animator anim;
	PlayerDashing playerDashing;
	BuffyGravityFlip playerGravityFlip;
	PlayerMovement playerMovement;
	PlayerShielding playerShielding;
	PlayerKickingTSO playerKickingTSO;
	BuffyLeechBlast buffyLeechBlast;
	
	[HideInInspector] public bool playerMidTeleport = false;
	[SerializeField] float teleportDistance;
	
    void Start()
    {
		rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
		playerDashing = GetComponent<PlayerDashing>();
		playerGravityFlip = GetComponent<BuffyGravityFlip>();
		playerMovement = GetComponent<PlayerMovement>();
		playerShielding = GetComponent<PlayerShielding>();
		playerKickingTSO = GetComponent<PlayerKickingTSO>();
		buffyLeechBlast = GetComponent<BuffyLeechBlast>();
    }

    void FixedUpdate()
    {
        if ((Input.GetKey("r")) && (!playerMidTeleport) && (playerGravityFlip.playerMidGravityShift == false) && (!playerShielding.playerMidShielding) && (!playerKickingTSO.playerMidKickingTSOButForTheCameraGameObject) && (!buffyLeechBlast.playerMidLeechBlast))
		{
			playerDashing.canDash = false;
			playerDashing.ResetDashCooldown();
			playerMidTeleport = true;
			playerMovement.playerCanMove = false;
			Invoke("Teleport", (0.75f/2));
			Invoke("ResetCooldown", (1.083f/2));
		}

		anim.SetBool("isTeleporting", playerMidTeleport);
    }
	
	void Teleport()
	{
		rb.position = rb.position + new Vector2(teleportDistance * Mathf.Sign(gameObject.transform.localScale.x), 0);
		//gameObject.transform.position = gameObject.transform.position + new Vector3(5 * Mathf.Sign(gameObject.transform.localScale.x),0,0);
	}
	
	void ResetCooldown()
	{
		playerMidTeleport = false;
		playerMovement.playerCanMove = true;
		playerDashing.canDash = true;
	}
}
