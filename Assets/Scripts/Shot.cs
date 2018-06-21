using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour {

	public SoundController sound;

	private DataController dataController = new DataController ();
	private int m_id = 0;
	private string m_type;
	private float m_timeCreation;
	private Vector3 m_vect;
	private int idSource;
	private AudioClip m_audioEx;
	private AudioClip m_audioSht;

	void Start () {
		
		WeaponOnPlayer weapon = dataController.SearchID (m_id);
		int idSpr = weapon.GetidSprShot ();
		m_type = weapon.Gettype ();
		m_timeCreation = Time.time;
		if (m_type == "distance" ) {
			string pathSpr = weapon.GetpathSprShot ();
			this.gameObject.GetComponent<SpriteRenderer> ().sprite = Resources.LoadAll<Sprite> (pathSpr) [idSpr];
			this.gameObject.GetComponent<Rigidbody> ().velocity = new Vector3 (m_vect.x * weapon.Getspeedprojectile (), 0, m_vect.z * weapon.Getspeedprojectile ());
		} else {
			this.gameObject.GetComponent<Rigidbody> ().velocity = new Vector3 (m_vect.x * 100, 0, m_vect.z * 100);
		}
		string pathSndEx = weapon.GetpathSndEx ();
		if (pathSndEx != null) {
			m_audioEx = Resources.Load<AudioClip> (pathSndEx);
		} else {
			m_audioEx = null;
		}

		string pathSndSht = weapon.GetpathSndSht ();
		if (pathSndSht != null) {
			m_audioSht = Resources.Load<AudioClip> (pathSndSht);
			SoundController clone = GameObject.Instantiate<SoundController> (sound, GetComponent<Transform> ().position, Quaternion.identity);
			clone.SetAudio (m_audioSht);

		} else {
			m_audioSht = null;
		}
	}

	void FixedUpdate () {
		if ((m_type == "melee") && (Time.time - m_timeCreation > 0.02f)) {
			Destroy (gameObject);
		}

	}
		
	public void Set(int idWeapon, int idPlayer, int h, int v) {
		m_id = idWeapon;
		idSource = idPlayer;
		m_vect = new Vector3 (h, 0, v);
	}

	private void Initialisation() {

	}

	public int GetID() {
		return m_id;
	}

	public int GetSource() {
		return idSource;
	}

	public AudioClip GetAudio() {
		return m_audioEx;
	}

}
