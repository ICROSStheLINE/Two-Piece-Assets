using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTracker : MonoBehaviour
{
	GameObject player;
	[SerializeField] bool isTrackingPlayer = true;

	void Start()
	{
		player = GameObject.FindWithTag("Player");
	}

    void Update()
    {
		if (isTrackingPlayer)
		{
			transform.position = new Vector3(player.transform.position.x,gameObject.transform.position.y,gameObject.transform.position.z);
			transform.position = Vector3.MoveTowards(transform.position, new Vector3(gameObject.transform.position.x,player.transform.position.y + Mathf.Sign(player.transform.localScale.y)*2,gameObject.transform.position.z), 70 * Time.deltaTime);
			
		}
    }
}
