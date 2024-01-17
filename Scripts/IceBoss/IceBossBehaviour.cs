using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBossBehaviour : MonoBehaviour
{
	Rigidbody2D rb;
	IceBossStats iceBossStats;
	GameObject player;
	GameObject fightingZone;
	PlayerStats playerStats;
	GameObject bossSclera;
	GameObject bossEye;

	Vector3 nextPosition;

	// Idling Variables
	Vector3 idleTargetPosition;
	Vector3[] idleTargetPositionPoints = new Vector3[2];
	int idlePositionIndex = 0;
	
	// Attacking Variables
	Vector3 attackingTarget;
	
	[SerializeField] GameObject retardedPrefab;


    void Start()
    {
		rb = GetComponent<Rigidbody2D>();
        iceBossStats = transform.parent.GetComponent<IceBossStats>();
		player = GameObject.FindWithTag("Player");
		fightingZone = GameObject.FindWithTag("Ice Arena Fighting Zone");
		playerStats = player.GetComponent<PlayerStats>();
		bossSclera = transform.GetChild(0).gameObject;
		bossEye = bossSclera.transform.GetChild(0).gameObject;
		
		
		SetNewIdlePositionPoints(transform.position, new Vector3(0,2,0), new Vector3(0,0,0));
    }

    void FixedUpdate()
    {
		if (iceBossStats.iceBossAttemptAttack && !iceBossStats.iceBossMidAttack)
			AttackStart();
		else if (iceBossStats.iceBossMidAttack)
			Invoke("AttackMiddle", 0.5f);
		else if (iceBossStats.iceBossIdling && !iceBossStats.iceBossMidAttack)
			Idling();
		
		
        transform.position = nextPosition;
    }

	void Idling()
	{
		nextPosition = Vector3.MoveTowards(transform.position, idleTargetPosition, 3 * Time.deltaTime);
		
		if (transform.position == idleTargetPosition)
		{
			idlePositionIndex = Mathf.Abs(idlePositionIndex - 1);
			idleTargetPosition = idleTargetPositionPoints[idlePositionIndex]; // Switch the idle bobbing destination
		}
		
		
	}

	void AttackStart()
	{
		iceBossStats.iceBossIdling = false;
		attackingTarget = player.transform.position + new Vector3(0,3,0);
		iceBossStats.iceBossMidAttack = true;
		
		SetNewIdlePositionPoints(attackingTarget, new Vector3(0,Mathf.Sign(player.transform.localScale.y)*6,0), new Vector3(0,Mathf.Sign(player.transform.localScale.y)*6 - 2,0));
		
		IceBossEyeStare();
		//SpawnAttackIndicator();
	}

	void AttackMiddle()
	{
		nextPosition = Vector3.MoveTowards(transform.position, attackingTarget, 450 * Time.deltaTime);
		if (transform.position == attackingTarget)
		{
			iceBossStats.iceBossMidAttack = false;
			iceBossStats.iceBossAttemptAttack = false;
			CancelInvoke("AttackMiddle");
			
			iceBossStats.iceBossIdling = true;
			bossEye.transform.position = bossSclera.transform.position;
		}
	}

	public void IceBossEyeStare()
	{
		bossEye.transform.position += new Vector3(Vector3.Normalize(player.transform.position - bossEye.transform.position).x, Vector3.Normalize(player.transform.position - bossEye.transform.position).y/2, Vector3.Normalize(player.transform.position - bossEye.transform.position).z);
	}

	/*void SpawnAttackIndicator()
	{
		Instantiate(retardedPrefab, transform.position + new Vector3(4,0,0), Quaternion.Euler(0,0,0));
		Instantiate(retardedPrefab, transform.position + new Vector3(-4,0,0), Quaternion.Euler(0,0,0));
		Instantiate(retardedPrefab, transform.position + new Vector3(0,4,0), Quaternion.Euler(0,0,0));
		Instantiate(retardedPrefab, transform.position + new Vector3(0,-4,0), Quaternion.Euler(0,0,0));
	}*/

	void SetNewIdlePositionPoints(Vector3 position_, Vector3 offset1, Vector3 offset2)
	{
		idleTargetPositionPoints[0] = position_ + offset1;
		idleTargetPositionPoints[1] = position_ + offset2;
		idleTargetPosition = idleTargetPositionPoints[0];
	}

	Vector3 BossPositionInArena()
	{
		Vector3 bossPos = gameObject.transform.position;
		Vector3 arenaStartPos = fightingZone.transform.position - new Vector3(fightingZone.transform.localScale.x/2,0,0);
		return (bossPos - arenaStartPos); // If returns (0,y,z) then the boss is at the leftmost side of the arena
	}

	Vector3 PlayerPositionInArena()
	{
		Vector3 playerPos = player.transform.position;
		Vector3 arenaStartPos = fightingZone.transform.position - new Vector3(fightingZone.transform.localScale.x/2,0,0);
		return (playerPos - arenaStartPos); // If returns (0,y,z) then the player is at the leftmost side of the arena
	}
	
	

}

/* Curved Movement Code

Vector3[] movementPoints = new Vector3[4];
bool curvedMovement = false;
int curvedMovementIndex = 0;

if (curvedMovement && curvedMovementIndex < 4)
	{
		transform.position = Vector3.MoveTowards(transform.position, movementPoints[curvedMovementIndex], 10f * Time.deltaTime);
		if (transform.position == movementPoints[curvedMovementIndex])
			curvedMovementIndex++;
	}
	else
	{
		curvedMovement = false;
		curvedMovementIndex = 0;
	}

void CurvedMovement()
	{
		movementPoints[0] = gameObject.transform.position + new Vector3(0,1,0);
		movementPoints[1] = gameObject.transform.position + new Vector3(1,4,0);
		movementPoints[2] = gameObject.transform.position + new Vector3(3,6,0);
		movementPoints[3] = gameObject.transform.position + new Vector3(6,7,0);
		curvedMovement = true;
		curvedMovementIndex = 0;
	}

*/