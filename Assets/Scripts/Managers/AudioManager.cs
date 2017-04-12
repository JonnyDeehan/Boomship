using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	public SoundEffect[] soundEffects;
	private AudioSource currentSource;

	public void PlayAudio(string audioName){
		foreach (SoundEffect effect in soundEffects) {
			if (effect.name.Equals (audioName)) {
				currentSource = effect.audio.GetComponent<AudioSource>();
				currentSource.Play ();
			}
		}
	}
}

[System.Serializable]
public class SoundEffect{
	public string name;
	public GameObject audio;
}