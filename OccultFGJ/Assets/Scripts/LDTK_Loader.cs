using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LDTK_Loader : MonoBehaviour
{
	[SerializeField]
	private GameObject collisionBlockPrefab;

	[SerializeField] private GameObject mapSprite;
	// Use this for initialization
	void Start ()
	{
		LoadMap("LDTK_Maps/Level_0");
	}

	public void LoadMap(string folderName)
	{
		string bgPath = "_composite.png";
		string collision = "Collision.csv";
		string datafile = "data.json";
		string path = "Assets/" + folderName + "/";

		Bounds spriteBoundsWorld = mapSprite.GetComponent<SpriteRenderer>().bounds;
		Vector3 spriteTopLeft = spriteBoundsWorld.center +
		                        new Vector3(-spriteBoundsWorld.extents.x, spriteBoundsWorld.extents.y, 0.0f);
		Vector2 bsize = collisionBlockPrefab.GetComponent<BoxCollider2D>().size;
		Vector3 collisionBlockSize = new Vector3(bsize.x, bsize.y, 0.0f);
		Vector3 blockStep = new Vector3(collisionBlockSize.x, 0.0f, 0.0f);
		Vector3 firstBlockCenter = spriteTopLeft;// + collisionBlockSize * 0.5f;
		
		Debug.Log("First block at " +firstBlockCenter.ToString());
		Debug.Log("Collision size is " + 
			collisionBlockSize.ToString());
		
		using (StreamReader reader = new StreamReader(path + collision))
		{
			while (reader.EndOfStream == false)
			{
				string line = reader.ReadLine();

				string[] values = line.Split(',');
				foreach (string tileNumber in values)
				{
					// Create collider prefabs
					int c = 0;
					if (int.TryParse(tileNumber, out c))
					{
						if (c == 1)
						{
							Instantiate(collisionBlockPrefab, firstBlockCenter, Quaternion.identity);
						}
					}

					firstBlockCenter += blockStep;
					Debug.Log("First block at " + firstBlockCenter.ToString());
				}
				Debug.Log("Line done");
				// Line read
				// Reset to left
				firstBlockCenter = new Vector3(spriteTopLeft.x + blockStep.x * 0.5f, firstBlockCenter.y, 0.0f);
				// Move down
				firstBlockCenter -= new Vector3(0.0f, collisionBlockSize.y, 0.0f);
				Debug.Log("First block at " + firstBlockCenter.ToString());
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
