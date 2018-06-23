using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Controller : MonoBehaviour {

	private Rigidbody rigidBody;
	private Animator animator;

	public float maxSpeed = 5.0f;
	private bool facingRight = false;

	private GameObject target;
	private SpriteRenderer sprite;
	// Use this for initialization
	void Start () {

		rigidBody = GetComponent<Rigidbody> ();
		animator = GetComponentInChildren<Animator> ();
		sprite = GetComponentInChildren<SpriteRenderer> ();

		target = GameObject.Find ("Player2");
	}

	// Update is called once per frame
	void Update () {
	}

	private void FixedUpdate()
	{
		//Valeur entre -1 et 1 selon intentsité de frappe sur l'axe horizontal
		float h = Input.GetAxis ("Horizontal_P1");
		float v = Input.GetAxis ("Vertical_P1");
		//Fonction responsable du mouvement

		MovePlayer (h, v);
		float distance = Vector3.Distance (target.transform.position, transform.position);
		float direction = Vector3.Dot ((target.transform.position - transform.position).normalized, new Vector3(h, 0, v).normalized);
		if (Input.GetAxis ("Fire1_P1") > 0) {
			Fire ();
			if (distance < 1.0f && Mathf.Abs (direction) > 0.9f) {
				Debug.Log ("Hit !");
			}
		}
	}

	private void MovePlayer( float h, float v)
	{
		rigidBody.velocity = new Vector3 (h * maxSpeed, 0, v * maxSpeed);
		SetBool_H_V(h, v);
		//Si on veut utiliser un miroir avec les sprites il faut ces lignes de code
		if ((h > 0 && facingRight) || (h < 0 && !facingRight))
		{
			Flip();
		}
	}

	private void SetBool_V(float v)
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

	private void SetBool_H_V(float h, float v)
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

	private void Flip()
	{
		sprite.flipX = !sprite.flipX;
		facingRight = !facingRight;
		/*Vector3 s = transform.localScale;
		s.x *= -1;
		transform.localScale = s;*/
	}

	private void Fire(){
	}


}
