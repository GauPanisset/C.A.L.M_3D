using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructionByContact : MonoBehaviour {

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Obstacle") {
			Destroy (gameObject);
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if (other.gameObject.tag == "Map") {
			Destroy (gameObject);
		}
	}
}
