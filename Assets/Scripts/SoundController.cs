using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour {

	private AudioSource m_audio;

	// Use this for initialization
	void Awake() {
		m_audio = gameObject.AddComponent<AudioSource>();
	}

	void Start() {
		IEnumerator coroutine = Explosion_sound (m_audio);
		StartCoroutine (coroutine);
	}

	public void SetAudio(AudioClip audio) {
		m_audio.clip = audio;
	}

	IEnumerator Explosion_sound (AudioSource audio) {
		audio.Play ();
		float duration = audio.clip.length;
		float timer = Time.time;
		while (Time.time - timer < duration) {

			yield return null;
		} 
		audio.Stop ();
		Destroy (gameObject);
	}

}
