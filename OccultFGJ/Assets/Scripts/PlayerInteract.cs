using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour {

	
	// The level has Characters and Symbols
	// Both can be highlighted
	// Characters have dialogue
	// Symbols do not have dialogue, and can only be
	// interacted with once
	// Use this for initialization
	enum InteractionState
	{
		None,
		CanInteract
	}

	private Character interactionTarget;
	private Symbol interactionSymbol;
	
	private InteractionState interactionState = InteractionState.None;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		// Notice when player is close to an interactable character
		
		// Show highlight on the character
		
		// If button is pressed, launch dialogue
		if (Input.GetButtonDown("Fire1"))
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
					string tag = hit.collider.gameObject.tag;
					if (tag == "Character")
					{
						Character testCharacter = hit.collider.gameObject.GetComponent<Character>();
						if (testCharacter != null)
						{
							interactionTarget = testCharacter;
						}
					}
					else if (tag == "Symbol")
					{
						Symbol testSymbol = hit.collider.gameObject.GetComponent<Symbol>();
						if (testSymbol != null)
						{
							interactionSymbol = testSymbol;
						}
					}
				}
			}

			if (interactionTarget != null)
			{
				// Do not command symbols to progress dialogue
				if (interactionSymbol != null)
				{
					interactionSymbol.OnSymbolInteracted();
				}
				else
				{
					interactionTarget.ProgressDialogue();
				}
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
		else if (interactionSymbol != null)
		{
			interaction = interactionSymbol.name;
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
		else if (other.tag == "Symbol")
		{
			Debug.Log("Trigger Symbol" + other.gameObject.name);
			if (interactionSymbol != null)
			{
				interactionSymbol.DisableHighlight();
			}
			interactionSymbol = other.gameObject.GetComponentInParent<Symbol>();
			interactionSymbol.EnableHighlight();
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
		else if (other.tag == "Symbol")
		{
			// Left symbol trigger
			Symbol leavingSymbol = other.gameObject.GetComponent<Symbol>();
			if (leavingSymbol != null)
			{
				leavingSymbol.DisableHighlight();
				if (interactionSymbol == leavingSymbol)
				{
					interactionSymbol = null;
				}
			}
		}
	}
}
