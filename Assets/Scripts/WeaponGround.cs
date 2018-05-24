using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponGround : MonoBehaviour {

	private PlayerController playerController;
	private DataController dataController = new DataController ();

	public int ID = 0;

	void Start () {

		GameObject playerControllerObject = GameObject.FindWithTag ("Player");
		try 
		{
			playerController = playerControllerObject.GetComponent<PlayerController>();
		}
		catch {
			Debug.Log ("Cannot find 'PlayerController' script");
		}
		WeaponOnPlayer weapon = dataController.SearchID (ID);
		int idSpr = weapon.GetidSprWeapon ();
		string pathSpr = weapon.GetpathSprWeapon ();
		this.gameObject.GetComponent<SpriteRenderer> ().sprite = Resources.LoadAll<Sprite> ( pathSpr)[idSpr];
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Player") {
			if (playerController.GetAvaible()) {
				playerController.GetNewWeapon (ID);
				Destroy (gameObject);
			}
		}
	}



}
