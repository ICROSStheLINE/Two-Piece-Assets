using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Louis : MonoBehaviour
{
	GameObject dialogue;
	SpeechBubbleScript speechBubbleScript;
	GameObject player;
	PlayerStats playerStats;
	
	bool waitForInteraction = true;
	[SerializeField] string[] potentialDialogue;
	[SerializeField] float textSpeed;
	[SerializeField] string preDialogueAction;
	[SerializeField] bool stopDash;
	[SerializeField] string postDialogueAction;
	
    void Start()
    {
		dialogue = GameObject.FindWithTag("Dialogue");
		speechBubbleScript = dialogue.GetComponent<SpeechBubbleScript>();
		player = GameObject.FindWithTag("Player");
		playerStats = player.GetComponent<PlayerStats>();
    }

    void Update()
    {
        
    }
	
	void OnTriggerStay2D(Collider2D collision)
	{
		if ((collision.gameObject.tag == "Player") && Input.GetKey(playerStats.interactKey) && waitForInteraction)
		{
			dialogue.SetActive(true);
			speechBubbleScript.textComponent.text = string.Empty;
			//speechBubbleScript.textComponent.color = new Color(1f,1f,1f,1f);
			speechBubbleScript.lines = potentialDialogue;
			speechBubbleScript.textSpeed = textSpeed;
			speechBubbleScript.preDialogueAction = preDialogueAction;
			speechBubbleScript.stopDash = stopDash;
			speechBubbleScript.postDialogueAction = postDialogueAction;
			speechBubbleScript.StartDialogue();
			waitForInteraction = false;
		}
	}
}
