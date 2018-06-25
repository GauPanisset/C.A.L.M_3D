using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private WeaponOnPlayer[] weapon = new WeaponOnPlayer[2];
	private Rigidbody rigidBody;
	private Animator animator;
	private RageManager ragemanager;
	private GameController gameController;
	private SpriteRenderer sprite;
	private Color colorS;

	private int h_direction;
	private int v_direction = -1;
	private int actualWeapon = 0;
	private Vector3 forceHit;
	private float nextFire;
	private bool gettingHit = false;
	private bool dashing=false;

    public float maxSpeed = 5.0f;
	public int ID;
	public Shot shot;
	public WeaponGround weaponGround;
	public TextMesh player_text;

    private float dashSpeed = 80;
    private float dashTime;
    public float dashLength;
    private float nextDash = 0;
	private float dashRate = 1.5f;

    // Use this for initialization
    void Start () {

		for (int i=0;i<2;i++){
			weapon [i] = DataController.SearchID(0);
		}

        rigidBody = GetComponent<Rigidbody>();
		animator = GetComponentInChildren<Animator> ();
		sprite = GetComponentInChildren<SpriteRenderer> ();
		colorS = sprite.color;

        ragemanager = GameObject.Find("Canvas").GetComponent<RageManager>();
		ragemanager.SetWeapon (weapon[actualWeapon].Getname(), weapon[actualWeapon].GetpathSprWeapon(), weapon[actualWeapon].GetidSprWeapon(),ID);
		gameController = GameObject.Find("GameController").GetComponent<GameController>();
		player_text.text = "<"+gameController.GetName (ID)+">";
		player_text.transform.position = GetComponent<Transform> ().position + new Vector3(0f,3f,1.5f);
	}
	
	// Update is called once per frame
	void Update () {
		ragemanager.SetWeapon (weapon[actualWeapon].Getname(), weapon[actualWeapon].GetpathSprWeapon(), weapon[actualWeapon].GetidSprWeapon(),ID);
		BoolAnimatorToDirection ();

		if (InputManager.Fire(ID))
		{
			Fire (h_direction,v_direction);
		}
		if (InputManager.Switch(ID))
		{
			Switch();
		}
		if (InputManager.Drop(ID)) {
			DropWeapon (h_direction,v_direction);
		}
	}

    private void FixedUpdate()
    {
		EffetRage ();
        BoolAnimatorToDirection();

		if (InputManager.Dash(ID) && Time.time > nextDash) {
			IEnumerator coroutine = Dash_Player (h_direction, v_direction, Time.time);
			StartCoroutine (coroutine);
		}

		if (!dashing) {
			MovePlayer (InputManager.MoveH(ID), InputManager.MoveV(ID));
		}
			
		if (gettingHit) {
			rigidBody.AddForce (forceHit, ForceMode.Impulse);
			gettingHit = false;
		}
    }

	private void MovePlayer( float h, float v)
	{
		rigidBody.velocity = new Vector3 (h * maxSpeed, 0, v * maxSpeed);
		SetBool_H_V (h, v);
	}

	IEnumerator Dash_Player (float h, float v, float startTime) {
		dashing = true;
		nextDash = Time.time + dashRate;
		dashTime = dashLength / dashSpeed;
	
		while (Time.time < startTime + dashTime) {
			rigidBody.velocity = new Vector3 (h * dashSpeed, 0, v * dashSpeed);
			yield return null;
		} 
		rigidBody.velocity = new Vector3 (0, 0, 0);
		dashing = false;
	}

    private void PutBool4Directions_False() {
		animator.SetBool("GoUp", false);
		animator.SetBool("GoDown", false);
		animator.SetBool("GoLeft", false);
		animator.SetBool("GoRight", false);
    }

	private void SetBool_V(float h, float v)
	{
		if (v > 0) {
			animator.SetBool ("GoUp", true);
			animator.SetBool ("GoDown", false);
            if (h > 0) {
            	animator.SetBool("GoUpRight", true);
                animator.SetBool("GoUpLeft", false);
                PutBool4Directions_False();
            } else if (h < 0) {
                animator.SetBool("GoUpLeft", true);
                animator.SetBool("GoUpRight", false);
                PutBool4Directions_False();
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
                PutBool4Directions_False();
            } else if (h < 0) {
                animator.SetBool("GoDownLeft", true);
                animator.SetBool("GoDownRight", false);
                PutBool4Directions_False();
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


	private void SetBool_H_V(float h, float v)
	{
		if (h > 0) {
			animator.SetBool ("GoLeft", false);
			animator.SetBool ("GoRight", true);
			SetBool_V (h, v);
		} else if (h < 0) {
			animator.SetBool ("GoLeft", true);
			animator.SetBool ("GoRight", false);
			SetBool_V (h, v);
		} else {
			animator.SetBool ("GoLeft", false);
			animator.SetBool ("GoRight", false);
			SetBool_V (h, v);
		}
        if (h == 0 && v == 0) {
            animator.SetBool("Move", false);
        } else {
            animator.SetBool("Move", true);
        }
	}

	private void Fire(int h, int v){
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

	private void Switch()
	{
		if (actualWeapon == 0) {
			actualWeapon = 1;
		} else {
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
			weapon [actualWeapon] = DataController.SearchID(ID);
		} else {
			weapon[1-actualWeapon] = DataController.SearchID(ID);
		}

	}

	private void DropWeapon(int h, int v)
	{
		if (weapon [actualWeapon].GetID () != 0) {
			WeaponGround clone;
			clone = GameObject.Instantiate<WeaponGround> (weaponGround, GetComponent<Transform> ().position, Quaternion.identity);
			clone.ID = weapon [actualWeapon].GetID ();
			clone.SetCreated (h,v);
			weapon [actualWeapon] = DataController.SearchID (0);
		}
	}

	public void GetHit(Vector3 direction, int IDweapon) {
		gettingHit = true;
		forceHit = DataController.SearchID(IDweapon).Getforce() * direction ;
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

	private void EffetRage(){
		float rage = ragemanager.GetRage (ID);
		rage = rage / 100f;
		colorS = new Vector4 (1f, 1f - rage, 1f - rage, 1f);
		sprite.color = colorS;
	}
		
}