using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterBossArena : MonoBehaviour
{
    GameObject cam;
	PlayerTracker cameraPlayerTracker;
	bool alreadyInIceArena = false;

    void Start()
    {
        cam = GameObject.FindWithTag("MainCamera");
		cameraPlayerTracker = cam.GetComponent<PlayerTracker>();
    }


	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Ice Arena Fighting Zone")
		{
			cameraPlayerTracker.Invoke("EnterIceArena", 0f);
			cameraPlayerTracker.CancelInvoke("TrackPlayerAgain");
			if (!alreadyInIceArena)
			{
				cameraPlayerTracker.InvokeRepeating("ActivateCutsceneMode", 0f, 0.1f);
				cameraPlayerTracker.Invoke("DeactivateCutsceneMode", 5f);
				alreadyInIceArena = true;
			}
		}
	}

	void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Ice Arena Fighting Zone")
		{
			cameraPlayerTracker.Invoke("TrackPlayerAgain", 1f);
		}
	}
}
