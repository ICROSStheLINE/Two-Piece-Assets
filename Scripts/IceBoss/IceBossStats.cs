using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBossStats : MonoBehaviour
{
	IceBossBehaviour iceBossBehaviour;
	IceBossJaw iceBossJaw;

    void Start()
    {
        iceBossBehaviour = GetComponentInChildren<IceBossBehaviour>();
		iceBossJaw = GetComponentInChildren<IceBossJaw>();
    }

    void Update()
    {
        
    }
}
