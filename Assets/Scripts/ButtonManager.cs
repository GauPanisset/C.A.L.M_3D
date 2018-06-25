using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour, ISelectHandler {

	public void OnSelect(BaseEventData eventData) {
		if (InputManager.Submit ()) {
			GetComponentInParent<Button> ().Select ();
		}
	}
}
