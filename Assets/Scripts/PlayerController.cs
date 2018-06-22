﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private DataController dataController = new DataController ();
	private WeaponOnPlayer[] weapon = new WeaponOnPlayer[2];
	private Rigidbody rigidBody;
	private Animator animator;
	private RageManager ragemanager;
	private GameController gameController;
	private SpriteRenderer sprite;
	private Color tmp;

	private int h_direction;
	private int v_direction = -1;
	private int actualWeapon = 0;
	private Vector3 forceHit;
	private float nextFire;
	private bool facingRight = false;
	private bool gettingHit = false;

    public float maxSpeed = 5.0f;
	public int ID;
	public Shot shot;
	public WeaponGround weaponGround;
	public TextMesh player_text;

    private float dashSpeed = 300;
    private float dashTime;
    public float dashLength;
    private float nextDash = 0;
	private float dashRate = 1.5f;

    // Use this for initialization
    void Start () {

		for (int i=0;i<2;i++){
			weapon [i] = dataController.SearchID(0);
		}

        rigidBody = GetComponent<Rigidbody>();
		animator = GetComponentInChildren<Animator> ();
		sprite = GetComponentInChildren<SpriteRenderer> ();
		tmp = sprite.color;

        ragemanager = GameObject.Find("Canvas").GetComponent<RageManager>();
		ragemanager.SetWeapon (weapon[actualWeapon].Getname(), weapon[actualWeapon].GetpathSprWeapon(), weapon[actualWeapon].GetidSprWeapon(),ID);
		gameController = GameObject.Find("GameController").GetComponent<GameController>();
		player_text.text = "<"+gameController.GetName (ID)+">";
		player_text.transform.position = GetComponent<Transform> ().position + new Vector3(0f,4f,1.5f);
	}
	
	// Update is called once per frame
	void Update () {
		ragemanager.SetWeapon (weapon[actualWeapon].Getname(), weapon[actualWeapon].GetpathSprWeapon(), weapon[actualWeapon].GetidSprWeapon(),ID);
		BoolAnimatorToDirection ();

		if (Input.GetButton("Fire_P1"))
		{
			Fire (h_direction,v_direction,1);
		}
		if (Input.GetButton("Fire_P2"))
		{
			Fire (h_direction,v_direction,2);
		}
		if (Input.GetButtonDown("Switch_P1"))
		{
			Switch(1);
		}
		if (Input.GetButtonDown("Switch_P2"))
		{
			Switch(2);
		}
		if (Input.GetButtonDown("Drop_P1")) {
			int h = (int) Input.GetAxis ("Horizontal_P1");
			int v = (int) Input.GetAxis ("Vertical_P1");
			if (h == 0 && v == 0) {
				v = -1;
			}
			DropWeapon (h,v,1);
		}
		if (Input.GetButtonDown("Drop_P2")) {
			int h = (int) Input.GetAxis ("Horizontal_P2");
			int v = (int) Input.GetAxis ("Vertical_P2");
			if (h == 0 && v == 0) {
				v = -1;
			}
			DropWeapon (h,v,2);
		}
	}

    private void FixedUpdate()
    {
        BoolAnimatorToDirection();
        //Valeur entre -1 et 1 selon intentsité de frappe sur l'axe horizontal
        float h1 = Input.GetAxis("Horizontal_P1");
		float v1 = Input.GetAxis ("Vertical_P1");
		float h2 = Input.GetAxis("Horizontal_P2");
		float v2 = Input.GetAxis ("Vertical_P2");
        //Fonction responsable du mouvement


		if (Input.GetButtonDown ("Dash_P1") && Time.time > nextDash) {
			IEnumerator coroutine = Dash_Player (h_direction, v_direction, 1, Time.time);
			StartCoroutine (coroutine);
		} else {
			MovePlayer (h1, v1, 1);
		}
			
		if (Input.GetButtonDown ("Dash_P2") && Time.time > nextDash) {
			IEnumerator coroutine = Dash_Player (h_direction, v_direction, 2, Time.time);
			StartCoroutine (coroutine);
		} else {
			MovePlayer (h2, v2, 2);
		}
			
		if (gettingHit) {
			rigidBody.AddForce (forceHit, ForceMode.Impulse);
			EffetRage (ID);
			gettingHit = false;
		}
    }

	private void MovePlayer( float h, float v, int player)
	{
		if (player == ID) {
			rigidBody.velocity = new Vector3 (h * maxSpeed, 0, v * maxSpeed);
			SetBool_H_V (h, v, player);
			//Si on veut utiliser un miroir avec les sprites il faut ces lignes de code
			/*if ((h > 0 && facingRight) || (h < 0 && !facingRight)) {
				Flip (player);
			}*/
		} 
	}

	IEnumerator Dash_Player (float h, float v, int player, float startTime) {
		if (ID == player) {
			nextDash = Time.time + dashRate;
			dashTime = dashLength / dashSpeed;

			while (Time.time < startTime + dashTime) {
				rigidBody.velocity = new Vector3 (h * dashSpeed, 0, v * dashSpeed);
				yield return null;
			} 
		}
	}

    private void PutBool4Directions_False(int player) {
        if (player == ID) {
            animator.SetBool("GoUp", false);
            animator.SetBool("GoDown", false);
            animator.SetBool("GoLeft", false);
            animator.SetBool("GoRight", false);
        }
    }

	private void SetBool_V(float h, float v, int player)
	{
		if (player == ID) {
			if (v > 0) {
				animator.SetBool ("GoUp", true);
				animator.SetBool ("GoDown", false);
                if (h > 0) {
                    animator.SetBool("GoUpRight", true);
                    animator.SetBool("GoUpLeft", false);
                    PutBool4Directions_False(ID);
                } else if (h < 0) {
                    animator.SetBool("GoUpLeft", true);
                    animator.SetBool("GoUpRight", false);
                    PutBool4Directions_False(ID);
                } else {
                    animator.SetBool("GoUpLeft", false);
                    animator.SetBool("GoUpRight", false);
                }
			} else if (v < 0) {
				animator.SetBool ("GoUp", false);
				animator.SetBool ("GoDown", true);
                if (h > 0) {
                    animator.SetBool("GoDownRight", true);
                    animator.SetBool("GoDownLeft", false);
                    PutBool4Directions_False(ID);
                } else if (h < 0) {
                    animator.SetBool("GoDownLeft", true);
                    animator.SetBool("GoDownRight", false);
                    PutBool4Directions_False(ID);
                } else {
                    animator.SetBool("GoDownLeft", false);
                    animator.SetBool("GoDownRight", false);
                }
            } else {
				animator.SetBool ("GoUp", false);
                animator.SetBool("GoDown", false);
                animator.SetBool("GoDownLeft", false);
                animator.SetBool("GoDownRight", false);
                animator.SetBool("GoUpLeft", false);
                animator.SetBool("GoUpRight", false);
            }
		}
	}

	private void SetBool_H_V(float h, float v, int player)
	{
		if (player == ID) {
			if (h > 0) {
				animator.SetBool ("GoLeft", false);
				animator.SetBool ("GoRight", true);
				SetBool_V (h, v, ID);
			} else if (h < 0) {
				animator.SetBool ("GoLeft", true);
				animator.SetBool ("GoRight", false);
				SetBool_V (h, v, ID);
			} else {
				animator.SetBool ("GoLeft", false);
				animator.SetBool ("GoRight", false);
				SetBool_V (h, v, ID);
			}
            if (h == 0 && v == 0) {
                animator.SetBool("Move", false);
            } else {
                animator.SetBool("Move", true);
            }
		}
	}

	private void Fire(int h, int v, int player){

		if (player == ID) {
			if (Time.time > nextFire) {
				nextFire = Time.time + weapon [actualWeapon].GetfireRate ();
				Shot clone;
				clone = GameObject.Instantiate<Shot> (shot, GetComponent<Transform> ().position, Quaternion.identity);
				clone.Set (weapon [actualWeapon].GetID (),ID, h, v);
				if (weapon[actualWeapon].Gettype() == "melee")
				{
					animator.SetBool("Cac", true);
				} else if (weapon[actualWeapon].Gettype() == "distance")
				{
					animator.SetBool("Fire", true);
				}
			}
        }
	}

	private void Switch(int player)
	{
		if (player == ID) {
			if (actualWeapon == 0) {
				actualWeapon = 1;
			} else {
				actualWeapon = 0;
			}
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

	private void DropWeapon(int h, int v, int player)
	{
		if (player == ID) {
			if (weapon [actualWeapon].GetID () != 0) {
				WeaponGround clone;
				clone = GameObject.Instantiate<WeaponGround> (weaponGround, GetComponent<Transform> ().position, Quaternion.identity);
				clone.ID = weapon [actualWeapon].GetID ();
				clone.SetCreated (h,v);

				weapon [actualWeapon] = dataController.SearchID (0);
			}
		}
	}

	private void Flip(int player)
	{
		if (player == ID) {
			sprite.flipX = false;
			facingRight = !facingRight;
		} 
	}

	public void GetHit(Vector3 direction, int IDweapon) {
		gettingHit = true;
		forceHit = dataController.SearchID(IDweapon).Getforce() * direction ;
	}

	public int GetID() {
		return ID;
	}

	private void BoolAnimatorToDirection() {
		if (animator.GetBool ("GoUp")) {
			h_direction = 0;
			v_direction = 1;
		}
		if (animator.GetBool ("GoDown")) {
			h_direction = 0;
			v_direction = -1;
		}
		if (animator.GetBool ("GoLeft")) {
			h_direction = -1;
			v_direction = 0;
		}
		if (animator.GetBool ("GoRight")) {
			h_direction = 1;
			v_direction = 0;
		}
		if (animator.GetBool ("GoUpLeft")) {
			h_direction = -1;
			v_direction = 1;
		}
		if (animator.GetBool ("GoUpRight")) {
			h_direction = 1;
			v_direction = 1;
		}
		if (animator.GetBool ("GoDownLeft")) {
			h_direction = -1;
			v_direction = -1;
		}
		if (animator.GetBool ("GoDownRight")) {
			h_direction = 1;
			v_direction = -1;
		}
		animator.SetBool("Cac", false);
		animator.SetBool("Fire", false);
	}

	private void EffetRage(int player){
		float rage = ragemanager.GetRage(player);
		rage = rage / 100f * 255f;
		tmp = new Vector4(255f, 255f - rage, 255f - rage, 255f);
		Debug.Log(tmp);
		sprite.color = tmp;
	}
		
}