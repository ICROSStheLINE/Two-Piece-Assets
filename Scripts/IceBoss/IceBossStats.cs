using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBossStats : MonoBehaviour
{
	IceBossBehaviour iceBossBehaviour;
	IceBossJaw iceBossJaw;
	GameObject bossHead;

	// Basic Stats
	public float iceBossTimeBetweenPatterns = 1.6f;
	public float iceBossOrbChargeTime = 1f;
	
	// Idle Variables
	public bool iceBossIsAwake = false;
	[HideInInspector] public bool iceBossIdling = true;
	
	// Basic Attack Variables
	[HideInInspector] public bool iceBossMidAttack = false;
	
	// Energy Orb Variables
	public bool iceBossAttemptOrb = false;
	[HideInInspector] public bool iceBossMidOrb = false;
	
	// Pattern Variables
	[HideInInspector] public bool iceBossPerformingPattern = false;
	[HideInInspector] public int iceBossSpecialPattern = 0;
	[HideInInspector] public float iceBossSpecialPatternStage = 0;
	


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
		// InvokeRepeating("ActivateAttack", 6f, 1.8f);
		// Activate cutscene
		// Activate aggro
	}


	void Update()
	{
		if (Input.GetKeyDown("m"))
			iceBossBehaviour.Invoke("ActivateLaser", 0f);
		
		if (Input.GetKeyDown("n"))
			iceBossBehaviour.Invoke("ActivateOrb", 0f);
		
		if (Input.GetKeyDown("l"))
			iceBossBehaviour.Invoke("PatternOne", 0f);
		
		if (Input.GetKeyDown("k"))
			iceBossBehaviour.Invoke("PatternTwo", 0f);
		
		if (Input.GetKeyDown("j"))
			iceBossBehaviour.Invoke("SpecialPatternOne", 0f);
	}
}
