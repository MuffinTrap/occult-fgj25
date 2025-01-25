using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
	[SerializeField]
	private AudioClip cafeMusic;
	[SerializeField]
	private AudioClip spookyMusic;
	
	private AudioSource musicSource;
	
	// Use this for initialization
	void Start () {
		musicSource = GetComponent<AudioSource>();
		musicSource.volume = PlayerPrefs.GetFloat("MusicVolume");
		PlayCafeMusic();
	}

	public void PlayCafeMusic()
	{
		musicSource.clip = cafeMusic;
		musicSource.Play();
	}

	public void PlaySpookyMusic()
	{
		musicSource.clip = spookyMusic;
		musicSource.Play();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
