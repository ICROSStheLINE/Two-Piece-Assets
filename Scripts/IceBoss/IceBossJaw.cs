using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBossJaw : MonoBehaviour
{
    IceBossStats iceBossStats;
	GameObject bossHead;
	
	Vector3 velocity = Vector3.zero; // This variable exists for a stupid reason LOL
	float followSpeed = 0.1f;

    void Start()
    {
        iceBossStats = transform.parent.GetComponent<IceBossStats>();
		bossHead = gameObject.transform.parent.transform.GetChild(0).gameObject;
    }

    void Update()
    {
		Vector3 targetPosition = bossHead.transform.position - new Vector3(0,6,0);
		
		if (targetPosition.x - transform.position.x < -1) // If the target position is to the LEFT of this gameobjects position
			transform.rotation = Quaternion.Euler(0,0,30);
		else if (targetPosition.x - transform.position.x > 1) // If the target position is to the RIGHT of this gameobjects position
			transform.rotation = Quaternion.Euler(0,0,330);
		else
			transform.rotation = Quaternion.Euler(0,0,0);
		
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, followSpeed);
    }
}
