using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

	public enum Characters
	{
		None,
		Darkness,
		Raye,
		Blanche,
		Argent,
		Leona
		
	}
	public Characters character;
	bool highlighted = false;
	DialogueManager dialogueManager;

	private float zRotate = 0.0f;
	private float slowTime = 0.0f;
	// Use this for initialization
	void Start () {
		dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
	}
	
	// Update is called once per frame
	void Update () {
		if (highlighted)
		{
			zRotate = 5.0f * Mathf.Sin(Time.time * 10.0f);
			transform.eulerAngles = new Vector3(0.0f, 0.0f, zRotate);
		}
		else if (zRotate < float.Epsilon || zRotate > float.Epsilon)
		{
			slowTime += Time.deltaTime / 10.0f;
			zRotate = Mathf.Lerp(zRotate, 0.0f, slowTime);
			transform.eulerAngles = new Vector3(0.0f, 0.0f, zRotate);
		}
	}

	public void ProgressDialogue()
	{
		DialogueTree dialogueTree = gameObject.GetComponentInChildren<DialogueTree>();
		if (dialogueManager.state == DialogueManager.DialogueState.Ongoing)
		{
			dialogueManager.ProgressDialogue(dialogueTree);
		}
		else
		{
			dialogueManager.StartDialogue(gameObject, dialogueTree);
		}
	}
	

	public void EnableHighlight()
	{
		highlighted = true;
		
	}

	public void DisableHighlight()
	{
		highlighted = false;
	}
}
