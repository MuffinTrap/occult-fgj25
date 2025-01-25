using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using UnityEngine.UI;
using UnityEngine;

public class DialogueManager : MonoBehaviour {

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
						new string[] {"HISS"}, 
						new string[] {"Choice1", "Choice2"});
	[SerializeField] public GameObject speechBubble;
	private Text textArea;
	
	public void StartDialogue(GameObject character)
	{
		speechBubble.SetActive(true);

		textArea = speechBubble.GetComponentInChildren<Text>();
		speechBubble.transform.position = Camera.main.WorldToScreenPoint(character.transform.position);
		textArea.text = dialogue.text[0];
	}
	
	public void ContinueDialogue()
	{
		// continue dialogue
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