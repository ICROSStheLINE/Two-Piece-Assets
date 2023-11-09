using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlorpProjectileScript : MonoBehaviour
{
    Rigidbody2D rb;
	SpriteRenderer spriteRenderer;
	GameObject healthBar;
	HealthScript healthScript;
	
	float movementSpeed = 22f;
	
    void Start()
    {
		healthBar = GameObject.FindWithTag("HealthBar");
		healthScript = healthBar.GetComponent<HealthScript>();
        rb = GetComponent<Rigidbody2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		
		if (gameObject.transform.rotation.z != 0)
		{
			movementSpeed = movementSpeed * -1;
			spriteRenderer.flipY = !spriteRenderer.flipY;
		}
    }

    void FixedUpdate()
    {
		rb.position += new Vector2(movementSpeed * Time.deltaTime,0);
		Invoke("KILLYOURSELF", 0.833f);
    }
	
	void KILLYOURSELF()
	{
		Destroy(gameObject);
	}
	
	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "PlayerShieldHitbox")
		{
			Destroy(gameObject);
		}
		else if (collision.gameObject.tag == "Player")
		{
			healthScript.LoseHealthBy(1);
			Destroy(gameObject);
		}
	}
}
