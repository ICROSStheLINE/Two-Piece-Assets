using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBossBehaviour : MonoBehaviour
{
	Rigidbody2D rb;
	GameObject healthBar;
	HealthScript healthScript;
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
	string[] queuedAttack = new string[6];
	
	// Laser Variables
	[SerializeField] GameObject laserPrefab;
	
	// Energy Orb Variables
	[SerializeField] GameObject energyOrbPrefab;
	GameObject chargedOrb;


    void Start()
    {
		rb = GetComponent<Rigidbody2D>();
		healthBar = GameObject.FindWithTag("HealthBar");
		healthScript = healthBar.GetComponent<HealthScript>();
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
		if (iceBossStats.iceBossSpecialPattern == 0) // Special patterns are cool attacks that aren't typical
		{
			// Checking Attack Variables
			if (iceBossStats.iceBossMidAttack && !iceBossStats.iceBossAttemptOrb)
				Invoke("AttackMiddle", 0.4f);
			else if (iceBossStats.iceBossAttemptOrb)
			{
				ChargeEnergyOrb();
				AttackFinish();
			}
			else if (iceBossStats.iceBossMidOrb)
				nextPosition = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, player.transform.position.y + 5, transform.position.z), 15 * Time.deltaTime);
			else if (iceBossStats.iceBossIdling && !iceBossStats.iceBossMidOrb && !iceBossStats.iceBossMidAttack && !iceBossStats.iceBossAttemptOrb)
				Idling();
		}
		else if (iceBossStats.iceBossSpecialPattern == 1)
			SpecialPatternOne();
		
		// Movement
		if (iceBossStats.iceBossIsAwake)
			transform.position = nextPosition;
    }

	void Idling()
	{
		nextPosition = Vector3.MoveTowards(transform.position, idleTargetPosition, 6 * Time.deltaTime);
		
		if (transform.position == idleTargetPosition)
		{
			idlePositionIndex = Mathf.Abs(idlePositionIndex - 1);
			idleTargetPosition = idleTargetPositionPoints[idlePositionIndex]; // Switch the idle bobbing destination
		}
	}

	void AttackStart(Vector3 customTarget = default(Vector3))
	{
		iceBossStats.iceBossIdling = false;
		if (customTarget == default(Vector3))
			attackingTarget = player.transform.position + new Vector3(0,3,0);
		else
			attackingTarget = customTarget;
		
		iceBossStats.iceBossMidAttack = true;
		
		IceBossEyeStare(customTarget);
	}

	void AttackMiddle()
	{
		nextPosition = Vector3.MoveTowards(transform.position, attackingTarget, 600 * Time.deltaTime);
		if (transform.position == attackingTarget)
		{
			AttackFinish();
			
			CheckAttackQueue();
		}
	}

	void AttackFinish()
	{
		iceBossStats.iceBossMidAttack = false;
		CancelInvoke("AttackMiddle");
		
		SetNewIdlePositionPoints(transform.position, new Vector3(0,Mathf.Sign(BossPositionInArena().y * -1)*3,0), new Vector3(0,Mathf.Sign(BossPositionInArena().y * -1)*3 - 2,0));

		iceBossStats.iceBossIdling = true;
		bossEye.transform.position = bossSclera.transform.position;
		
		if (iceBossStats.iceBossSpecialPattern != 0)
			iceBossStats.iceBossSpecialPatternStage += 0.5f;
	}

	void Laser()
	{
		Instantiate(laserPrefab, new Vector3(player.transform.position.x,11.27313f,0), Quaternion.Euler(0,0,0));
	}

	void ChargeEnergyOrb(float customChargeTime = default(float))
	{
		float oldChargeTime = iceBossStats.iceBossOrbChargeTime;
		int angleOfOrb = 0;
		if (Mathf.Sign(player.transform.position.x - transform.position.x) == -1)
			angleOfOrb = 180;
		if (customChargeTime != default(float))
			iceBossStats.iceBossOrbChargeTime = customChargeTime;
		chargedOrb = Instantiate(energyOrbPrefab, transform.position - new Vector3(0,5,0), Quaternion.Euler(0,0,angleOfOrb));
		Invoke("FireEnergyOrb", iceBossStats.iceBossOrbChargeTime);
		
		iceBossStats.iceBossOrbChargeTime = oldChargeTime;
		
		iceBossStats.iceBossMidOrb = true;
		iceBossStats.iceBossAttemptOrb = false;
		
		if (iceBossStats.iceBossSpecialPattern != 0)
			iceBossStats.iceBossSpecialPatternStage += 0.5f;
	}

	void FireEnergyOrb()
	{
		// Orb now fires itself (Code is in the "IceBossProjectile" script)
		iceBossStats.iceBossMidOrb = false;
		
		CheckAttackQueue();
	}

	void CheckAttackQueue()
	{
		// Checks if any attacks were queued up and activates the next queued attack
		if (queuedAttack[0] == "Attack")
			ActivateAttack();
		else if (queuedAttack[0] == "Orb")
			ActivateOrb();
		else if (queuedAttack[0] == "ResetPatternVar")
			ResetPatternVar();
		
		// Corrects the order of the queued attacks (Moves the order down by 1)
		for (int i = 0; i < queuedAttack.Length - 1; i += 1)
			queuedAttack[i] = queuedAttack[i + 1];
		queuedAttack[5] = null;
	}

	public void IceBossEyeStare(Vector3 customTarget = default(Vector3))
	{
		if (customTarget == default(Vector3)) // If no parameters were given, stare at the player
			bossEye.transform.position += new Vector3(Vector3.Normalize(player.transform.position - bossEye.transform.position).x, Vector3.Normalize(player.transform.position - bossEye.transform.position).y/2, 0);
		else // Otherwise, stare at the custom target (Obviously)
			bossEye.transform.position += new Vector3(Vector3.Normalize(customTarget - bossEye.transform.position).x/2, 0, 0);
	}

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

	// Attack Patterns

	void PatternOne()
	{
		// Just charging at the player
		if (!iceBossStats.iceBossPerformingPattern)
		{
			iceBossStats.iceBossPerformingPattern = true;
			
			ActivateAttack();
			queuedAttack[0] = "Attack";
			queuedAttack[1] = "Attack";
			queuedAttack[2] = "ResetPatternVar";
		}
	}

	void PatternTwo()
	{
		// Charging twice then energy orb
		if (!iceBossStats.iceBossPerformingPattern)
		{
			iceBossStats.iceBossPerformingPattern = true;
			
			ActivateAttack();
			queuedAttack[0] = "Attack";
			queuedAttack[1] = "Orb";
			queuedAttack[2] = "ResetPatternVar";
		}
	}

	
	void SpecialPatternOne()
	{
		if (!iceBossStats.iceBossPerformingPattern)
		{
			iceBossStats.iceBossSpecialPattern = 1; // Set this variable to 1 so the Update() function only does this shit
			
			if (iceBossStats.iceBossSpecialPatternStage == 0)
			{	
				AttackStart(player.transform.position + new Vector3(0,Mathf.Sign(PlayerPositionInArena().y * -1)*9,0));
				iceBossStats.iceBossSpecialPatternStage += 0.5f;
			}
			else if (iceBossStats.iceBossSpecialPatternStage == 0.5f)
				Invoke("AttackMiddle", 0.05f);
			else if (iceBossStats.iceBossSpecialPatternStage == 1)
			{
				AttackStart(player.transform.position + new Vector3(0,Mathf.Sign(PlayerPositionInArena().y * -1)*9,0));
				iceBossStats.iceBossSpecialPatternStage += 0.5f;
			}
			else if (iceBossStats.iceBossSpecialPatternStage == 1.5f)
				Invoke("AttackMiddle", 0.05f);
			else if (iceBossStats.iceBossSpecialPatternStage == 2)
			{
				AttackStart(player.transform.position + new Vector3(0,Mathf.Sign(PlayerPositionInArena().y * -1)*9,0));
				iceBossStats.iceBossSpecialPatternStage += 0.5f;
			}
			else if (iceBossStats.iceBossSpecialPatternStage == 2.5f)
				Invoke("AttackMiddle", 0.05f);
			else if (iceBossStats.iceBossSpecialPatternStage == 3)
			{
				AttackStart(player.transform.position + new Vector3(0,Mathf.Sign(PlayerPositionInArena().y * -1)*9,0));
				iceBossStats.iceBossSpecialPatternStage += 0.5f;
			}
			else if (iceBossStats.iceBossSpecialPatternStage == 3.5f)
				Invoke("AttackMiddle", 0.05f);
			else if (iceBossStats.iceBossSpecialPatternStage == 4)
			{	
				AttackStart();
				iceBossStats.iceBossSpecialPatternStage += 0.5f;
			}
			else if (iceBossStats.iceBossSpecialPatternStage == 4.5f)
				Invoke("AttackMiddle", 0.4f);
			else if (iceBossStats.iceBossSpecialPatternStage == 5) // Fly to the right side of arena
			{
				Vector3 endOfArena = fightingZone.transform.position + new Vector3(fightingZone.transform.localScale.x/2,0,0);
				nextPosition = Vector3.MoveTowards(transform.position, endOfArena, 45 * Time.deltaTime);
				if (transform.position == nextPosition)
					iceBossStats.iceBossSpecialPatternStage += 0.5f;
			}
			else if (iceBossStats.iceBossSpecialPatternStage > 5 && iceBossStats.iceBossSpecialPatternStage < 8) // Fires a couple energy orbs
			{
				if (!iceBossStats.iceBossMidOrb)
					ChargeEnergyOrb(0.3f);
				else // This else statement makes the boss track the player a bit
					nextPosition = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, player.transform.position.y + 5, transform.position.z), 35 * Time.deltaTime);
			}
			else if (iceBossStats.iceBossSpecialPatternStage == 8)
			{
				SetNewIdlePositionPoints(transform.position, new Vector3(0,Mathf.Sign(BossPositionInArena().y * -1)*3,0), new Vector3(0,Mathf.Sign(BossPositionInArena().y * -1)*3 - 2,0));
				iceBossStats.iceBossSpecialPattern = 0;
				iceBossStats.iceBossSpecialPatternStage = 0;
			}
		}
	}


	void ActivateAttack()
	{
		if (!iceBossStats.iceBossMidAttack && !iceBossStats.iceBossMidOrb && !iceBossStats.iceBossAttemptOrb)
			AttackStart();
	}

	void ActivateLaser()
	{
		Laser();
	}

	void ActivateOrb()
	{
		iceBossStats.iceBossAttemptOrb = true;
	}

	void ResetPatternVar()
	{
		iceBossStats.iceBossPerformingPattern = false;
	}
	
	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Player" && iceBossStats.iceBossMidAttack)
		{
			healthScript.LoseHealthBy(1);
		}
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