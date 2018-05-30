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
    private float taux_rage_1;
    private float taux_rage_2;
    private float maxrage = 100f;
	private float rageDecay = 0.01f;

    // Use this for initialization
    void Start ()
    {
        taux_rage_1 = 0;
        taux_rage_2 = 0;
		rageValue_1.text = "0 / 100";
		rageValue_2.text = "0 / 100";

    }
	
	// Update is called once per frame
	void Update ()
    {
        rage_1.fillAmount = taux_rage_1 / maxrage;
        rage_2.fillAmount = taux_rage_2 / maxrage;
		rageValue_1.text = ((int)taux_rage_1) + " / 100";
		rageValue_2.text = ((int)taux_rage_2) + " / 100";
    }
	void FixedUpdate() {
		taux_rage_1 -= rageDecay;
		taux_rage_2 -= rageDecay;
		if (taux_rage_1 < 0) {
			taux_rage_1 = 0;
		}
		if (taux_rage_2 < 0) {
			taux_rage_2 = 0;
		}
	}

    public void AddRage(float rage,int player) {
        if (player == 1)
        {
            taux_rage_1 += rage;
        }else
        {
            taux_rage_2 += rage;
        }
    }
}
