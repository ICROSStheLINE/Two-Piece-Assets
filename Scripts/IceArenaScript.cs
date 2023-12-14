using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceArenaScript : MonoBehaviour
{
	GameObject[] arenaEntranceGates = new GameObject[4];

	bool gateMustBeClosed = false;
	Vector3 gateCurrentPos;

	Vector3[] gateTargetPoses = new Vector3[4];

    void Start()
    {
		for (int i = 0; i < arenaEntranceGates.Length; i++)
		{
			arenaEntranceGates[i] = gameObject.transform.Find("Icicle Door" + (i+1)).gameObject;
			gateTargetPoses[i] = arenaEntranceGates[i].transform.position + ((-arenaEntranceGates[i].transform.up) * 9);
		}
    }

	void Update()
	{
		if (gateMustBeClosed)
		{
			for (int i = 0; i < arenaEntranceGates.Length; i++)
			{
				gateCurrentPos = arenaEntranceGates[i].transform.position;
				arenaEntranceGates[i].transform.position = Vector3.MoveTowards(gateCurrentPos, gateTargetPoses[i], 2f * Time.deltaTime);
			}
		}
	}

    void CloseEntranceGate()
	{
		gateMustBeClosed = true;
	}
}
