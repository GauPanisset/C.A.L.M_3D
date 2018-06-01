using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	private int m_winner = 0;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void EndGame() {
		DontDestroyOnLoad (this.gameObject);
		SceneManager.LoadScene (1);
	}

	public void Restart() {
		SceneManager.LoadScene (0);
	}

	public void QuitGame() {
		Application.Quit();
	}

	public int GetWinner() {
		return m_winner;
	}

	public void SetWinner(int looser) {
		if (looser == 1) {
			m_winner = 2;
		} else {
			m_winner = 1;
		}
	}
}
