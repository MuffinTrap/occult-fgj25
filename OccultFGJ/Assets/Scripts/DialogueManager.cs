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
	
	
	public void StartDialogue(GameObject character, Dialogue dialogue)
	{
		state = DialogueState.Ongoing;
		speechBubble.SetActive(true);

		textArea = speechBubble.GetComponentInChildren<Text>();
		speechBubble.transform.position = Camera.main.WorldToScreenPoint(character.transform.position);
		textArea.text = dialogue.text[dialogue.textIterator];
	}
	
	public void ProgressDialogue(Dialogue dialogue)
	{
		dialogue.textIterator++;
		if (dialogue.textIterator >= dialogue.text.Length)
		{
			EndDialogue(dialogue);
			return;
		}

		textArea.text = dialogue.text[dialogue.textIterator];
		
	}

	public void EndDialogue(Dialogue dialogue)
	{
		state = DialogueState.None;
		speechBubble.SetActive(false);
		dialogue.textIterator = 0;
	}
}

