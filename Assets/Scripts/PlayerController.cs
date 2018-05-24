using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private Rigidbody m_RigidBody;
	private Animator m_Animator;
	private WeaponOnPlayer[] weapon = new WeaponOnPlayer[2];
	private DataController dataController = new DataController ();

	private int actualWeapon = 0;
	private float nextFire;

    public float maxSpeed = 5.0f;
	public Shot shot;
	public WeaponGround weaponGround;

    private bool facingRight = false;
    private bool facingDown = true;

    // Use this for initialization
    void Start () {
        m_RigidBody = GetComponent<Rigidbody>();
		m_Animator = GetComponentInChildren<Animator> ();

		for (int i=0;i<2;i++){
			weapon [i] = dataController.SearchID(0);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButton("Fire_P1"))
		{
			int h = (int) Input.GetAxis ("Horizontal");
			int v = (int) Input.GetAxis ("Vertical");
			if (h == 0 && v == 0) {
				v = -1;
			}
			Fire (h,v);
		}
		if (Input.GetButtonDown("Switch"))
		{
			Switch();
		}
		if (Input.GetButtonDown("Drop")) {
			int h = (int) Input.GetAxis ("Horizontal");
			int v = (int) Input.GetAxis ("Vertical");
			if (h == 0 && v == 0) {
				v = -1;
			}
			DropWeapon (h,v);
		}
	}

    private void FixedUpdate()
    {
        //Valeur entre -1 et 1 selon intentsité de frappe sur l'axe horizontal
        float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis ("Vertical");
        //Fonction responsable du mouvement
        MovePlayer(h, v);
    }

	void MovePlayer( float h, float v )
    {
        m_RigidBody.velocity = new Vector3(h * maxSpeed, 0, v * maxSpeed);

        SetBool_H_V(h, v);

    }

    void SetBool_V(float v)
    {
        if (v > 0)
        {
            m_Animator.SetBool("GoUp", true);
            m_Animator.SetBool("GoDown", false);
        }
        else if (v < 0)
        {
            m_Animator.SetBool("GoUp", false);
            m_Animator.SetBool("GoDown", true);
        }
        else
        {
            m_Animator.SetBool("GoUp", false);
            m_Animator.SetBool("GoDown", false);
        }

    }

    void SetBool_H_V(float h, float v)
    {
        if (h > 0)
        {
            m_Animator.SetBool("GoLeft", false);
            m_Animator.SetBool("GoRight", true);
            SetBool_V(v);
        }
        else if (h < 0)
        {
            m_Animator.SetBool("GoLeft", true);
            m_Animator.SetBool("GoRight", false);
            SetBool_V(v);
        }
        else
        {
            m_Animator.SetBool("GoLeft", false);
            m_Animator.SetBool("GoRight", false);
            SetBool_V(v);
        }
    }

    void FlipH()
    {
        facingRight = !facingRight;
        
        Vector3 s = transform.localScale;
        s.x *= -1;
        transform.localScale = s;
    }

    void FlipV()
    {
        facingDown = !facingDown;

        Vector3 s = transform.localScale;
        s.y *= -1;
        transform.localScale = s;
    }


	private void Fire(int h, int v){

		if (Time.time > nextFire) {
			nextFire = Time.time + weapon[actualWeapon].GetfireRate();
			Vector3 newVect = new Vector3 (GetComponent<Transform> ().position.x + 2*h, GetComponent<Transform> ().position.y, GetComponent<Transform> ().position.z + 4*v);
			Shot clone;
			clone = GameObject.Instantiate<Shot>(shot, newVect, Quaternion.identity);
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
			newVect = new Vector3 (GetComponent<Transform> ().position.x - 10*h, GetComponent<Transform> ().position.y, GetComponent<Transform> ().position.z-10*v);

			WeaponGround clone;
			clone = GameObject.Instantiate<WeaponGround> (weaponGround, newVect, Quaternion.identity);
			clone.ID = weapon [actualWeapon].GetID();

			weapon [actualWeapon] = dataController.SearchID(0);
		}
	}
}