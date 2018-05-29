using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private DataController dataController = new DataController ();
	private WeaponOnPlayer[] weapon = new WeaponOnPlayer[2];
	private Rigidbody rigidBody;
	private Animator animator;
	private RageManager ragemanager;
	private GameObject player;
	private SpriteRenderer sprite;

	private int actualWeapon = 0;
	private Vector3 forceHit;
	private float nextFire;
	private bool facingRight = false;
	private bool gettingHit = false;
	private float m_rage = 0;
	private float add_rage;

    public float maxSpeed = 5.0f;
	public int ID;
	public Shot shot;
	public WeaponGround weaponGround;

    // Use this for initialization
    void Start () {

		for (int i=0;i<2;i++){
			weapon [i] = dataController.SearchID(0);
		}

        rigidBody = GetComponent<Rigidbody>();
		animator = GetComponentInChildren<Animator> ();
		sprite = GetComponentInChildren<SpriteRenderer> ();

        ragemanager = GameObject.Find("GlobalScript").GetComponent<RageManager>();
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
			Fire (h,v,1);
		}
		if (Input.GetButton("Fire_P2"))
		{
			int h = (int) Input.GetAxis ("Horizontal_P2");
			int v = (int) Input.GetAxis ("Vertical_P2");
			if (h == 0 && v == 0) {
				v = -1;
			}
			Fire (h,v,2);
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
        //Valeur entre -1 et 1 selon intentsité de frappe sur l'axe horizontal
        float h1 = Input.GetAxis("Horizontal_P1");
		float v1 = Input.GetAxis ("Vertical_P1");
		float h2 = Input.GetAxis("Horizontal_P2");
		float v2 = Input.GetAxis ("Vertical_P2");
        //Fonction responsable du mouvement
		MovePlayer (h1, v1, 1);
		MovePlayer (h2, v2, 2);

		if (gettingHit) {
			rigidBody.AddForce (forceHit, ForceMode.Impulse);
			gettingHit = false;
		}
    }

	private void MovePlayer( float h, float v, int player)
	{
		if (player == ID) {
			rigidBody.velocity = new Vector3 (h * maxSpeed, 0, v * maxSpeed);
			SetBool_H_V (h, v, player);
			//Si on veut utiliser un miroir avec les sprites il faut ces lignes de code
			if ((h > 0 && facingRight) || (h < 0 && !facingRight)) {
				Flip (player);
			}
		} 
	}

    private void PutBool4Directions_False(int player) {
        if (player == 1) {
            animator1.SetBool("GoUp", false);
            animator1.SetBool("GoDown", false);
            animator1.SetBool("GoLeft", false);
            animator1.SetBool("GoRight", false);
        } else {
            animator2.SetBool("GoUp", false);
            animator2.SetBool("GoDown", false);
            animator2.SetBool("GoLeft", false);
            animator2.SetBool("GoRight", false);
        }
    }

	private void SetBool_V(float h, float v, int player)
	{
		if (player == ID) {
			if (v > 0) {
<<<<<<< HEAD
				animator1.SetBool ("GoUp", true);
				animator1.SetBool ("GoDown", false);
                if (h > 0) {
                    animator1.SetBool("GoUpRight", true);
                    animator1.SetBool("GoUpLeft", false);
                    PutBool4Directions_False(1);
                } else if (h < 0) {
                    animator1.SetBool("GoUpLeft", true);
                    animator1.SetBool("GoUpRight", false);
                    PutBool4Directions_False(1);
                } else {
                    animator1.SetBool("GoUpLeft", false);
                    animator1.SetBool("GoUpRight", false);
                }
			} else if (v < 0) {
				animator1.SetBool ("GoUp", false);
				animator1.SetBool ("GoDown", true);
                if (h > 0) {
                    animator1.SetBool("GoDownRight", true);
                    animator1.SetBool("GoDownLeft", false);
                    PutBool4Directions_False(1);
                } else if (h < 0) {
                    animator1.SetBool("GoDownLeft", true);
                    animator1.SetBool("GoDownRight", false);
                    PutBool4Directions_False(1);
                } else {
                    animator1.SetBool("GoDownLeft", false);
                    animator1.SetBool("GoDownRight", false);
                }
            } else {
				animator1.SetBool ("GoUp", false);
                animator1.SetBool("GoDown", false);
                animator1.SetBool("GoDownLeft", false);
                animator1.SetBool("GoDownRight", false);
                animator1.SetBool("GoUpLeft", false);
                animator1.SetBool("GoUpRight", false);
            }
		} else {
            if (v > 0)
            {
                animator2.SetBool("GoUp", true);
                animator2.SetBool("GoDown", false);
                if (h > 0)
                {
                    animator2.SetBool("GoUpRight", true);
                    animator2.SetBool("GoUpLeft", false);
                    PutBool4Directions_False(2);
                }
                else if (h < 0)
                {
                    animator2.SetBool("GoUpLeft", true);
                    animator2.SetBool("GoUpRight", false);
                    PutBool4Directions_False(2);
                }
                else
                {
                    animator2.SetBool("GoUpLeft", false);
                    animator2.SetBool("GoUpRight", false);
                }
            }
            else if (v < 0)
            {
                animator2.SetBool("GoUp", false);
                animator2.SetBool("GoDown", true);
                if (h > 0)
                {
                    animator2.SetBool("GoDownRight", true);
                    animator2.SetBool("GoDownLeft", false);
                    PutBool4Directions_False(2);
                }
                else if (h < 0)
                {
                    animator2.SetBool("GoDownLeft", true);
                    animator2.SetBool("GoDownRight", false);
                    PutBool4Directions_False(2);
                }
                else
                {
                    animator2.SetBool("GoDownLeft", false);
                    animator2.SetBool("GoDownRight", false);
                }
            }
            else
            {
                animator2.SetBool("GoUp", false);
                animator2.SetBool("GoDown", false);
                animator2.SetBool("GoDownLeft", false);
                animator2.SetBool("GoDownRight", false);
                animator2.SetBool("GoUpLeft", false);
                animator2.SetBool("GoUpRight", false);
            }
        }
=======
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
>>>>>>> 86bff81da013f4fd79fb34e680c59f814ad203ca
	}

	private void SetBool_H_V(float h, float v, int player)
	{
		if (player == ID) {
			if (h > 0) {
<<<<<<< HEAD
				animator1.SetBool ("GoLeft", false);
				animator1.SetBool ("GoRight", true);
				SetBool_V (h, v, 1);
			} else if (h < 0) {
				animator1.SetBool ("GoLeft", true);
				animator1.SetBool ("GoRight", false);
				SetBool_V (h, v, 1);
			} else {
				animator1.SetBool ("GoLeft", false);
				animator1.SetBool ("GoRight", false);
				SetBool_V (h, v, 1);
			}
            if (h == 0 && v == 0) {
                animator1.SetBool("Move", false);
            } else {
                animator1.SetBool("Move", true);
            }
		} else {
			if (h > 0) {
				animator2.SetBool ("GoLeft", false);
				animator2.SetBool ("GoRight", true);
				SetBool_V (h, v, 2);
			} else if (h < 0) {
				animator2.SetBool ("GoLeft", true);
				animator2.SetBool ("GoRight", false);
				SetBool_V (h, v, 2);
			} else {
				animator2.SetBool ("GoLeft", false);
				animator2.SetBool ("GoRight", false);
				SetBool_V (h, v, 2);
=======
				animator.SetBool ("GoLeft", false);
				animator.SetBool ("GoRight", true);
				SetBool_V (v, player);
			} else if (h < 0) {
				animator.SetBool ("GoLeft", true);
				animator.SetBool ("GoRight", false);
				SetBool_V (v, player);
			} else {
				animator.SetBool ("GoLeft", false);
				animator.SetBool ("GoRight", false);
				SetBool_V (v, player);
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
>>>>>>> 86bff81da013f4fd79fb34e680c59f814ad203ca
			}
            if (h == 0 && v == 0) {
                animator2.SetBool("Move", false);
            } else {
                animator2.SetBool("Move", true);
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
		add_rage = dataController.SearchID(IDweapon).Getdamage();
		m_rage = m_rage + add_rage;
		ragemanager.AddRage(add_rage,ID);
		gettingHit = true;
		forceHit = dataController.SearchID(IDweapon).Getforce() * direction ;
	}

	public int GetID() {
		return ID;
	}

	public float GetRage() {
		return m_rage;
	}
		
}