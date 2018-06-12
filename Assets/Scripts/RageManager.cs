using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RageManager : MonoBehaviour
{
    public Image rage_1;
    public Image rage_2;
	public Text rageValue_1;
	public Text rageValue_2;
	public Text name_P1;
	public Text name_P2;
	private float taux_rage_1;
    private float taux_rage_2;
    private float maxrage = 100f;
	private float rageDecay = 0.01f;
	private DataController dataController = new DataController ();
	private GameController gameController;

    // Use this for initialization
    void Start ()
    {
		gameController = GameObject.Find("GameController").GetComponent<GameController>();
        taux_rage_1 = 0;
        taux_rage_2 = 0;
		rageValue_1.text = "0 / 100";
		rageValue_2.text = "0 / 100";
		name_P1.text = gameController.GetNameP1 ();
		name_P2.text = gameController.GetNameP2 ();

    }
	
	// Update is called once per frame
	void Update ()
    {
        
    }

	void FixedUpdate() {
		rage_1.fillAmount = taux_rage_1 / maxrage;
		rage_2.fillAmount = taux_rage_2 / maxrage;

		taux_rage_1 -= rageDecay;
		taux_rage_2 -= rageDecay;
		if (taux_rage_1 < 0) {
			taux_rage_1 = 0;
		}
		if (taux_rage_2 < 0) {
			taux_rage_2 = 0;
		}
		if (taux_rage_1 >= 100) {
			taux_rage_1 = 100;
			Loose (1);
		}
		if (taux_rage_2 >= 100) {
			taux_rage_2 = 100;
			Loose (2);
		}
		rageValue_1.text = Mathf.RoundToInt(taux_rage_1) + " / 100";
		rageValue_2.text = Mathf.RoundToInt(taux_rage_2) + " / 100";
	}

	public void AddRage(int id,int damageSource) {
		float rage = dataController.SearchID(id).Getdamage();
		if (damageSource == 1)
        {
            taux_rage_2 += rage;
        }else
        {
            taux_rage_1 += rage;
        }
    }

	private void Loose(int ID) {
		gameController.SetWinner (ID);
		gameController.EndGame ();

	}

}
