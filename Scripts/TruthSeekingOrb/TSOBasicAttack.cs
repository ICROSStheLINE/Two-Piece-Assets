using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TSOBasicAttack : MonoBehaviour
{
    [SerializeField] GameObject theHitbox;
	//GameObject player;
	Animator anim;

	static readonly float attackAnimationDurationSpeedMultiplier = 2;
	static readonly float attackAnimationDuration = 1 / attackAnimationDurationSpeedMultiplier;
	static readonly float attackAnimationFrames = 12;
	static readonly float attackHitboxSpawn = (4 / attackAnimationFrames) * attackAnimationDuration;
	static readonly float attackHitboxDespawn = (7 / attackAnimationFrames) * attackAnimationDuration;

	[HideInInspector] public bool isTSOBasicAttacking = false;
	bool isTSOBasicAttackOnCooldown = false;

    void Start()
    {
		//player = GameObject.FindWithTag("Player");
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if ((Input.GetKeyDown("g")) && (!isTSOBasicAttacking) && (!isTSOBasicAttackOnCooldown))
		{
			anim.SetBool("basicAttacking", true);
			isTSOBasicAttacking = true;
			isTSOBasicAttackOnCooldown = true;
			Invoke("SpawnHitbox", attackHitboxSpawn);
			Invoke("DespawnHitbox", attackHitboxDespawn);
			Invoke("ResetAttackCooldown", attackAnimationDuration);
		}
    }

	void SpawnHitbox()
	{
		GameObject referenceObject = Instantiate(theHitbox, gameObject.transform.position + new Vector3(2.5f * Mathf.Sign(gameObject.transform.localScale.x),-1f,0), gameObject.transform.rotation);
		referenceObject.transform.parent = gameObject.transform;
		referenceObject.transform.localScale += new Vector3(2.5f * Mathf.Sign(gameObject.transform.localScale.x),2,0);
	}

	void DespawnHitbox()
	{
		GameObject existingHitbox = gameObject.transform.GetChild(0).gameObject;
		Destroy(existingHitbox);
	}

	void ResetAttackCooldown()
	{
		isTSOBasicAttacking = false;
		isTSOBasicAttackOnCooldown = false;
		anim.SetBool("basicAttacking", false);
	}
}
