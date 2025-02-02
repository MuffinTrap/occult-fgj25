using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
	[SerializeField]
	private AudioClip cafeMusic;
	[SerializeField]
	private AudioClip spookyMusic;
	[SerializeField]
	private float volume;

	private enum MusicPlaying
	{
		None,
		PlayingCafeMusic,
		PlayingSpookyMusic
	}

	private MusicPlaying currentMusic;
	private AudioSource musicSource;
	
	// Use this for initialization
	void Start () {
		currentMusic = MusicPlaying.None;
		musicSource = GetComponent<AudioSource>();
		musicSource.volume = PlayerPrefs.GetFloat("MusicVolume") * volume;
		PlayCafeMusic();
	}

	public void PlayCafeMusic()
	{
		if (currentMusic != MusicPlaying.PlayingCafeMusic)
		{
			musicSource.clip = cafeMusic;
			musicSource.Play();
			currentMusic = MusicPlaying.PlayingCafeMusic;
		}
	}

	public void PlaySpookyMusic()
	{
		if (currentMusic != MusicPlaying.PlayingSpookyMusic)
		{
			musicSource.clip = spookyMusic;
			musicSource.Play();
			currentMusic = MusicPlaying.PlayingSpookyMusic;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
