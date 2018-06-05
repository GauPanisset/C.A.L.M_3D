using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructionByContact : MonoBehaviour {

	//public AudioSource audio;

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Obstacle") {
			Destroy (gameObject);
		}
		if (other.gameObject.tag == "Player") {
			PlayerController playerController = other.GetComponentInParent<PlayerController> ();
			Shot shot = GetComponentInParent<Shot> ();

			if (playerController.GetID () != shot.GetSource ()) {
				Vector3 direction = shot.GetComponent<Rigidbody>().velocity.normalized;
				RageManager ragemanager = GameObject.Find("Canvas").GetComponent<RageManager>();

				playerController.GetHit (direction, shot.GetID ());
				Destroy (gameObject);
				ragemanager.AddRage(shot.GetID(),shot.GetSource ());
			}
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if (other.gameObject.tag == "Map") {
			Destroy (gameObject);
		}
	}
		
}
