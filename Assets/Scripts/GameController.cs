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
		source_menu.Play ();
		source_game.Play ();

	}

	// Update is called once per frame
	void Update () {
		
	}

	public void Option() {
		SceneManager.LoadScene (1);
	}

	public void StartGame(string Name1, string Name2) {

		startTime = Time.time;
		IEnumerator coroutineOut = Fade_sound (-1, source_menu, 0.08f, 0f);
		IEnumerator coroutineIn = Fade_sound (1, source_game, 0.08f, 1f);
		StartCoroutine (coroutineIn);
		StartCoroutine (coroutineOut);
		name_P1 = Name1;
		name_P2 = Name2;
		SceneManager.LoadScene (2);
	}

	public void EndGame() {
		SceneManager.LoadScene (3);
	}

	public void RestartGame() {
		SceneManager.LoadScene (0);
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

	IEnumerator Fade_sound (int Direction, AudioSource audio, float fadeRate, float waitingTime) {
		yield return new WaitForSeconds (waitingTime);
		while (audio.volume != Mathf.Clamp01(1*Direction)) {
			audio.volume = Mathf.Clamp01 (Mathf.Lerp (-1.0f * Direction, 1.0f * Direction, fadeRate * (Time.time - startTime)));
			yield return null;
		} 
	}
}
