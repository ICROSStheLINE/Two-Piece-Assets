using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTracker : MonoBehaviour
{
	GameObject player;
	[SerializeField] bool isTrackingPlayer = true;
	PlayerKickingTSO playerKickingTSO;
	PlayerMovement playerMovement;
	PlayerDashing playerDashing;
	Camera cam;
	
	float velocity = 0; // This variable exists for a stupid reason LOL
	float targetZoom = 11.5f;
	Vector3 targetPosition;

	void Start()
	{
		cam = GetComponent<Camera>();
		cam.orthographicSize = targetZoom;
		player = GameObject.FindWithTag("Player");
		playerKickingTSO = player.GetComponent<PlayerKickingTSO>();
		playerMovement = player.GetComponent<PlayerMovement>();
		playerDashing = player.GetComponent<PlayerDashing>();
	}

    void Update()
    {
		if (isTrackingPlayer)
		{
			transform.position = new Vector3(player.transform.position.x,gameObject.transform.position.y,gameObject.transform.position.z);
			if (!playerKickingTSO.playerMidKickingTSOButForTheCameraGameObject)
				transform.position = Vector3.MoveTowards(transform.position, new Vector3(gameObject.transform.position.x,player.transform.position.y + Mathf.Sign(player.transform.localScale.y)*2,gameObject.transform.position.z), 70 * Time.deltaTime);
		}
		else
			transform.position = Vector3.MoveTowards(transform.position, targetPosition, 0.01f);
		
		if (cam.orthographicSize != targetZoom)
		{
			cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, targetZoom, ref velocity, 1.2f);
		}
    }
	
	void ActivateCutsceneMode()
	{
		playerDashing.canDash = false;
		playerDashing.ResetDashCooldown();
		playerMovement.playerCanMove = false;
	}
	
	void DeactivateCutsceneMode()
	{
		playerDashing.canDash = true;
		playerMovement.playerCanMove = true;
	}

	void EnterIceArena()
	{
		isTrackingPlayer = false;
		targetZoom = 15.8f;
		Vector3 collidedObjectCoords = GameObject.FindWithTag("Ice Arena").transform.position;
		targetPosition = new Vector3(collidedObjectCoords.x, collidedObjectCoords.y, gameObject.transform.position.z);
	}
	
	void TrackPlayerAgain()
	{
		isTrackingPlayer = true;
		targetZoom = 11.5f;
	}
}
