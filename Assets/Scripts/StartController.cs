using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartController : MonoBehaviour {

	private GameController gameController;

	public Image background;
	public Button m_option;
	public Button m_quit;
	// Use this for initialization
	void Start () {

		gameController = GameObject.Find("GameController").GetComponent<GameController>();
		m_option.onClick.AddListener (() => {gameController.Option ();});
		m_quit.onClick.AddListener (() => {gameController.QuitGame();});
	}

	// Update is called once per frame
	void Update () {

	}
}
