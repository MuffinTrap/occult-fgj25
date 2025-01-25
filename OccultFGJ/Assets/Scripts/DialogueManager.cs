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

	public struct Dialogue
	{
		public string speaker;
		public string[] text;
		public string[] choices;

		public Dialogue(string speaker, string[] text, string[] choices) : this()
		{
			this.speaker = speaker;
			this.text = text;
			this.choices = choices;
		}
	}
	[SerializeField] public Dialogue dialogue 
		= new Dialogue("Leona", 
						new string[] {"HISS", "GRRR", "MEOW"}, 
						new string[] {"Choice1", "Choice2"});
	[SerializeField] public DialogueState state = DialogueState.None;
	[SerializeField] public GameObject speechBubble;
	[SerializeField] public int textIterator = 0;
	private Text textArea;
	
	public void StartDialogue(GameObject character)
	{
		state = DialogueState.Ongoing;
		speechBubble.SetActive(true);
		textIterator = 0;

		textArea = speechBubble.GetComponentInChildren<Text>();
		speechBubble.transform.position = Camera.main.WorldToScreenPoint(character.transform.position);
		textArea.text = dialogue.text[textIterator];
	}
	
	public void ProgressDialogue()
	{
		//Debug.Log("Progressing dialogue");
		textIterator++;
		if (textIterator < dialogue.text.Length)
		{
			textArea.text = dialogue.text[textIterator];
		}
		else
		{
			EndDialogue();
		}
	}

	public void EndDialogue()
	{
		state = DialogueState.None;
		speechBubble.SetActive(false);
		textIterator = 0;
	}
}



/*
void ReadXML() {

		string path = "Assets/Text/dialogue.xml";
		text = GetComponent<Text>();

        //Load
		using (StreamReader reader = new StreamReader(path))
		{
			while (reader.EndOfStream == false)
			{
				string lines = reader.ReadToEnd();

				XmlDocument doc = new XmlDocument();
				doc.LoadXml(lines);

				XmlNode root = doc.FirstChild;

				if (root.HasChildNodes)
				{
					for (int i = 0; i < root.ChildNodes.Count; i++)
					{
						Debug.Log(root.ChildNodes[i].InnerText);
					}
				}
			}
		}
    }
*/