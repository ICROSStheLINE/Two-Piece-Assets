using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleporting : MonoBehaviour
{
	Rigidbody2D rb;
	Animator anim;
	PlayerStats playerStats;
	SpriteRenderer playerSpriteRenderer;
	
	static readonly float animationDurationSpeedMultiplier = 1.5f;
	static readonly float animationDuration = 1.083f / animationDurationSpeedMultiplier;
	static readonly float animationFrames = 13f;
	static readonly float teleportFrame = 9f;
	static readonly float secondsUntilTeleport = (teleportFrame / animationFrames) * animationDuration;
	
	//[HideInInspector] public bool playerStats.playerMidTeleport = false;
	[SerializeField] float teleportDistance;
	float teleportHeight = 0;
	
	[SerializeField] GameObject teleportIndicatorPrefab;
	GameObject teleportIndicator;
	
	
    void Start()
    {
		rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
		playerStats = GetComponent<PlayerStats>();
		playerSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        if (Input.GetKey("r"))
		{
			if (Input.GetKey("w") && Input.GetKey("s"))
				teleportHeight = 0;
			else if (Input.GetKey("w"))
				teleportHeight = teleportDistance/2;
			else if (Input.GetKey("s"))
				teleportHeight = teleportDistance/2 * -1;
			else 
				teleportHeight = 0;
			
			if (!playerStats.playerMidActionNoDash && !playerStats.midCutscene)
			{
				playerStats.playerQueuingTeleport = true;
				playerSpriteRenderer.color = new Color(1f,0.5f,1f,1f);
			
				if (teleportIndicator == null)
					teleportIndicator = Instantiate(teleportIndicatorPrefab, transform.position + new Vector3(teleportDistance * Mathf.Sign(gameObject.transform.localScale.x),teleportHeight,0), transform.rotation);
			}
			else if (playerStats.playerQueuingTeleport)
				teleportIndicator.transform.position = transform.position + new Vector3(teleportDistance * Mathf.Sign(gameObject.transform.localScale.x),teleportHeight,0);
			
		}
		else if (playerStats.playerQueuingTeleport)
		{
			playerStats.playerQueuingTeleport = false;
			
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
		transform.position = teleportIndicator.transform.position;
		Destroy(teleportIndicator);
		teleportIndicator = null;
		/*rb.position = rb.position + new Vector2(teleportDistance * Mathf.Sign(gameObject.transform.localScale.x), 0);
		gameObject.transform.position = gameObject.transform.position + new Vector3(5 * Mathf.Sign(gameObject.transform.localScale.x),0,0);*/
		
		playerSpriteRenderer.color = new Color(1f,1f,1f,1f);
	}

	void ResetCooldown()
	{
		playerStats.playerMidTeleport = false;
		playerStats.playerCanMove = true;
		playerStats.playerCanDash = true;
	}
}
