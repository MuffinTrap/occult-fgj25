using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTree : MonoBehaviour {

	public Dialogue[] tree;
	public int treeIterator = 0; 

	public DialogueManager dialogueManager;

	private void Start() {
		dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
	}

	public Dialogue GetDialogue()
	{
		return tree[0];
	}

	public Dialogue NextLine()
	{
		Dialogue dialogue = tree[treeIterator];
		dialogue.lineIterator++;
		if (dialogue.lineIterator >= dialogue.textLines.Length)
		{
			dialogue = GetNextDialogue();
		}
		return dialogue;
	}

	public Dialogue GetNextDialogue()
	{
		treeIterator++;
		if (treeIterator >= tree.Length)
		{
			ResetTree();
			dialogueManager.EndDialogue();
			return null;
		}
		return tree[treeIterator];
	}

	public void ResetTree()
	{
		treeIterator = 0;
		foreach (Dialogue dialogue in tree)
		{
			dialogue.lineIterator = 0;
		}
	}
}
