using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBossStats : MonoBehaviour
{
	IceBossBehaviour iceBossBehaviour;
	IceBossJaw iceBossJaw;
	GameObject bossHead;

	public bool iceBossIsAwake = false;
	[HideInInspector] public bool iceBossIdling = true;
	
	public bool iceBossAttemptAttack = false;
	[HideInInspector] public bool iceBossMidAttack = false;
	
	public bool iceBossAttemptLaser = false;
	
	public bool iceBossAttemptOrb = false;
	

    void Start()
    {
        iceBossBehaviour = GetComponentInChildren<IceBossBehaviour>();
		iceBossJaw = GetComponentInChildren<IceBossJaw>();
		bossHead = transform.GetChild(0).gameObject;
    }

	// Used in "EnterBossArena.cs" script
	public void WakeUpIceBoss()
	{
		iceBossIsAwake = true;
		InvokeRepeating("ActivateAttack", 6f, 1.8f);
		// Activate cutscene
		// Activate aggro
	}

	void ActivateAttack()
	{
		iceBossAttemptAttack = true;
	}
	
	void ActivateLaser()
	{
		iceBossAttemptLaser = true;
	}
	
	void ActivateOrb()
	{
		iceBossAttemptOrb = true;
	}

	void Update()
	{
		if (Input.GetKeyDown("m"))
			ActivateLaser();
	}
}
