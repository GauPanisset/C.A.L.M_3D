using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponGround : MonoBehaviour {

	public int ID = 0;
	private bool created = false;
	private float timer;
	private Vector3 m_vect;
	private Rigidbody rigidBody;
	private BoxCollider capsule;
	private int layerMask = 1 << 9;
	private float distanceCapsule;

	void Start () {

		WeaponOnPlayer weapon = DataController.SearchID (ID);
		int idSpr = weapon.GetidSprWeapon ();
		string pathSpr = weapon.GetpathSprWeapon ();
		this.gameObject.GetComponent<SpriteRenderer> ().sprite = Resources.LoadAll<Sprite> ( pathSpr)[idSpr];

		rigidBody = GetComponent<Rigidbody>();
		rigidBody.AddForce(m_vect, ForceMode.Impulse);
		capsule = GetComponentInChildren<BoxCollider>();
		distanceCapsule = Mathf.Abs(capsule.transform.position.y - transform.position.y);
	}

	void Update() {
		if (Physics.Raycast (transform.position, transform.TransformDirection (Vector3.down), distanceCapsule +  2, layerMask)) {
			created = false;
			timer = Time.time;
		}


	}

	void FixedUpdate() {
		if (created) {
			rigidBody.useGravity = true;
			rigidBody.isKinematic = false;
			this.gameObject.GetComponentInChildren<Collider> ().isTrigger = false;
		} else {
			rigidBody.useGravity = false;
			this.gameObject.GetComponentInChildren<Collider> ().isTrigger = true;
			FloatingWeapong (Time.time - timer);
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
		m_vect = new Vector3 (-h*100, 600, -v*100);
	}

	private void FloatingWeapong(float time) {
		if (time < 0.3f) {
			rigidBody.velocity = new Vector3 (0, 5, 0);
		} else {
			rigidBody.velocity = new Vector3 (0, 0.5f*Mathf.Sin(time), 0);
		}
	}

}
