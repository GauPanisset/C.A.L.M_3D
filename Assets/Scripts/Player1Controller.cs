using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Controller : MonoBehaviour {

	private WeaponOnPlayer[] weapon = new WeaponOnPlayer[2];
	private DataController dataController = new DataController ();
	private Rigidbody2D rigidBody;
	private Animator animator;
	private GameObject target;

	private int actualWeapon = 0;
	private float nextFire;

	public float maxSpeed = 5.0f;
	private bool facingRight = false;
	public Shot shot;
	public WeaponGround dropped;


	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody2D> ();
		animator = GetComponent<Animator> ();

		target = GameObject.Find ("Player2");

		for (int i=0;i<2;i++){
			weapon [i] = dataController.SearchID(0);
		}
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetButton("Fire_P1"))
		{
			int h = (int) Input.GetAxis ("Horizontal_P1");
			int v = (int) Input.GetAxis ("Vertical_P1");
			if (h == 0 && v == 0) {
				v = -1;
			}
			Fire (h,v);
		}
		if (Input.GetButtonDown("Switch_P1"))
		{
			Switch();
		}

		if (Input.GetButtonDown("Drop_P1")) {
			int h = (int) Input.GetAxis ("Horizontal_P1");
			int v = (int) Input.GetAxis ("Vertical_P1");
			if (h == 0 && v == 0) {
				v = -1;
			}
			DropWeapon (h,v);
		}
	}

	private void FixedUpdate()
	{
		//Valeur entre -1 et 1 selon intentsité de frappe sur l'axe horizontal
		float h = Input.GetAxis ("Horizontal_P1");
		float v = Input.GetAxis ("Vertical_P1");
		//Fonction responsable du mouvement

		MovePlayer (h, v);

	}

	void MovePlayer( float h, float v)
	{
		rigidBody.velocity = new Vector2 (h * maxSpeed, v * maxSpeed);
		SetBool_H_V(h, v);
		//Si on veut utiliser un miroir avec les sprites il faut ces lignes de code
		if ((h > 0 && facingRight) || (h < 0 && !facingRight))
		{
			Debug.Log ("Flip");
			Flip();
		}
	}

	void SetBool_V(float v)
	{
		if (v > 0) {
			animator.SetBool ("GoUp", true);
			animator.SetBool ("GoDown", false);
		} else if (v < 0) {
			animator.SetBool ("GoUp", false);
			animator.SetBool ("GoDown", true);
		} else {
			animator.SetBool ("GoUp", false);
			animator.SetBool ("GoDown", false);
		}
	}

	void SetBool_H_V(float h, float v)
	{
		if (h > 0) {
			animator.SetBool ("GoLeft", false);
			animator.SetBool ("GoRight", true);
			SetBool_V (v);
		} else if (h < 0) {
			animator.SetBool ("GoLeft", true);
			animator.SetBool ("GoRight", false);
			SetBool_V (v);
		} else {
			animator.SetBool ("GoLeft", false);
			animator.SetBool ("GoRight", false);
			SetBool_V (v);
		}
	}

	void Flip()
	{
		facingRight = !facingRight;
		Vector3 s = transform.localScale;
		s.x *= -1;
		transform.localScale = s;
	}

	private void Fire(int h, int v){

		if (Time.time > nextFire) {
			nextFire = Time.time + weapon[actualWeapon].GetfireRate();
			Shot clone;
			clone = GameObject.Instantiate<Shot>(shot, GetComponent<Transform> ().position, Quaternion.identity);
			clone.Set(weapon [actualWeapon].GetID(),h,v);
		}
	}

	private void Switch()
	{
		if (actualWeapon == 0){
			actualWeapon = 1;
		}else{
			actualWeapon = 0;
		}
	}

	public bool GetAvaible() {
		if (weapon [0].GetID() == 0 || (weapon [1].GetID() == 0)) {
			return true;
		} else {
			return false;
		}
	}

	public void GetNewWeapon(int ID){

		if (weapon [actualWeapon].GetID() == 0) {
			weapon [actualWeapon] = dataController.SearchID(ID);
		} else {
			weapon[1-actualWeapon] = dataController.SearchID(ID);
		}
	}

	private void DropWeapon(int h, int v)
	{
		if (weapon [actualWeapon].GetID() != 0) {
			Vector3 newVect;
			newVect = new Vector3 (GetComponent<Transform> ().position.x - h, GetComponent<Transform> ().position.y - v, GetComponent<Transform> ().position.z);

			WeaponGround clone;
			clone = GameObject.Instantiate<WeaponGround> (dropped, newVect, Quaternion.identity);
			clone.ID = weapon [actualWeapon].GetID();

			weapon [actualWeapon] = dataController.SearchID(0);
		}
	}
}
