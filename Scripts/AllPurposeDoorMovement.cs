using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllPurposeDoorMovement : MonoBehaviour
{
	[SerializeField] int numberOfDoors;
	
	GameObject[] arenaEntranceGates;

	bool gateMustBeClosed = false;

	Vector3[] gateTargetPoses;
	
	[SerializeField] string doorName;

    void Start()
    {
		arenaEntranceGates = new GameObject[numberOfDoors];
		gateTargetPoses = new Vector3[numberOfDoors];
		
		for (int i = 0; i < arenaEntranceGates.Length; i++)
		{
			arenaEntranceGates[i] = gameObject.transform.Find(doorName + (i+1)).gameObject;
			gateTargetPoses[i] = arenaEntranceGates[i].transform.position + ((-arenaEntranceGates[i].transform.up) * 9);
		}
    }

	void Update()
	{
		if (gateMustBeClosed)
		{
			for (int i = 0; i < arenaEntranceGates.Length; i++)
			{
				Vector3 gateCurrentPos = arenaEntranceGates[i].transform.position;
				arenaEntranceGates[i].transform.position = Vector3.MoveTowards(gateCurrentPos, gateTargetPoses[i], 5f * Time.deltaTime);
			}
		}
	}
	
	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			gateMustBeClosed = true;
		}
	}
}
