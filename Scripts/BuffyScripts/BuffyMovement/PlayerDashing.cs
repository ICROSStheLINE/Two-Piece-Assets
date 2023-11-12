using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashing : MonoBehaviour
{
	Rigidbody2D rb;
	Animator anim;
	[HideInInspector] public bool isDashing = false;
	[HideInInspector] public bool isDashingButResets1MillisecondEarlier = false;
	[HideInInspector] public bool canDash;
	
	

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
    }

    void Update()
    {
		if (Input.GetKeyDown("e") && (isDashing == false) && (canDash == true))
		{
			rb.velocity = new Vector2(40 * Mathf.Sign(gameObject.transform.localScale.x),0);
			anim.SetBool("isDashing", true);
			isDashing = true;
			isDashingButResets1MillisecondEarlier = true;
			Invoke("ResetDashCooldown", 0.5f);
			Invoke("ResetTheIsDashingButResets1MillisecondEarlierVariableSoThatTurningWhileDashingIsntGlitched", 0.49f);
		}
    }
	
	void ResetTheIsDashingButResets1MillisecondEarlierVariableSoThatTurningWhileDashingIsntGlitched()
	{
		isDashingButResets1MillisecondEarlier = false;
	}

	public void ResetDashCooldown()
	{
		isDashing = false;
		anim.SetBool("isDashing", false);
		rb.velocity = new Vector2(0,0);
	}
}
