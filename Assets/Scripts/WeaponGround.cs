using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponGround : MonoBehaviour {

	private DataController dataController = new DataController ();

	public int ID = 0;
	private bool created = false;
	private float timer;
	private float timeLimite = 2.0f;
	Vector3 m_vect;

	void Start () {

		WeaponOnPlayer weapon = dataController.SearchID (ID);
		int idSpr = weapon.GetidSprWeapon ();
		string pathSpr = weapon.GetpathSprWeapon ();
		this.gameObject.GetComponent<SpriteRenderer> ().sprite = Resources.LoadAll<Sprite> ( pathSpr)[idSpr];
		this.gameObject.GetComponent<Rigidbody> ().AddForce(m_vect, ForceMode.Impulse);
	}

	void Update() {
		if (Time.time - timer > timeLimite) {
			created = false;
		}

	}

	void FixedUpdate() {
		if (created) {
			this.gameObject.GetComponent<Rigidbody> ().useGravity = true;
			this.gameObject.GetComponent<Rigidbody> ().isKinematic = false;
			this.gameObject.GetComponent<Collider> ().isTrigger = false;
		} else {
			this.gameObject.GetComponent<Rigidbody> ().useGravity = false;
			this.gameObject.GetComponent<Rigidbody> ().isKinematic = true;
			this.gameObject.GetComponent<Collider> ().isTrigger = true;
		}

	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Player") {
			PlayerController playerController = other.GetComponentInParent<PlayerController>();

			if (playerController.GetAvaible()) {
				playerController.GetNewWeapon (ID);
				Destroy (gameObject);
			}
		}
	}

	public void SetCreated(int h, int v) {
		created = true;
		timer = Time.time;
		m_vect = new Vector3 (-h*80, 400, -v*80);
	}



}
