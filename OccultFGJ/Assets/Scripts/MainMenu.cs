using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

	private AudioSource menuMusicAudio;
	private Slider musicSlider;
	// Use this for initialization
	void Start () {
		menuMusicAudio = GetComponentInChildren<AudioSource>();
		
		musicSlider = GameObject.Find("MusicSlider").GetComponent<Slider>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void StartGame()
	{
		SceneManager.LoadScene("cafe_level");
		PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
		menuMusicAudio.Stop();
	}

	public void OnMusicVolumeChanged()
	{
		menuMusicAudio.volume = musicSlider.value;
	}
}
