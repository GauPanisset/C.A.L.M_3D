using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour {

	private DataController dataController = new DataController ();
	private int m_id = 0;
	private Vector3 m_vect;

	void Start () {
		WeaponOnPlayer weapon = dataController.SearchID (m_id);
		int idSpr = weapon.GetidSprShot ();
		string pathSpr = weapon.GetpathSprShot ();
		this.gameObject.GetComponent<SpriteRenderer> ().sprite = Resources.LoadAll<Sprite> ( pathSpr)[idSpr];
		this.gameObject.GetComponent<Rigidbody> ().velocity = new Vector3(m_vect.x*weapon.Getspeedprojectile (),0,m_vect.z*weapon.Getspeedprojectile ());
	}
		
	public void Set(int id, int h, int v) {
		m_id = id;
		m_vect = new Vector3 (h, 0, v);
	}
		
}
