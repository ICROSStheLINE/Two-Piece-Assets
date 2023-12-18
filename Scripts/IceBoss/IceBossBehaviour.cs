using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBossBehaviour : MonoBehaviour
{
	IceBossStats iceBossStats;

    void Start()
    {
        iceBossStats = transform.parent.GetComponent<IceBossStats>();
    }

    void Update()
    {
        
    }
}
