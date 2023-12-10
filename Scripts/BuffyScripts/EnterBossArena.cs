using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterBossArena : MonoBehaviour
{
    GameObject cam;
	PlayerTracker cameraPlayerTracker;
	
    void Start()
    {
        cam = GameObject.FindWithTag("MainCamera");
		cameraPlayerTracker = cam.GetComponent<PlayerTracker>();
    }


	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Ice Arena")
		{
			cameraPlayerTracker.Invoke("EnterIceArena", 0f);
			cameraPlayerTracker.Invoke("ActivateCutsceneMode", 0f);
			cameraPlayerTracker.Invoke("DeactivateCutsceneMode", 5f);
		}
	}

	void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Ice Arena")
		{
			cameraPlayerTracker.Invoke("TrackPlayerAgain", 0f);
		}
	}
}
