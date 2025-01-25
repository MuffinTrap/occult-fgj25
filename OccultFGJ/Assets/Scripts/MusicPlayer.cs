using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {

	
	// Use this for initialization
	void Start () {
		AudioSource music = GetComponent<AudioSource>();
		music.volume = PlayerPrefs.GetFloat("MusicVolume");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
