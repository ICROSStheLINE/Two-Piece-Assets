using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour
{
	Animator anim;
	PlayerStats playerStats;
    public PlayerPosition playerPosition = new PlayerPosition();
	
	static readonly float pettingDurationSpeedMultiplier = 0.5f;
	static readonly float pettingAnimationDuration = 0.833f / pettingDurationSpeedMultiplier;
	//static readonly float pettingAnimationFrames = 10;
	
	void Start()
	{
		anim = GetComponent<Animator>();
		playerStats = GetComponent<PlayerStats>();
		LoadFromJson();
	}
	
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.F5))
		{
			SaveToJson();
		}
		if (Input.GetKeyDown(KeyCode.F6))
		{
			LoadFromJson();
		}
	}
	
	public void SaveToJson()
	{
		playerPosition.xPosition = transform.position.x;
		playerPosition.yPosition = transform.position.y;
		playerPosition.zPosition = transform.position.z;
		
		string positionData = JsonUtility.ToJson(playerPosition);
		string filePath = Application.persistentDataPath + "/playerPositionData.json";
		Debug.Log(filePath);
		System.IO.File.WriteAllText(filePath, positionData);
		Debug.Log("Save Completed");
		StartCoroutine(PetCat());
	}
	
	public void LoadFromJson()
	{
		string filePath = Application.persistentDataPath + "/playerPositionData.json";
		string positionData = System.IO.File.ReadAllText(filePath);
		
		playerPosition = JsonUtility.FromJson<PlayerPosition>(positionData);
		Debug.Log("Save Loaded");
		
		transform.position = new Vector3(playerPosition.xPosition, playerPosition.yPosition, playerPosition.zPosition);
	}
	
	IEnumerator PetCat()
	{
		anim.SetBool("isPetting", true);
		playerStats.midCutscene = true;
		playerStats.playerCanMove = false;
		playerStats.playerCanDash = false;
		yield return new WaitForSeconds(pettingAnimationDuration);
		playerStats.midCutscene = false;
		playerStats.playerCanMove = true;
		playerStats.playerCanDash = true;
		anim.SetBool("isPetting", false);
	}
}

[System.Serializable]
public class PlayerPosition
{
	public float xPosition;
	public float yPosition;
	public float zPosition;
}