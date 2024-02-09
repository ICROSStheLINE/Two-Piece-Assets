using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleporting : MonoBehaviour
{
	Rigidbody2D rb;
	Animator anim;
	PlayerStats playerStats;
	
	static readonly float animationDurationSpeedMultiplier = 1.5f;
	static readonly float animationDuration = 1.083f / animationDurationSpeedMultiplier;
	static readonly float animationFrames = 13f;
	static readonly float teleportFrame = 9f;
	static readonly float secondsUntilTeleport = (teleportFrame / animationFrames) * animationDuration;
	
	//[HideInInspector] public bool playerStats.playerMidTeleport = false;
	[SerializeField] float teleportDistance;
	
    void Start()
    {
		rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
		playerStats = GetComponent<PlayerStats>();
    }

    void FixedUpdate()
    {
        if ((Input.GetKey("r")) && !playerStats.playerMidActionNoDash && !playerStats.midCutscene)
		{
			playerStats.playerCanDash = false;
			playerStats.ResetPlayerDashCooldown();
			playerStats.playerMidTeleport = true;
			playerStats.playerCanMove = false;
			Invoke("Teleport", secondsUntilTeleport);
			Invoke("ResetCooldown", animationDuration);
		}

		anim.SetBool("isTeleporting", playerStats.playerMidTeleport);
    }
	
	void Teleport()
	{
		rb.position = rb.position + new Vector2(teleportDistance * Mathf.Sign(gameObject.transform.localScale.x), 0);
		//gameObject.transform.position = gameObject.transform.position + new Vector3(5 * Mathf.Sign(gameObject.transform.localScale.x),0,0);
	}
	
	void ResetCooldown()
	{
		playerStats.playerMidTeleport = false;
		playerStats.playerCanMove = true;
		playerStats.playerCanDash = true;
	}
}
