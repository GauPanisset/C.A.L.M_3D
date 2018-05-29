using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructionByContact : MonoBehaviour {

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Obstacle") {
			Destroy (gameObject);
		}
		if (other.gameObject.tag == "Player") {
			PlayerController playerController = other.GetComponentInParent<PlayerController> ();
			Shot shot = GetComponentInParent<Shot> ();
			Vector3 direction = shot.GetComponent<Rigidbody>().velocity.normalized;
			if (playerController.GetID () != shot.GetSource ()) {
				playerController.GetHit (direction, shot.GetID ());
				Destroy (gameObject);
			}
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if (other.gameObject.tag == "Map") {
			Destroy (gameObject);
		}
	}
		
}
