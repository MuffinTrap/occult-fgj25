using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstatntiateOnClick : MonoBehaviour {

	[SerializeField] public GameObject instantiate;
	[SerializeField] public GameObject parent;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnMouseDown() {
		Debug.Log("Clicked on " + gameObject.name);
		GameObject go = Instantiate(instantiate, parent.transform);
		go.transform.position = gameObject.transform.position;
		
	}
}
