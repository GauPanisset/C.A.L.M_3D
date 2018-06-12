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

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Option() {
		DontDestroyOnLoad (this.gameObject);
		SceneManager.LoadScene (1);
	}

	public void StartGame(string Name1, string Name2) {
		name_P1 = Name1;
		name_P2 = Name2;
		DontDestroyOnLoad (this.gameObject);
		SceneManager.LoadScene (2);
	}

	public void EndGame() {
		DontDestroyOnLoad (this.gameObject);
		SceneManager.LoadScene (3);
	}

	public void RestartGame() {
		SceneManager.LoadScene (0);
		Destroy (gameObject);
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
}
