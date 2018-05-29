using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	private PlayerController player1;
	private PlayerController player2;
	private float loose_rage = 100;
	private int winner = 0;

	// Use this for initialization
	void Awake () {
		player1 = GameObject.Find ("player1_iso").GetComponent<PlayerController>();
		player2 = GameObject.Find ("player2_iso").GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
		if (player1.GetRage () >= loose_rage || player2.GetRage () >= loose_rage) {
			if (player1.GetRage () >= loose_rage) {
				winner = 2;
			} else {
				winner = 1;
			}
			SceneManager.LoadScene (1);
		}
	}
}
