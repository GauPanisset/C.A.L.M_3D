using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

	private GameController gameController;

	public Image background;
	public Button m_start;
	public InputField input_P1;
	public InputField input_P2;

	// Use this for initialization
	void Start () {

		gameController = GameObject.Find("GameController").GetComponent<GameController>();
		m_start.interactable = false;
		m_start.onClick.AddListener (() => {gameController.StartGame (input_P1.text,input_P2.text);});

	}

	// Update is called once per frame
	void Update () {
		if (input_P1.text.Length > 0) {
			m_start.interactable = true;
		}
	}
}
