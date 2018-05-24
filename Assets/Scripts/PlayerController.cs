using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private Rigidbody rigidBody1;
	private Animator animator1;
	private Rigidbody rigidBody2;
	private Animator animator2;

    public float maxSpeed = 5.0f;
    private bool facingRight1 = false;
	private bool facingRight2 = false;

	private GameObject player1;
	private GameObject player2;

	private SpriteRenderer sprite1;
	private SpriteRenderer sprite2;
    // Use this for initialization
    void Start () {
		player1 = GameObject.Find ("Player1");
		player2 = GameObject.Find ("Player2");

        rigidBody1 = player1.GetComponent<Rigidbody>();
		animator1 = player1.GetComponentInChildren<Animator> ();
		rigidBody2 = player2.GetComponent<Rigidbody>();
		animator2 = player2.GetComponentInChildren<Animator> ();

		sprite1 = player1.GetComponentInChildren<SpriteRenderer> ();
		sprite2 = player2.GetComponentInChildren<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        //Valeur entre -1 et 1 selon intentsité de frappe sur l'axe horizontal
        float h1 = Input.GetAxis("Horizontal_P1");
		float v1 = Input.GetAxis ("Vertical_P1");
		float h2 = Input.GetAxis("Horizontal_P2");
		float v2 = Input.GetAxis ("Vertical_P2");
        //Fonction responsable du mouvement

		MovePlayer (h1, v1, 1);
		MovePlayer (h2, v2, 2);

		float distance = Vector3.Distance (player1.transform.position, player2.transform.position);
		float direction1 = Vector3.Dot ((player2.transform.position - player1.transform.position).normalized, new Vector3(h1, v1, 0).normalized);
		float direction2 = Vector3.Dot ((player1.transform.position - player2.transform.position).normalized, new Vector3(h2, v2, 0).normalized);

		if (Input.GetAxis ("Fire1_P1") > 0) {
			Fire ();
			if (distance < 1.0f && Mathf.Abs (direction1) > 0.9f) {
				Debug.Log ("Hit !");
			}
		}
		if (Input.GetAxis ("Fire1_P2") > 0) {
			Fire ();
			if (distance < 1.0f && Mathf.Abs (direction2) > 0.9f) {
				Debug.Log ("Hit !");
			}
		}
    }

	private void MovePlayer( float h, float v, int player)
	{
		if (player == 1) {
			rigidBody1.velocity = new Vector3 (h * maxSpeed, 0, v * maxSpeed);
			SetBool_H_V (h, v, 1);
			//Si on veut utiliser un miroir avec les sprites il faut ces lignes de code
			if ((h > 0 && facingRight1) || (h < 0 && !facingRight1)) {
				Flip (1);
			}
		} else {
			rigidBody2.velocity = new Vector3 (h * maxSpeed, 0,  v * maxSpeed);
			SetBool_H_V (h, v, 2);
			//Si on veut utiliser un miroir avec les sprites il faut ces lignes de code
			if ((h > 0 && facingRight2) || (h < 0 && !facingRight2)) {
				Flip (2);
			}
		}
	}

	private void SetBool_V(float v, int player)
	{
		if (player == 1) {
			if (v > 0) {
				animator1.SetBool ("GoUp", true);
				animator1.SetBool ("GoDown", false);
			} else if (v < 0) {
				animator1.SetBool ("GoUp", false);
				animator1.SetBool ("GoDown", true);
			} else {
				animator1.SetBool ("GoUp", false);
				animator1.SetBool ("GoDown", false);
			}
		} else {
			if (v > 0) {
				animator2.SetBool ("GoUp", true);
				animator2.SetBool ("GoDown", false);
			} else if (v < 0) {
				animator2.SetBool ("GoUp", false);
				animator2.SetBool ("GoDown", true);
			} else {
				animator2.SetBool ("GoUp", false);
				animator2.SetBool ("GoDown", false);
			}
		}
	}

	private void SetBool_H_V(float h, float v, int player)
	{
		if (player == 1) {
			if (h > 0) {
				animator1.SetBool ("GoLeft", false);
				animator1.SetBool ("GoRight", true);
				SetBool_V (v, 1);
			} else if (h < 0) {
				animator1.SetBool ("GoLeft", true);
				animator1.SetBool ("GoRight", false);
				SetBool_V (v, 1);
			} else {
				animator1.SetBool ("GoLeft", false);
				animator1.SetBool ("GoRight", false);
				SetBool_V (v, 1);
			}
		} else {
			if (h > 0) {
				animator2.SetBool ("GoLeft", false);
				animator2.SetBool ("GoRight", true);
				SetBool_V (v, 2);
			} else if (h < 0) {
				animator2.SetBool ("GoLeft", true);
				animator2.SetBool ("GoRight", false);
				SetBool_V (v, 2);
			} else {
				animator2.SetBool ("GoLeft", false);
				animator2.SetBool ("GoRight", false);
				SetBool_V (v, 2);
			}
		}
	}

	private void Flip(int player)
	{
		if (player == 1) {
			sprite1.flipX = !sprite1.flipX;
			facingRight1 = !facingRight1;
		} else {
			sprite2.flipX = !sprite2.flipX;
			facingRight2 = !facingRight2;
		}
	}

	private void Fire(){
	}


}