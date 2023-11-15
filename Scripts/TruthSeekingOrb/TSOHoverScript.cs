using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TSOHoverScript : MonoBehaviour
{
    GameObject player;
	TSOBasicAttack tsoBasicAttack;

	Vector3 wherePlayerWasFacing;

	void Start()
	{
		player = GameObject.FindWithTag("Player");
		tsoBasicAttack = GetComponent<TSOBasicAttack>();
	}

    void Update()
    {
		// (If player is attacking, don't switch the direction of the orb, or else it'll switch the direction of its attack mid-way)
		if (!tsoBasicAttack.isTSOBasicAttacking)
			CheckWherePlayerFacing();
		// The following line has the ball constantly moving towards the player at an incredibly slow speed. 
		// However, this speed increases multiplicitively when it's farther away from the player.
		transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x - Mathf.Sign(wherePlayerWasFacing.x)*2,player.transform.position.y + Mathf.Sign(player.transform.localScale.y)*2,gameObject.transform.position.z), (5 + (Mathf.Abs(gameObject.transform.position.x - player.transform.position.x) * 5) + (Mathf.Abs(gameObject.transform.position.y - player.transform.position.y) * 5)) * Time.deltaTime);
		transform.localScale = new Vector3(Mathf.Sign(wherePlayerWasFacing.x),Mathf.Sign(wherePlayerWasFacing.y),gameObject.transform.localScale.z);
    }

	void CheckWherePlayerFacing()
	{
		wherePlayerWasFacing = player.transform.localScale;
	}
}
