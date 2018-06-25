using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenController : MonoBehaviour {

	private Image BlackScreen;

	void Awake () {
		BlackScreen = GetComponentInChildren<Image> ();
		float startTime = Time.time;
		IEnumerator coroutine = Fondu_screen (BlackScreen, 2f, startTime);
		StartCoroutine (coroutine);
	}

	IEnumerator Fondu_screen (Image Screen, float fadeRate ,float fadeTime) {
		while (Screen.color.a != 0) {
			Color color = Screen.color;
			color.a = Mathf.Lerp (1.0f, 0.0f, fadeRate * (Time.time - fadeTime));
			yield return null;
		}
	}
}
