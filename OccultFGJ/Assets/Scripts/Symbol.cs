﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Symbol : MonoBehaviour
{
	
	bool highlighted = false;
	private float zRotate = 0.0f;
	private float slowTime = 0.0f;	

	private CafeLogic logic;
	public bool hasBeenInteracted = false;

	// Use this for initialization
	void Start () {
		logic = GameObject.Find("GameLogic").GetComponent<CafeLogic>();
	}
	
	// Update is called once per frame
	
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

	public void OnSymbolInteracted()
	{
		if (!hasBeenInteracted)
		{
			logic.AddSymbol();
			hasBeenInteracted = true;
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
