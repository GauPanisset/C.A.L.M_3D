using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	private string m_winner = "";
	private string name_P1 = "";
	private string name_P2 = "";
	private float startTime;

	public static GameController instance = null;
	public AudioSource source_menu;
	public AudioSource source_game;
	public AudioClip source_fight;
	public SoundController sound;

	void Awake() {
		DataController.Read ();
			Cursor.visible = false;
		if (instance == null) {
			instance = this;
		}
		else if (instance != this) {
			Destroy (gameObject);
		}

		DontDestroyOnLoad (gameObject);
	}
		

	// Use this for initialization
	void Start () {
		
		Reinitialisation ();
	}

	// Update is called once per frame
	void Update () {
		if (InputManager.Cancel()) {
			RestartGame();
		}
	}

	private void Reinitialisation() {
		m_winner = "";
		name_P1 = "";
		name_P2 = "";

		startTime = Time.time;
		IEnumerator coroutine = Softstart_sound (source_menu, 0.5f, startTime);
		StartCoroutine (coroutine);
		source_game.Stop ();
	}

	public void Option() {
		SceneManager.LoadScene (1);
	}

	public void StartGame(string Name1, string Name2) {

		startTime = Time.time;
		IEnumerator coroutine = Transition_sound (source_game, source_menu, 0.5f, startTime + 1f, startTime);
		IEnumerator coroutine2 = Delay_sound (source_fight, 1f);

		StartCoroutine (coroutine);
		StartCoroutine (coroutine2);
		name_P1 = Name1;
		name_P2 = Name2;
		SceneManager.LoadScene (2);
		IEnumerator coroutine3 = Screen_launch();
		StartCoroutine (coroutine3);

	}

	public void EndGame() {
		SceneManager.LoadScene (3);
	}

	public void RestartGame() {
		SceneManager.LoadScene (0);
		Reinitialisation ();
	}

	public void QuitGame() {
		Application.Quit();
	}

	public void newName(){
		Debug.Log ("name");
	}

	public string GetWinner() {
		return m_winner;
	}

	public void SetWinner(int looser) {
		if (looser == 1) {
			m_winner = name_P2;
		} else {
			m_winner = name_P1;
		}
	}

	public string GetName(int ID) {
		if (ID == 1) {
			return name_P1;
		} else if (ID == 2) {
			return name_P2;
		} else {
			return "";
		}
	}

	IEnumerator Transition_sound (AudioSource audioIn, AudioSource audioOut, float fadeRate, float startInTime, float startOutTime) {
		audioIn.Play ();
		audioIn.volume = 0f;
		while (audioOut.volume != 0f || audioIn.volume != 0.5f) {
			audioOut.volume = Mathf.Lerp (0.5f, 0.0f, fadeRate * (Time.time - startOutTime));
			audioIn.volume = Mathf.Lerp (0.0f, 0.5f, fadeRate * (Time.time - startInTime));
			yield return null;
		} 
		audioOut.Stop ();
	}

	IEnumerator Softstart_sound (AudioSource audio, float fadeRate, float startInTime) {
		audio.Play ();
		audio.volume = 0;
		while (audio.volume != 0.5f) {
			audio.volume = Mathf.Lerp (0.0f, 0.5f, fadeRate * (Time.time - startInTime));
			yield return null;
		} 
	}

	IEnumerator Delay_sound (AudioClip audio, float delayTime) {
		yield return new WaitForSeconds (delayTime);
		if (audio != null) {
			SoundController clone = GameObject.Instantiate<SoundController> (sound, GetComponent<Transform> ().position, Quaternion.identity);
			clone.SetAudio (audio);
		}
		yield return null;
	}

	IEnumerator Screen_launch () {
		yield return new WaitForSeconds (1f);

		RageManager rageManager = GameObject.Find("Canvas").GetComponent<RageManager>();
		rageManager.Sceen ();
	}
}
