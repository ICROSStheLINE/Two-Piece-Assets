using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bissbiss : MonoBehaviour
{
	static readonly float tailWagAnimationDurationSpeedMultiplier = 0.5f;
	static readonly float tailWagAnimationDuration = 1f / tailWagAnimationDurationSpeedMultiplier;

	Animator anim;

    void Start()
    {
		anim = GetComponent<Animator>();
		
        InvokeRepeating("TailWag", 1, tailWagAnimationDuration + 4);
    }

	void Update()
	{
		
	}

	void TailWag()
	{
		anim.SetBool("WagTail", true);
		Invoke("StopTailWag", tailWagAnimationDuration);
	}
	
	void StopTailWag()
	{
		anim.SetBool("WagTail", false);
	}
}

