using System;
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

	// This is the state of the game
	// it is expected to advance in this order
	public enum CafeState
	{
		Introduction, // Show introduction text
		Gameplay, // Move and talk to characters, this state is used multiple times
		// Story parts:
		
		// Player starts in the cafe
		Enter,
		// Interacts with bell: this happens multiple times
		// The next state depends on the current state or amount of
		// drinks 
		BellInteract, 
		
		// This happens when player has talked to Ray
		RayeInteractionDone,
		
		// Player orders the tea and drinks it
		DrinkTea,
		
		// Player has interacted with all the symbols
		AllSymbolsInteracted,
		
		// Player orders coffee and drinks it
		DrinkCoffee,
		// Player has talked to Blanche
		BlancheInteractDone,
		
		// Player order herbal tea and drinks it
		DrinkHerbal,
		// Player has talked to Argent
		ArgentInteractDone,
		
		// When player has ordered all 3 drinks
		// The smol black cat comes out of the Darkness
		AllDrinksDone,
		
		// Epiloque dialogue 
		Epiloque,
		
		// Game changes to end_screen scene
		ToCredits
		
	}
	private CafeState cafeState = CafeState.Introduction;
	private CafeState incomingState = CafeState.Introduction;

	// How many of the drinks the player has 
	// ordered: Tea: Coffee: Herbal tea
	private int drinksCounter = 0;
	
	// How many of the symbols the player
	// has interacted with 0-3
	[SerializeField] private int symbolsCounter = 0;
	
	// When symbols are visible, player spooky music
	private MusicPlayer cafeMusicPlayer;
	
	// Introduction text
	public GameObject IntroBg;
	private Image introBackground;
	public Text IntroText;

	// Moving the black cat
	private Vector3 BlackCatTargetPoint;
	public GameObject BlackCat;
	public GameObject Darkness;
	public float BlackCatSpeed = 1.0f;
	
	// Bell 
	private BellInteraction bellScript;
	
	// Appearing characters
	public List<GameObject> appearCharacters;
	
	public bool stateChangeInProgress = false;
	public float stateChangeDuration = 3.0f;
	public float stateChangeCounter = 0.0f;
	
	// Barista Raye
	public GameObject RayeCat;
	
	// White cat Blanche
	public GameObject BlancheCat;

	// Grey cat Argent
	public GameObject Argent;
	
	// Occult symbols
	public List<GameObject> Symbols;
	// Leave time for sounds to play

	// Bell on the counter
	public GameObject BellOnCounter;
	
	// Use this for initialization
	void Start () {
		BlackCatTargetPoint = GameObject.Find("BlackCatTargetPoint").transform.position;
		introBackground = IntroBg.GetComponent<Image>();
		IntroText = introBackground.GetComponentInChildren<Text>();
		cafeMusicPlayer = GameObject.Find("MusicPlayer").GetComponent<MusicPlayer>();
		bellScript = GameObject.Find("bell").GetComponent<BellInteraction>();
		ChangeState(CafeState.Introduction);
	}

	public CafeState GetCafeState()
	{
		return cafeState;
	}
	public int GetDrinksCounter()
	{
		return drinksCounter;
	}
	public int GetSymbolsCounter()
	{
		return symbolsCounter;
	}

	public void AddDrink()
	{
		drinksCounter++;
		if (drinksCounter >= 3)
		{
			drinksCounter = 3;
			ChangeState(CafeState.AllDrinksDone);
		}
	}

	public void AddSymbol()
	{
		symbolsCounter++;
		if (symbolsCounter >= 3)
		{
			symbolsCounter = 3;
			ChangeState(CafeState.AllSymbolsInteracted);
		}
	}

	// Debug buttons to change the state manually
	public void OnGUI()
	{
		MenuRect rect = new MenuRect();
		rect.x = 10;
		rect.y = 40;
		rect.w = 200;
		rect.h = 20;

		GUI.TextArea(rect.GetNext(), "State : " + cafeState);
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
		
		rect.GetNext();

		if (GUI.Button(rect.GetNext(), "Drink (" + drinksCounter + ")"))
		{
			drinksCounter = Math.Min(3, drinksCounter + 1);
		}

		if (GUI.Button(rect.GetNext(), "Clear drinks"))
		{
			drinksCounter = 0;
		}
		if (GUI.Button(rect.GetNext(), "Symbol (" + symbolsCounter + ")"))
		{
			symbolsCounter = Math.Min(3, symbolsCounter + 1);
		}

		if (GUI.Button(rect.GetNext(), "Clear symbols"))
		{
			symbolsCounter = 0;
		}
	}
	
	//  This is called by dialogue code when
	// a dialogue finishes
	public void ChangeState(CafeState newState)
	{
		// Avoid getting stuck in a loop
		if (!stateChangeInProgress && newState == CafeState.AllSymbolsInteracted && incomingState != CafeState.AllSymbolsInteracted)
		{
			incomingState = newState;
			stateChangeInProgress = true;
			return;
		}
		switch (newState)
		{
			case CafeState.Introduction:
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
				break;
			case CafeState.Gameplay:
			{
				introBackground.enabled = false;
				IntroText.enabled = false;
			}
				break;
			case CafeState.BellInteract:
			{
				ShowRaye(true);
				cafeMusicPlayer.PlayCafeMusic();
			}
				break;
			case CafeState.RayeInteractionDone:
				// DEMO  
				ChangeState(CafeState.ToCredits);

				// ChangeState(CafeState.DrinkTea);

				break;
			case CafeState.DrinkTea:
			{
				ShowBell(false);
				ShowSymbols(true);
				cafeMusicPlayer.PlaySpookyMusic();
			}
				break;
			case CafeState.AllSymbolsInteracted:
			{
				ShowSymbols(false);
				ShowBell(true);
				cafeMusicPlayer.PlayCafeMusic();
			}
				break;
			case CafeState.DrinkCoffee:
			{
				ShowBell(false);
				ShowBlanche(true);
			}
				break;
			case CafeState.BlancheInteractDone:
			{
				ShowBell(true);
				ShowBlanche(false);
			}
				break;
			case CafeState.DrinkHerbal:
			{
				ShowBell(false);
				ShowArgent(true);
			}
				break;

			case CafeState.AllDrinksDone:
			{
				// TODO: Make characters and occult symbols appear
				foreach (GameObject character in appearCharacters)
				{
					character.SetActive(true);
				}

				// Then make characters appear
				ShowBlackCat(true);
			}
				break;
			case CafeState.ToCredits:
			{
				SceneManager.LoadScene("end_screen");
			}
				break;
		}

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
		bellScript.hasBeenInteracted = false;
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
		if (stateChangeInProgress)
		{
			stateChangeCounter += Time.deltaTime;
			if (stateChangeCounter >= stateChangeDuration)
			{
				stateChangeCounter = 0.0f;
				stateChangeInProgress = false;
				ChangeState(incomingState);
			}
		}
		switch (cafeState)
		{
			case CafeState.AllDrinksDone:
			{
				if (BlackCat != null)
				{
					BlackCat.transform.position = Vector3.MoveTowards(BlackCat.transform.position,
						BlackCatTargetPoint,
						Time.deltaTime * BlackCatSpeed);
					if (Vector3.Distance(BlackCat.transform.position, BlackCatTargetPoint) < 0.1f)
					{
						ChangeState(CafeState.Epiloque);
					}
					Debug.DrawLine(BlackCat.transform.position, BlackCatTargetPoint, Color.red);
				}
			}
				break;
		}
	}
}
