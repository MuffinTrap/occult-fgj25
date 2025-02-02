using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float speed;
	Camera camera;
	bool moveTargetReached = true;
	public float moveDirection;
	public Vector3 moveTarget;
	Rigidbody2D rb;
	SpriteRenderer spriteRenderer;
	Animator animator;

	DialogueManager dialogueManager;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		spriteRenderer = GetComponentInChildren<SpriteRenderer>();
		animator = GetComponentInChildren<Animator>();
		
		camera = Camera.main;
		dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

     void FixedUpdate()
    {
		if (dialogueManager.state == DialogueManager.DialogueState.None)
		{
			// keyboard movement
			moveDirection = Input.GetAxis("Horizontal");
			
			rb.velocity = new Vector2(moveDirection * speed, 0);
			Vector3 playerPosition = camera.WorldToScreenPoint(transform.position); // transforms transform.position to pixels

			if (moveDirection > 0)
			{
				spriteRenderer.flipX = true;
				animator.SetBool("isWalking", true);
			}
			else { 
				
			}
			//mouse movement
			if (Input.GetButtonDown("Fire1"))
			{
				
				moveTarget.x = Mathf.Clamp(Input.mousePosition.x, 406,970);
				moveTargetReached = false;

			}

			if (moveDirection < 0)
			{
				animator.SetBool("isWalking", true);
				spriteRenderer.flipX = false;
			}

			if (Math.Abs(moveDirection) == 0)
			{
				animator.SetBool("isWalking", false);
			}

			if (moveTargetReached == false)
			{
				if (moveTarget.x > playerPosition.x)
				{
					rb.velocity = new Vector2(1 * speed, 0);
					spriteRenderer.flipX = true;
				}
				else
				{
					rb.velocity = new Vector2(-1 * speed, 0);
					spriteRenderer.flipX = false;
				}

				if (Math.Abs(moveTarget.x - playerPosition.x) < 5)
				{
					moveTargetReached = true;
				}
			}
		}
		else 
		{
			// in dialogue 
			moveTargetReached = true;
			rb.velocity = new Vector2(0, 0);
		}
    }
}
