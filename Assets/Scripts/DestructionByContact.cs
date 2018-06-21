using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructionByContact : MonoBehaviour {

	public SoundController sound;

	//public AudioSource audio;

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Obstacle") {
			Explosion ();
		}
		if (other.gameObject.tag == "Player") {
			PlayerController playerController = other.GetComponentInParent<PlayerController> ();
			Shot shot = GetComponentInParent<Shot> ();

			if (playerController.GetID () != shot.GetSource ()) {
				Vector3 direction = shot.GetComponent<Rigidbody>().velocity.normalized;
				RageManager ragemanager = GameObject.Find("Canvas").GetComponent<RageManager>();

				playerController.GetHit (direction, shot.GetID ());
				Explosion ();
				ragemanager.AddRage(shot.GetID(),shot.GetSource ());
			}
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if (other.gameObject.tag == "Map") {
			Explosion ();
		}
	}

	private void Explosion () {
		AudioClip audioEx = GetComponentInParent<Shot> ().GetAudio ();
		if (audioEx != null) {
			SoundController clone = GameObject.Instantiate<SoundController> (sound, GetComponent<Transform> ().position, Quaternion.identity);
			clone.SetAudio (audioEx);
		}
		Destroy (gameObject);
	}
		
}
