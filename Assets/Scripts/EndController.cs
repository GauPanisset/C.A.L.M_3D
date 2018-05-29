using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndController : MonoBehaviour {

	public Image background;
	public Button m_restart;
	// Use this for initialization
	void Start () {
		
		m_restart.onClick.AddListener (() => {Restart ();});
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void Restart() {
		SceneManager.LoadScene (0);
	}
}
