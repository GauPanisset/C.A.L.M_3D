using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	private string m_winner = "";
	private string name_P1 = "";
	private string name_P2 = "";
	private float startTime;

	public static GameController instance = null;
	public AudioSource source_menu;
	public AudioSource source_game;
	public GameObject Blackscreen;

	void Awake() {
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
		IEnumerator coroutine = Transition_sound (source_game, source_menu, 0.5f, startTime + 0.5f, startTime);

		GameObject clone;
		clone = GameObject.Instantiate<GameObject> (Blackscreen, GetComponent<Transform> ().position, Quaternion.identity);

		StartCoroutine (coroutine);
		name_P1 = Name1;
		name_P2 = Name2;
		SceneManager.LoadScene (2);
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
		audioIn.volume = 0;
		while (audioOut.volume != 0 || audioIn.volume != 1) {
			audioOut.volume = Mathf.Lerp (1.0f, 0.0f, fadeRate * (Time.time - startOutTime));
			audioIn.volume = Mathf.Lerp (0.0f, 1.0f, fadeRate * (Time.time - startInTime));
			yield return null;
		} 
		audioOut.Stop ();
	}

	IEnumerator Softstart_sound (AudioSource audio, float fadeRate, float startInTime) {
		audio.Play ();
		audio.volume = 0;
		while (audio.volume != 1) {
			audio.volume = Mathf.Lerp (0.0f, 1.0f, fadeRate * (Time.time - startInTime));
			yield return null;
		} 
	}
}
