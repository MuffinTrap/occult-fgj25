﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

	bool highlighted = false;

	private float zRotate = 0.0f;
	private float slowTime = 0.0f;
	// Use this for initialization
	void Start () {
		
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

	public void StartDialogue()
	{
		Debug.Log("StartDialogue called");
		DialogueManager dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
		dialogueManager.StartDialogue(gameObject);
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
