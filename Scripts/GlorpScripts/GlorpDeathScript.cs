using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlorpDeathScript : MonoBehaviour
{
    Animator anim;
	
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (anim.GetBool("isDying"))
		{
			Invoke("KILLYOURSELF", (0.417f * 2f));
			anim.SetBool("isWalking", false);
			anim.SetBool("isAttacking", false);
			GetComponent<GlorpAttackingScript>().CancelInvoke();
			GetComponent<GlorpAttackingScript>().enabled = false;
			GetComponent<GlorpWalkingScript>().CancelInvoke();
			GetComponent<GlorpWalkingScript>().enabled = false;
		}
    }
	
	void KILLYOURSELF()
	{
		Destroy(gameObject);
	}
}
