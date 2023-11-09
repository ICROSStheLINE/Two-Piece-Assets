using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTracker : MonoBehaviour
{
	[SerializeField] GameObject playerObject;
	[SerializeField] bool isTrackingPlayer = true;

    void Update()
    {
		if (isTrackingPlayer)
		{
			transform.position = new Vector3(playerObject.transform.position.x,gameObject.transform.position.y,gameObject.transform.position.z);
			transform.position = Vector3.MoveTowards(transform.position, new Vector3(gameObject.transform.position.x,playerObject.transform.position.y + Mathf.Sign(playerObject.transform.localScale.y)*2,gameObject.transform.position.z), 50 * Time.deltaTime);
			
		}
    }
}
