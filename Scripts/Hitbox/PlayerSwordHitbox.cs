using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwordHitbox : MonoBehaviour
{
	Animator shlorpNGlorpAnimator;
	
    void Update()
    {
        
    }

	void OnTriggerEnter2D(Collider2D collision)
	{
		if ((collision.gameObject.tag == "Glorp") || (collision.gameObject.tag == "Shlorp"))
		{
			shlorpNGlorpAnimator = collision.GetComponent<Animator>();
			shlorpNGlorpAnimator.SetBool("isDying", true);
		}
	}
}