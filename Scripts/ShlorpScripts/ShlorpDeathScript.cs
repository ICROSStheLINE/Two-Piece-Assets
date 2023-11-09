using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShlorpDeathScript : MonoBehaviour
{
    Animator anim;
	[SerializeField] GameObject shlorpSoul;
	bool scheduledToDie = false;
	
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (anim.GetBool("isDying"))
		{
			if (!scheduledToDie)
			{
				Invoke("KILLYOURSELF", (0.833f / 0.35f));
				scheduledToDie = true;
			}
			anim.SetInteger("teleportStage", 0);
			anim.SetBool("isAttacking", false);
			GetComponent<ShlorpCombatScript>().CancelInvoke();
			GetComponent<ShlorpCombatScript>().enabled = false;
		}
    }
	
	void KILLYOURSELF()
	{
		Instantiate(shlorpSoul, transform.position, transform.rotation);
		Destroy(gameObject);
	}
}