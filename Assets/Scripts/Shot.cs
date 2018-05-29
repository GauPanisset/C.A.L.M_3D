using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour {

	private DataController dataController = new DataController ();
	private int m_id = 0;
	private Vector3 m_vect;
	private int idSource;

	void Start () {
		
		WeaponOnPlayer weapon = dataController.SearchID (m_id);
		int idSpr = weapon.GetidSprShot ();
		string pathSpr = weapon.GetpathSprShot ();
		this.gameObject.GetComponent<SpriteRenderer> ().sprite = Resources.LoadAll<Sprite> ( pathSpr)[idSpr];
		this.gameObject.GetComponent<Rigidbody> ().velocity = new Vector3(m_vect.x*weapon.Getspeedprojectile (),0,m_vect.z*weapon.Getspeedprojectile ());
	}
		
	public void Set(int idWeapon, int idPlayer, int h, int v) {
		m_id = idWeapon;
		idSource = idPlayer;
		m_vect = new Vector3 (h, 0, v);
	}

	public int GetID() {
		return m_id;
	}

	public int GetSource() {
		return idSource;
	}
}
