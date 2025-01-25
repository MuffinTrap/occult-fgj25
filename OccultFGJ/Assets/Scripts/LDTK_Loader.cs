using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LDTK_Loader : MonoBehaviour
{
	[SerializeField]
	private GameObject collisionBlockPrefab;

	[SerializeField]
	public string MapFolderName;

	// Use this for initialization
	void Start ()
	{
		if (string.IsNullOrEmpty(MapFolderName) == false)
		{
			LoadMap(MapFolderName);
		}
	}

	public void LoadMap(string folderName)
	{
		string collision = "Collision.csv";
		string datafile = "data.json";
		string path = "Assets/" + folderName + "/";

		// Size of the map background sprite in world units
		SpriteRenderer mapSprite = GetComponentInChildren<SpriteRenderer>();
		Bounds spriteBoundsWorld = mapSprite.bounds;
		
		// Top left of the sprite in world
		Vector3 spriteTopLeft = spriteBoundsWorld.center +
		                        new Vector3(-spriteBoundsWorld.extents.x, spriteBoundsWorld.extents.y, 0.0f);
		
		// Size of one collision block : should match one map tile
		// 0.64 is about 64 pixels
		Vector2 bsize = collisionBlockPrefab.GetComponent<BoxCollider2D>().size;
		Vector3 collisionBlockSize = new Vector3(bsize.x, bsize.y, 0.0f);
		
		// Steps to take on X and Y axis after each block insert
		Vector3 blockStepX = new Vector3(collisionBlockSize.x, 0.0f, 0.0f);
		Vector3 blockStepY = new Vector3(0.0f, -collisionBlockSize.y, 0.0f);
		
		// Block position is the center of the block: place the first block
		// half a size away from the top left corner
		
		Vector3 firstBlockPosition = spriteTopLeft + blockStepX * 0.5f + blockStepY * 0.5f;
		Vector3 blockInsertPosition = firstBlockPosition;
		
		/*
		Debug.Log("First block at " +firstBlockCenter.ToString());
		Debug.Log("Collision size is " + 
			collisionBlockSize.ToString());
			*/
		
		using (StreamReader reader = new StreamReader(path + collision))
		{
			// Read the whole file
			// each line is a row of blocks
			// The blocks start from upper left corner
			while (reader.EndOfStream == false)
			{
				string line = reader.ReadLine();

				string[] values = line.Split(',');
				int blockNumber = 0;
				foreach (string tileNumber in values)
				{
					// Each number is a tile number:
					// 0: No tile
					// 1: Collision
					if (int.TryParse(tileNumber, out blockNumber))
					{
                        // Create collider prefabs
						if (blockNumber == 1)
						{
							Instantiate(collisionBlockPrefab, blockInsertPosition, Quaternion.identity);
						}
					}

					blockInsertPosition += blockStepX;
					// Debug.Log("First block at " + firstBlockCenter.ToString());
				}
				// Debug.Log("Line done");
				// Line read
				// Reset to left
				blockInsertPosition = new Vector3(firstBlockPosition.x, blockInsertPosition.y, 0.0f);
				// Move down
				blockInsertPosition += blockStepY;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
