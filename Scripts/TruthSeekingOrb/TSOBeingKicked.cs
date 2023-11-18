using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TSOBeingKicked : MonoBehaviour
{
    Rigidbody2D rb;
	SpriteRenderer spriteRenderer;
	[SerializeField] GameObject trailPrefab;
	
	float movementSpeed = 50f;
	
    void Start()
    {
		rb = GetComponent<Rigidbody2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		
        if (gameObject.transform.rotation.z != 0)
		{
			movementSpeed = movementSpeed * -1;
			spriteRenderer.flipY = !spriteRenderer.flipY;
		}
		
		InvokeRepeating("SpawnTrail", 0.1f, 0.07f);
		Invoke("KILLYOURSELF", 1f);
    }

    void FixedUpdate()
    {
		rb.position += new Vector2(movementSpeed * Time.deltaTime,0);
    }

	void SpawnTrail()
	{
		Instantiate(trailPrefab, transform.position, transform.rotation);
	}

	void KILLYOURSELF()
	{
		Destroy(gameObject);
	}
}
