﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuRect
{
	public int x;
	public int y;
	public int w;
	public int h;

	public Rect GetNext()
	{
		Rect nextButton = new Rect(x, y, w, h);
		y += h;
		return nextButton;
	}
}

public class CafeLogic : MonoBehaviour {

	public enum CafeState
	{
		Start, // Show introduction text
		Gameplay, // Move and talk to characters
		// Story beats
		Enter,
		BellInteract,
		DrinkTea,
		
		AllSymbolsInteracted,
		DrinkCoffee,
		
		BlancheInteractDone,
		DrinkHerbal,
		
		AllDrinksDone,
		Epiloque,
		ToCredits
		
	}
	public CafeState cafeState = CafeState.Start;

	private int drinksCounter = 0;
	
	// Introduction text
	private Image introBackground;
	public GameObject IntroBg;
	public Text IntroText;

	// Moving the black cat
	private Vector3 BlackCatTargetPoint;
	public GameObject BlackCat;
	public GameObject Darkness;
	public float BlackCatSpeed = 1.0f;
	
	// Appearing characters
	public List<GameObject> appearCharacters;
	public float CharacterAppearDuration = 2.0f;
	private float CharacterAppearCounter = 0.0f;
	
	// Barista Raye
	public GameObject RayeCat;
	
	// White cat Blanche
	public GameObject BlancheCat;

	public GameObject Argent;
	
	// Occult symbols
	public List<GameObject> Symbols;

	// Bell on the counter
	public GameObject BellOnCounter;
	
	// Use this for initialization
	void Start () {
		BlackCatTargetPoint = GameObject.Find("BlackCatTargetPoint").transform.position;
		introBackground = IntroBg.GetComponent<Image>();
		IntroText = introBackground.GetComponentInChildren<Text>();
		ChangeState(CafeState.Start);
	}

	public void OnGUI()
	{
		MenuRect rect = new MenuRect();
		rect.x = 10;
		rect.y = 40;
		rect.w = 200;
		rect.h = 20;

		GUI.TextArea(rect.GetNext(), "Change Game State:");
		string[] names = Enum.GetNames(typeof(CafeState));
		CafeState[] values = Enum.GetValues(typeof(CafeState)) as CafeState[];
		for (int i = 0; i < values.Length; i++)
		{
			if (GUI.Button(rect.GetNext(), names[i]))
			{
				 ChangeState(values[i]);	
			}
		}
	}

	void ChangeState(CafeState newState)
	{
		if (newState == CafeState.Start)
		{
			introBackground.enabled = true;
			IntroText.enabled = true;
			ShowSymbols(false);
			ShowBlackCat(false);
			ShowBlanche(false);
			ShowArgent(false);
			ShowRaye(false);
			
			ShowBell(true);
			ShowDarkness(true);
		}
		else if (newState == CafeState.Gameplay)
		{
			introBackground.enabled = false;
			IntroText.enabled = false;
		}
		else if (newState == CafeState.BellInteract)
		{
			ShowRaye(true);
		}
		else if (newState == CafeState.DrinkTea)
		{
			ShowBell(false);
			ShowSymbols(true);
		}
		else if (newState == CafeState.AllSymbolsInteracted)
		{
			ShowSymbols(false);
			ShowBell(true);
		}
		else if (newState == CafeState.DrinkCoffee)
		{
			ShowBell(false);
			ShowBlanche(true);
		}
		else if (newState == CafeState.BlancheInteractDone)
		{
			ShowBell(true);
			ShowBlanche(false);
		}
		else if (newState == CafeState.DrinkHerbal)
		{
			ShowBell(false);
			ShowArgent(true);
		}
		
		else if (newState == CafeState.AllDrinksDone)
		{
			// TODO: Make characters and occult symbols appear
			foreach (GameObject character in appearCharacters)
			{
				character.SetActive(true);
			}
			// Then make characters appear
			ShowBlackCat(true);
			
		}
		else if (newState == CafeState.ToCredits)
		{
			SceneManager.LoadScene("end_screen");
		}

		CharacterAppearCounter = 0.0f;
		cafeState = newState;
	}

	void ShowSymbols(bool visible)
	{
		foreach (GameObject symbol in Symbols)
		{
			symbol.SetActive(visible);
		}
	}

	void ShowRaye(bool visible)
	{
		RayeCat.SetActive(visible);
	}

	void ShowBell(bool visible)
	{
		BellOnCounter.SetActive(visible);
	}

	void ShowBlanche(bool visible)
	{
		BlancheCat.SetActive(visible);
	}

	void ShowArgent(bool visible)
	{
		Argent.SetActive(visible);
	}

	void ShowBlackCat(bool visible)
	{
		BlackCat.SetActive(visible);
	}

	void ShowDarkness(bool visible)
	{
		Darkness.SetActive(visible);
	}
	
	// Update is called once per frame
	void Update () 
	{
		switch (cafeState)
		{
			case CafeState.AllDrinksDone:
			{
				if (BlackCat != null)
				{
					Vector3 newPos = Vector3.MoveTowards(BlackCat.transform.position,
						BlackCatTargetPoint,
						Time.deltaTime * BlackCatSpeed);
					if (Vector3.Distance(BlackCat.transform.position, BlackCatTargetPoint) < 0.1f)
					{
						ChangeState(CafeState.Epiloque);
					}
				}
			}
				break;
			default: 
				// NOP
				break;
		}
	}
}
