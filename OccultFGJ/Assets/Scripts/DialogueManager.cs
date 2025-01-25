using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using UnityEngine.UI;
using UnityEngine;

public class DialogueManager : MonoBehaviour {

	[SerializeField] private Text text;
	private string path = "Assets/Text/dialogue.xml";
	
	private void Awake() {
		text = GetComponent<Text>();

        //Load
		using (StreamReader reader = new StreamReader(path))
		{
			while (reader.EndOfStream == false)
			{
				string line = reader.ReadToEnd();
				text.text = line;



			}
		}
    }

}
