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
		if (!tsoBasicAttack.isTSOBasicAttacking)
			CheckWherePlayerFacing();
		transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x - Mathf.Sign(wherePlayerWasFacing.x)*2,player.transform.position.y + Mathf.Sign(player.transform.localScale.y)*2,gameObject.transform.position.z), 50 * Time.deltaTime);
		transform.localScale = new Vector3(Mathf.Sign(wherePlayerWasFacing.x),Mathf.Sign(wherePlayerWasFacing.y),gameObject.transform.localScale.z);
    }
	
	void CheckWherePlayerFacing()
	{
		wherePlayerWasFacing = player.transform.localScale;
	}
}
