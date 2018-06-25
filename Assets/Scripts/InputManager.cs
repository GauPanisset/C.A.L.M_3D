using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class InputManager {

	static public bool Submit() {
		return Input.GetButtonDown("Submit");
	}

	static public bool Cancel() {
		return Input.GetButtonDown ("Cancel");
	}

	static public bool Fire(int ID) {
		if (ID == 1) {
			return Input.GetButton ("Fire_P1");
		} else {
			return Input.GetButton ("Fire_P2");
		}
	}

	static public bool Switch(int ID) {
		if (ID == 1) {
			return Input.GetButtonDown ("Switch_P1");
		} else {
			return Input.GetButtonDown ("Switch_P2");
		}
	}

	static public bool Drop(int ID) {
		if (ID == 1) {
			return Input.GetButtonDown ("Drop_P1");
		} else {
			return Input.GetButtonDown ("Drop_P2");
		}
	}

	static public bool Dash(int ID) {
		if (ID == 1) {
			return Input.GetButtonDown ("Dash_P1");
		} else {
			return Input.GetButtonDown ("Dash_P2");
		}
	}

	static public float MoveH(int ID) {
		if (ID == 1) {
			return Input.GetAxis ("Horizontal_P1");
		} else {
			return Input.GetAxis ("Horizontal_P2");
		}
	}

	static public float MoveV(int ID) {
		if (ID == 1) {
			return (Input.GetAxis ("Vertical_P1"));
		} else {
			return (Input.GetAxis ("Vertical_P2"));
		}
	}
}
