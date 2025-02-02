using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using UnityEngine.UI;
using UnityEngine;

public class DialogueManager : MonoBehaviour {

	public enum DialogueState
	{
		None,
		Ongoing,
		Choice,
	}

	[SerializeField] public DialogueState state = DialogueState.None;
	[SerializeField] public GameObject speechBubble;
	private Text textArea;
	private AudioSource audioSource;

	public GameObject partner;

	private void Start() {
		audioSource = GetComponent<AudioSource>();
		if(audioSource == null)
		{
			audioSource = gameObject.AddComponent<AudioSource>();
			audioSource.volume = PlayerPrefs.GetFloat("MusicVolume");
		}
	}

	public void StartDialogue(GameObject character, DialogueTree dialogueTree)
	{
		state = DialogueState.Ongoing;
		partner = character;
		speechBubble.SetActive(true);

		textArea = speechBubble.GetComponentInChildren<Text>();
		speechBubble.transform.position = Camera.main.WorldToScreenPoint(character.transform.position);

		ProgressDialogue(dialogueTree);
	}
	
	public void ProgressDialogue(DialogueTree dialogueTree)
	{
		Dialogue dialogue = dialogueTree.NextLine();
		if (dialogue == null)
		{
			return;
		}

		MoveSpeechBubble(dialogue);

		textArea.text = dialogue.textLines[dialogue.lineIterator];
		audioSource.clip = dialogue.audioLines[dialogue.lineIterator];
		audioSource.Play(0);
	}

	public void EndDialogue()
	{
		state = DialogueState.None;
		speechBubble.SetActive(false);

		if (partner.GetComponent<Character>().character == Character.Characters.Raye)
		{
			ChangeGameStateTo(CafeLogic.CafeState.RayeInteractionDone);
		}
	}

	public void ChangeGameStateTo(CafeLogic.CafeState newState)
	{
		GameObject.Find("GameLogic").GetComponent<CafeLogic>().ChangeState(newState);
	}

	public void MoveSpeechBubble(Dialogue dialogue)
	{
		CafeLogic logic = GameObject.Find("GameLogic").GetComponent<CafeLogic>();
		if(dialogue.speaker == Character.Characters.Leona)
		{
			GameObject player = GameObject.Find("PlayerCharacter");
			speechBubble.transform.position = Camera.main.WorldToScreenPoint(player.transform.position);
		}
		else if(dialogue.speaker == Character.Characters.Darkness)
		{
			speechBubble.transform.position = Camera.main.WorldToScreenPoint(logic.BlackCat.transform.position);
		}
		else if(dialogue.speaker == Character.Characters.Raye)
		{
			speechBubble.transform.position = Camera.main.WorldToScreenPoint(logic.RayeCat.transform.position);
		}
		else if(dialogue.speaker == Character.Characters.Blanche)
		{
			speechBubble.transform.position = Camera.main.WorldToScreenPoint(logic.BlancheCat.transform.position);
		}
		else if(dialogue.speaker == Character.Characters.Argent)
		{
			speechBubble.transform.position = Camera.main.WorldToScreenPoint(logic.Argent.transform.position);
		}
	}
}

