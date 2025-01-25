using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour {

	// Use this for initialization
	enum InteractionState
	{
		None,
		CanInteract
	}

	private Character interactionTarget;
	
	private InteractionState interactionState = InteractionState.None;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		// Notice when player is close to an interactable character
		
		// Show highlight on the character
		
		// If button is pressed, launch dialogue
		if (Input.GetButtonUp("Fire1"))
		{
			// Check if mouse is clicked over a character trigger
			if (Input.GetMouseButtonDown(0))
			{
				Physics2D.queriesHitTriggers = true;
				Vector3 mouseOnWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				Vector2 mouseOnWorld2D = new Vector2(mouseOnWorld.x, mouseOnWorld.y);
				RaycastHit2D hit = Physics2D.CircleCast(mouseOnWorld2D, 0.1f, Vector2.zero, 0.0f);
				if (hit.collider != null)
				{
					Character test = hit.collider.gameObject.GetComponent<Character>();
					if (test != null)
					{
						interactionTarget = test;
					}
				}
			}

			if (interactionTarget != null)
			{
				interactionTarget.StartDialogue();
				
				// TODO: Change interaction state so can advance in the dialogue 
			}
		}
	}

	public void OnGUI()
	{
		string interaction = "No target";
		if (interactionTarget != null)
		{
			interaction = interactionTarget.name;
		}
		GUI.TextField(new Rect(10, 10, 300, 30), "Interacting with: " + interaction);
	}

	public void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Character")
		{
			// Change target to new one
			if (interactionTarget != null)
			{
				interactionTarget.DisableHighlight();
			}
			interactionTarget = other.gameObject.GetComponentInParent<Character>();
			interactionTarget.EnableHighlight();
		}
	}

	public void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "Character")
		{
			Character leavingTarget = other.gameObject.GetComponentInParent<Character>();
			if (leavingTarget != null)
			{
				leavingTarget.DisableHighlight();
				if (interactionTarget == leavingTarget)
				{
					interactionTarget = null;
				}
			}
		}
	}
}
