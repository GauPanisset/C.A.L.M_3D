using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndController : MonoBehaviour {

	private GameController gameController;

	public Image background;
	public Button m_restart;
	public Button m_quit;
	public Text m_text;
	// Use this for initialization
	void Start () {
		
		gameController = GameObject.Find("GameController").GetComponent<GameController>();
		m_text.text = "Le joueur " + gameController.GetWinner () + " a gagné !";
		m_restart.onClick.AddListener (() => {gameController.Restart ();});
		m_quit.onClick.AddListener (() => {gameController.QuitGame ();});
	}
	
	// Update is called once per frame
	void Update () {
		
	}
		
}
