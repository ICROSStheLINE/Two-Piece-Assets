using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBossStats : MonoBehaviour
{
	IceBossBehaviour iceBossBehaviour;
	IceBossJaw iceBossJaw;

	public bool iceBossShouldMoveAround = false;
	public bool iceBossIdling = true;
	
	public bool iceBossAttemptAttack = false;
	public bool iceBossMidAttack = false;

    void Start()
    {
        iceBossBehaviour = GetComponentInChildren<IceBossBehaviour>();
		iceBossJaw = GetComponentInChildren<IceBossJaw>();
    }
	
	// Used in "EnterBossArena.cs" script
	public void WakeUpIceBoss()
	{
		// Activate cutscene
		// Activate aggro
	}
}
