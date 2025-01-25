using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float speed;
	Camera camera;
	bool moveTargetReached = true;
	public float moveDirection;
	Vector3 moveTarget;
	Rigidbody2D rb;
	SpriteRenderer spriteRenderer;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		spriteRenderer = GetComponentInChildren<SpriteRenderer>();
		
		camera = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

     void FixedUpdate()
    {
		// keyboard movement
		moveDirection = Input.GetAxis("Horizontal");
		
		rb.velocity = new Vector2(moveDirection * speed, 0);
		Vector3 playerPosition = camera.WorldToScreenPoint(transform.position); // transforms transform.position to pixels

		if (moveDirection > 0)
		{
			spriteRenderer.flipX = true;
		}
		else { 
			spriteRenderer.flipX = false;
		}
		//mouse movement
		if (Input.GetButtonDown("Fire1"))
		{
			moveTarget = Input.mousePosition;
			moveTargetReached = false;

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
}
