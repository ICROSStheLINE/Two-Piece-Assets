using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleporting : MonoBehaviour
{
	Rigidbody2D rb;
	Animator anim;
	PlayerStats playerStats;
	
	PlayerKickingTSO playerKickingTSO;
	BuffyLeechBlast buffyLeechBlast;
	
	//[HideInInspector] public bool playerStats.playerMidTeleport = false;
	[SerializeField] float teleportDistance;
	
    void Start()
    {
		rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
		playerStats = GetComponent<PlayerStats>();
		
		playerKickingTSO = GetComponent<PlayerKickingTSO>();
		buffyLeechBlast = GetComponent<BuffyLeechBlast>();
    }

    void FixedUpdate()
    {
        if ((Input.GetKey("r")) && (!playerStats.playerMidTeleport) && (playerStats.playerMidGravityShift == false) && (!playerStats.playerMidShielding) && (!playerKickingTSO.playerMidKickingTSOButForTheCameraGameObject) && (!buffyLeechBlast.playerMidLeechBlast))
		{
			playerStats.playerCanDash = false;
			playerStats.ResetPlayerDashCooldown();
			playerStats.playerMidTeleport = true;
			playerStats.playerCanMove = false;
			Invoke("Teleport", (0.75f/2));
			Invoke("ResetCooldown", (1.083f/2));
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
