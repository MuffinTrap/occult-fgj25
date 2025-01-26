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

	private void Start() {
		audioSource = GetComponent<AudioSource>();
		if(audioSource == null)
		{
			audioSource = gameObject.AddComponent<AudioSource>();
		}
	}
	
	
	public void StartDialogue(GameObject character, Dialogue dialogue)
	{
		state = DialogueState.Ongoing;
		speechBubble.SetActive(true);

		textArea = speechBubble.GetComponentInChildren<Text>();
		speechBubble.transform.position = Camera.main.WorldToScreenPoint(character.transform.position);
		textArea.text = dialogue.textLines[dialogue.lineIterator];
		audioSource.PlayOneShot(dialogue.audioLines[dialogue.lineIterator], 1.0f);
	}
	
	public void ProgressDialogue(Dialogue dialogue)
	{
		dialogue.lineIterator++;
		if (dialogue.lineIterator >= dialogue.textLines.Length)
		{
			EndDialogue(dialogue);
			return;
		}

		textArea.text = dialogue.textLines[dialogue.lineIterator];
		audioSource.PlayOneShot(dialogue.audioLines[dialogue.lineIterator], 1.0f);		
	}

	public void EndDialogue(Dialogue dialogue)
	{
		state = DialogueState.None;
		speechBubble.SetActive(false);
		dialogue.lineIterator = 0;
	}
}

